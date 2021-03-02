﻿using System;
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
    public partial class Frmdoktorpaneli : Form
    {
        public Frmdoktorpaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Frmdoktorpaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select *from Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;


            //bransları combabaoxa aktarma

            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                combrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar(DoktorAd,DoktorSoyad,DoktorBrans,DoktorTc,DoktorSifre)values(@d1,@d2,@d3,@d4,@d5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", txad.Text);
            komut.Parameters.AddWithValue("@d2", txsoyad.Text);
            komut.Parameters.AddWithValue("@d3", combrans.Text);
            komut.Parameters.AddWithValue("@d4", masTC.Text);
            komut.Parameters.AddWithValue("@d5", txsifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            combrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            masTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txsifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Doktorlar where DoktorTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", masTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d5 where DoktorTc=@d4", bgl.baglanti());
            komut1.Parameters.AddWithValue("@d1", txad.Text);
            komut1.Parameters.AddWithValue("@d2", txsoyad.Text);
            komut1.Parameters.AddWithValue("@d3", combrans.Text);
            komut1.Parameters.AddWithValue("@d4", masTC.Text);
            komut1.Parameters.AddWithValue("@d5", txsifre.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
