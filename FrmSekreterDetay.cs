using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Hastane_Otomasyon
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCnumara;

            //Ad Soyad

            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTc=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lbladsoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            // Bransları Datagride aktarma

            DataTable dt1 = new DataTable();
            SqlDataAdapter da=new SqlDataAdapter("Select * from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktorları Datagride aktarma

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' '+ DoktorSoyad) as'Doktorlar', DoktorBrans from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //bransı combax a aktarma

            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                combrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void btndurum_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values(@r1,@r2,@r3,@r4)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", mastarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", massaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", combrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", comdoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevunuz Oluşturulmuştur");
        }

        private void comdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            comdoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", combrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comdoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnoluştur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular(duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", ricduyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");

        }

        private void btndoktor_Click(object sender, EventArgs e)
        {
            Frmdoktorpaneli drp = new Frmdoktorpaneli();
            drp.Show();
        }

        private void btnbrans_Click(object sender, EventArgs e)
        {
            Frmbrans frb = new Frmbrans();
            frb.Show();
        }

        private void btnrandevu_Click(object sender, EventArgs e)
        {
            Frmrandevulistesi frl = new Frmrandevulistesi();
            frl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frmduyurular fr = new Frmduyurular();
            fr.Show();
        }
    }
}
