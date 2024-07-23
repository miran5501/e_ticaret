using E_ticaret.classlar;
using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_ticaret
{
    public partial class Urun_Ekle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KategorileriYukle();
            }
        }

        protected void btnUrunekle_Click(object sender, EventArgs e)
        {
            /// Stok durumu değeri
            int stokDurumu = rblStokDurumu.SelectedValue == "var" ? 1 : 0;

            // Ürün oluşturma tarihi
            DateTime olusturmaTarihi = DateTime.Now;

            // SQL sorgusunu güncelleyin
            SqlCommand commanEkle = new SqlCommand("INSERT INTO Urunler (Urun_Adi, Urun_Fiyat, Urun_Resmi, Urun_Aciklamasi, Urun_KategoriID, Urun_Stok_Durum, Urun_Olusturulma_Tarihi) VALUES (@purun_adi, @purun_fiyat, @purun_resmi, @purun_aciklamasi, @pkategori_id, @pstok_durumu, @polusturma_tarihi)", SqlConnectionClass.connection);
            SqlConnectionClass.CheckConnection();

            commanEkle.Parameters.AddWithValue("@purun_adi", txtUrunismi.Text);
            commanEkle.Parameters.AddWithValue("@purun_fiyat", txtUrunfiyat.Text);
            commanEkle.Parameters.AddWithValue("@purun_resmi", txtUrunresmi.Text);
            commanEkle.Parameters.AddWithValue("@purun_aciklamasi", txtUrunaciklama.Text);
            commanEkle.Parameters.AddWithValue("@pkategori_id", ddlUrunKategori.SelectedValue);
            commanEkle.Parameters.AddWithValue("@pstok_durumu", stokDurumu);
            commanEkle.Parameters.AddWithValue("@polusturma_tarihi", olusturmaTarihi);

            commanEkle.ExecuteNonQuery();
        }

        private void KategorileriYukle()
        {
            using (SqlCommand commanKategori = new SqlCommand("SELECT KategoriID, KategoriAdi FROM Kategoriler", SqlConnectionClass.connection))
            {
                SqlConnectionClass.CheckConnection();
                using (SqlDataReader dr = commanKategori.ExecuteReader())
                {
                    ddlUrunKategori.DataSource = dr;
                    ddlUrunKategori.DataTextField = "KategoriAdi";
                    ddlUrunKategori.DataValueField = "KategoriID";
                    ddlUrunKategori.DataBind();
                    ddlUrunKategori.Items.Insert(0, new ListItem("Seçiniz", ""));
                }
            }
        }
    }
}
