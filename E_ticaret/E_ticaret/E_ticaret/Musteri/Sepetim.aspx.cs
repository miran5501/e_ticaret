using E_ticaret.classlar;
using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_ticaret
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] != null)
            {
                int kullaniciID = (int)Session["KullaniciID"];
                Response.Write("Giriş yapan KullaniciID: " + kullaniciID);
                if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
                {
                    SepetiGoruntule(kullaniciID);
                    KategorileriYukle(kullaniciID);
                }
            }
            else
            {
                Response.Write("Kullanıcı oturum açmamış.");
                Response.Redirect("/Giris_Kayit/GirisYap.aspx");
            }

        }

        private void SepetiGoruntule(int kullaniciID)
        {
            // Stokta olmayan ürünleri sepetten sil
            SqlCommand commandDeleteOutOfStockItems = new SqlCommand(
                "DELETE FROM Sepet_icerik_Table WHERE Sepet_UrunID IN " +
                "(SELECT UrunID FROM Urunler WHERE Urun_Stok_Durum = 0) " +
                "AND SepetinID IN (SELECT SepetID FROM Sepet_Table WHERE Sepet_OlusturanID = @kullaniciID)",
                SqlConnectionClass.connection);
            commandDeleteOutOfStockItems.Parameters.AddWithValue("@kullaniciID", kullaniciID);

            SqlConnectionClass.CheckConnection();
            commandDeleteOutOfStockItems.ExecuteNonQuery();

            // Sepet detaylarını alacak SQL sorgusu
            SqlCommand commandList = new SqlCommand("SELECT Sepet_icerik_Table.SepetinID, Sepet_icerik_Table.Sepet_UrunID, Sepet_icerik_Table.Sepet_urun_fiyat, " +
                                                    "Sepet_icerik_Table.Urun_adet, Urunler.Urun_Adi, Urunler.Urun_Resmi, Urunler.Urun_Aciklamasi " +
                                                    "FROM Sepet_icerik_Table " +
                                                    "INNER JOIN Urunler ON Sepet_icerik_Table.Sepet_UrunID = Urunler.UrunID " +
                                                    "INNER JOIN Sepet_Table ON Sepet_icerik_Table.SepetinID = Sepet_Table.SepetID " +
                                                    "WHERE Sepet_Table.Sepet_OlusturanID = @kullaniciID", SqlConnectionClass.connection);

            commandList.Parameters.AddWithValue("@kullaniciID", kullaniciID);

            SqlDataReader dr = commandList.ExecuteReader();
            Repeater1.DataSource = dr;
            Repeater1.DataBind();
            dr.Close();

            // Sepet tutarını güncellemek için yeni bir SqlCommand oluşturun
            float sepetTutari = GetSepetTutari(kullaniciID);
            LabelToplamTutar.Text = sepetTutari.ToString("N2") + " TL"; // N2 formatıyla iki ondalık basamak gösterimi

            SqlCommand commandUpdate = new SqlCommand("UPDATE Sepet_Table SET Sepet_Tutari = @sepetTutari WHERE Sepet_OlusturanID = @kullaniciID", SqlConnectionClass.connection);
            commandUpdate.Parameters.AddWithValue("@sepetTutari", sepetTutari);
            commandUpdate.Parameters.AddWithValue("@kullaniciID", kullaniciID);
            commandUpdate.ExecuteNonQuery();
            bool sepetBos = sepetTutari <= 0;

            // Butonları durumlarına göre ayarla
            BtnOnayla.Enabled = !sepetBos;
            BtnSepetiTemizle.Enabled = !sepetBos;
        }


        protected void BtnOnayla_Click(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];

            // Yeni siparişin eklenmesi için SQL komutu
            SqlCommand commandInsertSiparis = new SqlCommand("INSERT INTO Siparisler (Siparis_Tutari, Siparis_Olusturma_Tarihi, Siparis_Teslim_Adresi, Siparis_OlusturanID, Siparis_Notu, Siparis_Durumu) " +
                                                             "SELECT @pSepetTutari, GETDATE(), @pTeslimAdresi, Sepet_Table.Sepet_OlusturanID, @pNot, @pDurum " +
                                                             "FROM Sepet_Table " +
                                                             "WHERE Sepet_Table.Sepet_OlusturanID = @kullaniciID; SELECT SCOPE_IDENTITY()", SqlConnectionClass.connection);

            SqlConnectionClass.CheckConnection();

            commandInsertSiparis.Parameters.AddWithValue("@pSepetTutari", GetSepetTutari(kullaniciID));
            commandInsertSiparis.Parameters.AddWithValue("@pTeslimAdresi", ddlAdres.SelectedValue);
            commandInsertSiparis.Parameters.AddWithValue("@pNot", TextBoxNot.Text);
            commandInsertSiparis.Parameters.AddWithValue("@kullaniciID", kullaniciID);
            commandInsertSiparis.Parameters.AddWithValue("@pDurum", "Bekliyor");

            int siparisID = Convert.ToInt32(commandInsertSiparis.ExecuteScalar());

            // Sipariş başarıyla oluşturulduğunda, sepet içeriğini sipariş içeriğine aktar
            SqlCommand commandInsertSiparisIcerik = new SqlCommand("INSERT INTO Siparis_Urunler_Table (SiparisID, Siparis_urunID, Siparis_urun_adet, Siparis_urun_fiyat) " +
                                                                    "SELECT @pSiparisID, Sepet_UrunID, Urun_adet, Sepet_urun_fiyat " +
                                                                    "FROM Sepet_icerik_Table " +
                                                                    "INNER JOIN Sepet_Table ON Sepet_icerik_Table.SepetinID = Sepet_Table.SepetID " +
                                                                    "WHERE Sepet_Table.Sepet_OlusturanID = @kullaniciID", SqlConnectionClass.connection);

            commandInsertSiparisIcerik.Parameters.AddWithValue("@pSiparisID", siparisID);
            commandInsertSiparisIcerik.Parameters.AddWithValue("@kullaniciID", kullaniciID);

            int affectedRowsSiparisIcerik = commandInsertSiparisIcerik.ExecuteNonQuery();

            if (affectedRowsSiparisIcerik > 0)
            {
                // Sepeti ve sepet içeriğini silme işlemi
                SqlCommand commandDeleteSepetIcerik = new SqlCommand(
                    "DELETE FROM Sepet_icerik_Table WHERE SepetinID IN (SELECT SepetID FROM Sepet_Table WHERE Sepet_OlusturanID = @kullaniciID)",
                    SqlConnectionClass.connection);
                commandDeleteSepetIcerik.Parameters.AddWithValue("@kullaniciID", kullaniciID);
                commandDeleteSepetIcerik.ExecuteNonQuery();

                SqlCommand commandDeleteSepet = new SqlCommand(
                    "DELETE FROM Sepet_Table WHERE Sepet_OlusturanID = @kullaniciID",
                    SqlConnectionClass.connection);
                commandDeleteSepet.Parameters.AddWithValue("@kullaniciID", kullaniciID);
                commandDeleteSepet.ExecuteNonQuery();

                Response.Write("Sepetiniz başarıyla siparişe dönüştürüldü!");
                SepetiGoruntule(kullaniciID);
            }
            else
            {
                Response.Write("Sipariş içeriği oluşturma işlemi başarısız oldu.");
            }
        }
        protected void BtnSepetiTemizle_Click(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];

            // Sepet içeriğini silme işlemi
            SqlCommand commandDeleteSepetIcerik = new SqlCommand(
                "DELETE FROM Sepet_icerik_Table WHERE SepetinID IN (SELECT SepetID FROM Sepet_Table WHERE Sepet_OlusturanID = @kullaniciID)",
                SqlConnectionClass.connection);
            commandDeleteSepetIcerik.Parameters.AddWithValue("@kullaniciID", kullaniciID);

            SqlConnectionClass.CheckConnection();
            commandDeleteSepetIcerik.ExecuteNonQuery();

            SqlCommand commandDeleteSepet = new SqlCommand(
                "DELETE FROM Sepet_Table WHERE Sepet_OlusturanID = @kullaniciID",
                SqlConnectionClass.connection);
            commandDeleteSepet.Parameters.AddWithValue("@kullaniciID", kullaniciID);
            commandDeleteSepet.ExecuteNonQuery();

            // Sepeti temizledikten sonra sayfayı yenile
            SepetiGoruntule(kullaniciID);
        }
        private void KategorileriYukle(int kullaniciID)
        {
            SqlCommand commandAdresler = new SqlCommand("SELECT AdresBasligi, Adres FROM Adresler WHERE AdresSahibiID=@pkullaniciID", SqlConnectionClass.connection);
            commandAdresler.Parameters.AddWithValue("@pkullaniciID", kullaniciID);

            SqlConnectionClass.CheckConnection();
            using (SqlDataReader dr = commandAdresler.ExecuteReader())
            {
                ddlAdres.DataSource = dr;
                ddlAdres.DataTextField = "AdresBasligi";
                ddlAdres.DataValueField = "Adres";
                ddlAdres.DataBind();
                ddlAdres.Items.Insert(0, new ListItem("Seçiniz", ""));
            }

        }





        protected void BtnUrunuSil_Click(object sender, EventArgs e)
        {
            // Button'un CommandArgument özelliğinden sileceğimiz ürünün ID'sini alalım
            Button btn = (Button)sender;
            int urunID = Convert.ToInt32(btn.CommandArgument);

            // Session'dan kullanıcı ID'sini alalım
            int kullaniciID = (int)Session["KullaniciID"];

            // Sepet işlemleri için SQL komutu: Sepet_icerik_Table'dan ilgili ürünü silecek
            SqlCommand commandSil = new SqlCommand("DELETE FROM Sepet_icerik_Table WHERE Sepet_UrunID = @urunID AND SepetinID IN (SELECT SepetID FROM Sepet_Table WHERE Sepet_OlusturanID = @kullaniciID)", SqlConnectionClass.connection);
            SqlConnectionClass.CheckConnection();

            commandSil.Parameters.AddWithValue("@urunID", urunID);
            commandSil.Parameters.AddWithValue("@kullaniciID", kullaniciID);

            int affectedRows = commandSil.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                // Ürün başarıyla silindiği durumda sepeti güncelleyelim
                SepetiGoruntule(kullaniciID);
                Response.Write("Ürün sepetten başarıyla silindi.");
            }
            else
            {
                Response.Write("Ürün silme işlemi başarısız oldu.");
            }
        }




        private float GetSepetTutari(int kullaniciID)
        {
            float sepet_Tutari = 0;

            // Sepet içeriğinden toplam tutarı hesaplamak için SQL sorgusu
            SqlCommand commandTutar = new SqlCommand("SELECT Sepet_icerik_Table.Sepet_urun_fiyat, Sepet_icerik_Table.Urun_adet " +
                                                     "FROM Sepet_icerik_Table " +
                                                     "INNER JOIN Sepet_Table ON Sepet_icerik_Table.SepetinID = Sepet_Table.SepetID " +
                                                     "WHERE Sepet_Table.Sepet_OlusturanID = @kullaniciID", SqlConnectionClass.connection);
            commandTutar.Parameters.AddWithValue("@kullaniciID", kullaniciID);
            SqlConnectionClass.CheckConnection();

            SqlDataReader dr = commandTutar.ExecuteReader();

            while (dr.Read())
            {
                float urun_fiyat = Convert.ToSingle(dr["Sepet_urun_fiyat"]);
                int urun_adet = Convert.ToInt32(dr["Urun_adet"]);
                sepet_Tutari += urun_fiyat * urun_adet;
            }
            dr.Close();

            return sepet_Tutari;
        }

        protected void BtnAdetAzalt_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int urunID = Convert.ToInt32(btn.CommandArgument);
            int kullaniciID = (int)Session["KullaniciID"];

            SqlCommand commandUpdate = new SqlCommand("UPDATE Sepet_icerik_Table SET Urun_adet = Urun_adet - 1 " +
                                                      "WHERE Sepet_UrunID = @urunID AND SepetinID IN (SELECT SepetID FROM Sepet_Table WHERE Sepet_OlusturanID = @kullaniciID)", SqlConnectionClass.connection);
            commandUpdate.Parameters.AddWithValue("@urunID", urunID);
            commandUpdate.Parameters.AddWithValue("@kullaniciID", kullaniciID);
            SqlConnectionClass.CheckConnection();

            int affectedRows = commandUpdate.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                SepetiGoruntule(kullaniciID);
                Response.Write("Ürün adeti azaltıldı.");
            }
            else
            {
                Response.Write("Ürün adeti azaltma işlemi başarısız oldu.");
            }
        }

        protected void BtnAdetArttir_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int urunID = Convert.ToInt32(btn.CommandArgument);
            int kullaniciID = (int)Session["KullaniciID"];

            SqlCommand commandUpdate = new SqlCommand("UPDATE Sepet_icerik_Table SET Urun_adet = Urun_adet + 1 " +
                                                      "WHERE Sepet_UrunID = @urunID AND SepetinID IN (SELECT SepetID FROM Sepet_Table WHERE Sepet_OlusturanID = @kullaniciID)", SqlConnectionClass.connection);
            commandUpdate.Parameters.AddWithValue("@urunID", urunID);
            commandUpdate.Parameters.AddWithValue("@kullaniciID", kullaniciID);
            SqlConnectionClass.CheckConnection();

            int affectedRows = commandUpdate.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                SepetiGoruntule(kullaniciID);
                Response.Write("Ürün adeti artırıldı.");
            }
            else
            {
                Response.Write("Ürün adeti artırma işlemi başarısız oldu.");
            }
        }


        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Urun_adet değerini alın
                int urun_adet = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Urun_adet"));

                // BtnAdetAzalt butonunu bulun
                Button btnAdetAzalt = (Button)e.Item.FindControl("BtnAdetAzalt");

                // Eğer urun_adet 1 veya daha düşükse butonu görünmez yapın
                if (urun_adet <= 1)
                {
                    btnAdetAzalt.Enabled = false;
                }
                else
                {
                    btnAdetAzalt.Enabled = true; // Gerekli değil, ama ekstra açıklık için
                }
            }
        }

    }
}
