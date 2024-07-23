using E_ticaret.classlar;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace E_ticaret.Admin
{
    public partial class Urun_Guncelle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ürün ID'sini query string'den al
                if (Request.QueryString["UrunID"] != null)
                {
                    int urunID = Convert.ToInt32(Request.QueryString["UrunID"]);

                    // Ürün bilgilerini veritabanından al
                    string sqlUrunler = "SELECT * FROM Urunler WHERE UrunID=@urunID";
                    SqlCommand commandUrun = new SqlCommand(sqlUrunler, SqlConnectionClass.connection);
                    commandUrun.Parameters.AddWithValue("@urunID", urunID);

                    // Bağlantıyı kontrol etme ve veri okuma işlemi
                    SqlConnectionClass.CheckConnection();
                    SqlDataReader drUrun = commandUrun.ExecuteReader();

                    if (drUrun.Read())
                    {
                        txtUrunismiGuncelle.Text = drUrun["Urun_Adi"].ToString();
                        txtUrunfiyatGuncelle.Text = drUrun["Urun_Fiyat"].ToString();
                        txtUrunresmiGuncelle.Text = drUrun["Urun_Resmi"].ToString();
                        txtUrunaciklamaGuncelle.Text = drUrun["Urun_Aciklamasi"].ToString();

                        // Ürünün kategori ID'sini al
                        int kategoriID = Convert.ToInt32(drUrun["Urun_KategoriID"]);

                        // Stok durumu RadioButtonList'e seçilen değeri atama
                        bool stokDurumu = Convert.ToBoolean(drUrun["Urun_Stok_Durum"]);
                        rblStokDurumuGuncelle.SelectedValue = stokDurumu ? "1" : "0";

                        // Ürün kategorisini seçili olarak göster
                        ddlUrunKategoriGuncelle.SelectedValue = kategoriID.ToString();
                    }

                    // SqlDataReader'ı kapat
                    drUrun.Close();

                    // Kategori listesini al ve DropDownList'e bağla
                    string sqlKategori = "SELECT KategoriID, KategoriAdi FROM Kategoriler ORDER BY KategoriID";
                    SqlCommand commandKategoriler = new SqlCommand(sqlKategori, SqlConnectionClass.connection);

                    // Bağlantıyı kontrol etme ve veri okuma işlemi
                    SqlConnectionClass.CheckConnection();
                    SqlDataReader drKategoriler = commandKategoriler.ExecuteReader();

                    // DropDownList'e kategori verilerini ekleyin
                    ddlUrunKategoriGuncelle.DataSource = drKategoriler;
                    ddlUrunKategoriGuncelle.DataTextField = "KategoriAdi";
                    ddlUrunKategoriGuncelle.DataValueField = "KategoriID";
                    ddlUrunKategoriGuncelle.DataBind();

                    // SqlDataReader'ı kapatın
                    drKategoriler.Close();
                }
                else
                {
                    // UrunID query string'de yoksa, hata işleme veya başka bir işlem yapma
                    Response.Redirect($"Urunleri_Yonet.aspx");
                }
            }
        }
        protected void btnUrunGuncelle_Click(object sender, EventArgs e)
        {
            // Ürün ID'sini query string'den al
            int urunID = Convert.ToInt32(Request.QueryString["UrunID"]);

            // Güncellenecek ürün bilgilerini al
            string urunAdi = txtUrunismiGuncelle.Text.Trim();
            decimal urunFiyat = Convert.ToDecimal(txtUrunfiyatGuncelle.Text.Trim());
            string urunResmi = txtUrunresmiGuncelle.Text.Trim();
            string urunAciklamasi = txtUrunaciklamaGuncelle.Text.Trim();
            int kategoriID = Convert.ToInt32(ddlUrunKategoriGuncelle.SelectedValue);
            bool stokDurumu = rblStokDurumuGuncelle.SelectedValue == "1";

            // Ürünü veritabanında güncelle
            string sqlUpdate = @"UPDATE Urunler SET Urun_Adi=@urunAdi, Urun_Fiyat=@urunFiyat, 
                                 Urun_Resmi=@urunResmi, Urun_Aciklamasi=@urunAciklamasi, 
                                 Urun_KategoriID=@kategoriID, Urun_Stok_Durum=@stokDurumu 
                                 WHERE UrunID=@urunID";
            SqlCommand commandUpdate = new SqlCommand(sqlUpdate, SqlConnectionClass.connection);
            commandUpdate.Parameters.AddWithValue("@urunAdi", urunAdi);
            commandUpdate.Parameters.AddWithValue("@urunFiyat", urunFiyat);
            commandUpdate.Parameters.AddWithValue("@urunResmi", urunResmi);
            commandUpdate.Parameters.AddWithValue("@urunAciklamasi", urunAciklamasi);
            commandUpdate.Parameters.AddWithValue("@kategoriID", kategoriID);
            commandUpdate.Parameters.AddWithValue("@stokDurumu", stokDurumu);
            commandUpdate.Parameters.AddWithValue("@urunID", urunID);

            // Bağlantıyı kontrol etme ve güncelleme işlemi
            SqlConnectionClass.CheckConnection();
            commandUpdate.ExecuteNonQuery();

            // Güncelleme işleminden sonra yönlendirme yap
            Response.Redirect("Urunleri_Yonet.aspx");
        }

    }
}
