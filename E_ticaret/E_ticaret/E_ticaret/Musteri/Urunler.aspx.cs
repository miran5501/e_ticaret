using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using E_ticaret.classlar;

namespace E_ticaret
{
    public partial class Urunler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kullanıcı oturum kontrolü
            //if (Session["KullaniciID"] != null)
            //{
            //    int kullaniciID = (int)Session["KullaniciID"];
            //    Response.Write("Giriş yapan KullaniciID: " + kullaniciID);
            //}
            //else
            //{
            //    Response.Write("Kullanıcı oturum açmamış.");
            //    Response.Redirect("/Giris_Kayit/GirisYap.aspx");
            //}


            if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
            {
                // Ürünleri veritabanından getirerek Datalist1'e bağla
                SqlCommand commandList = new SqlCommand("SELECT UrunID, Urun_Adi, Urun_Fiyat, Urun_Resmi, Urun_Aciklamasi, Urun_Stok_Durum FROM Urunler WHERE Urun_Stok_Durum = 1", SqlConnectionClass.connection);

                SqlConnectionClass.CheckConnection();

                SqlDataReader dr = commandList.ExecuteReader();

                Datalist1.DataSource = dr;
                Datalist1.DataBind();
                dr.Close(); // SqlDataReader'ı kapat
                LoadSelectedItems();
            }


        }
        protected void SilStoktaOlmayanUrunler(int sepetID)
        {
            // Stokta olmayan ürünleri sepetten sil
            SqlCommand commandGetOutOfStockItems = new SqlCommand("SELECT Sepet_UrunID FROM Sepet_icerik_Table WHERE SepetinID = @psepetid AND Sepet_UrunID IN (SELECT UrunID FROM Urunler WHERE Urun_Stok_Durum = 0)", SqlConnectionClass.connection);
            commandGetOutOfStockItems.Parameters.AddWithValue("@psepetid", sepetID);

            SqlConnectionClass.CheckConnection();
            SqlDataReader reader = commandGetOutOfStockItems.ExecuteReader();
            List<int> outOfStockItems = new List<int>();

            while (reader.Read())
            {
                outOfStockItems.Add(reader.GetInt32(0));
            }
            reader.Close();

            foreach (int urunID in outOfStockItems)
            {
                using (SqlCommand commandDelete = new SqlCommand("DELETE FROM Sepet_icerik_Table WHERE SepetinID = @psepetinid AND Sepet_UrunID = @psepeturunid", SqlConnectionClass.connection))
                {
                    commandDelete.Parameters.AddWithValue("@psepetinid", sepetID);
                    commandDelete.Parameters.AddWithValue("@psepeturunid", urunID);
                    commandDelete.ExecuteNonQuery();
                }
            }

            // Sepet tutarını yeniden hesapla ve güncelle
            float toplamtutar = 0;
            SqlCommand commandCalculateTotal = new SqlCommand("SELECT SUM(Sepet_urun_fiyat * Urun_adet) FROM Sepet_icerik_Table WHERE SepetinID = @psepetid", SqlConnectionClass.connection);
            commandCalculateTotal.Parameters.AddWithValue("@psepetid", sepetID);
            object totalResult = commandCalculateTotal.ExecuteScalar();

            if (totalResult != DBNull.Value)
            {
                toplamtutar = Convert.ToSingle(totalResult);
            }

            SqlCommand updateSepetTutar = new SqlCommand("UPDATE Sepet_Table SET Sepet_Tutari = @psepettutari WHERE SepetID = @psepetid", SqlConnectionClass.connection);
            updateSepetTutar.Parameters.AddWithValue("@psepettutari", toplamtutar);
            updateSepetTutar.Parameters.AddWithValue("@psepetid", sepetID);
            updateSepetTutar.ExecuteNonQuery();
        }

        protected void BtnSepeteEkle_Click(object sender, EventArgs e)
        {
            Sepet_Olustur();
        }

        public void Sepet_Olustur()
        {
            List<int> seciliUrunler = new List<int>();
            List<int> kaldirilanUrunler = new List<int>();

            foreach (DataListItem item in Datalist1.Items)
            {
                CheckBox chkBox = (CheckBox)item.FindControl("CheckBoxAddToCart");
                HiddenField hiddenUrunID = (HiddenField)item.FindControl("HiddenFieldUrunID");
                int urunID = Convert.ToInt32(hiddenUrunID.Value);

                if (chkBox.Checked)
                {
                    seciliUrunler.Add(urunID);
                }
                else
                {
                    kaldirilanUrunler.Add(urunID);
                }
            }

            Session["SeciliUrunler"] = seciliUrunler;

            int kullaniciID = (int)Session["KullaniciID"];
            DateTime currentDateTime = DateTime.Now;

            SqlCommand commandCheckSepet = new SqlCommand("SELECT SepetID FROM Sepet_Table WHERE Sepet_OlusturanID = @psepetolusturan", SqlConnectionClass.connection);
            commandCheckSepet.Parameters.AddWithValue("@psepetolusturan", kullaniciID);

            SqlConnectionClass.CheckConnection();
            object result = commandCheckSepet.ExecuteScalar();
            int sepetID;

            if (result != null)
            {
                sepetID = Convert.ToInt32(result);
            }
            else
            {
                SqlCommand commandSepet = new SqlCommand("INSERT INTO Sepet_Table(Sepet_OlusturanID, Sepet_Olusturma_Tarihi, Sepet_Tutari) VALUES (@psepetolusturan, @psepetolusturmatarihi, @psepettutari); SELECT SCOPE_IDENTITY();", SqlConnectionClass.connection);
                commandSepet.Parameters.AddWithValue("@psepetolusturan", kullaniciID);
                commandSepet.Parameters.AddWithValue("@psepetolusturmatarihi", currentDateTime);
                commandSepet.Parameters.AddWithValue("@psepettutari", 0);
                sepetID = Convert.ToInt32(commandSepet.ExecuteScalar());
            }

            float toplamtutar = sepet_icerik(sepetID, seciliUrunler, kaldirilanUrunler);

            Session["SepetID"] = sepetID;

            SqlCommand updateSepetTutar = new SqlCommand("UPDATE Sepet_Table SET Sepet_Tutari = @psepettutari WHERE SepetID = @psepetid", SqlConnectionClass.connection);
            updateSepetTutar.Parameters.AddWithValue("@psepettutari", toplamtutar);
            updateSepetTutar.Parameters.AddWithValue("@psepetid", sepetID);
            updateSepetTutar.ExecuteNonQuery();

            // Sepet sayfasına yönlendirme yapabilirsiniz
            // Response.Redirect("Sepet.aspx");
        }

        public float sepet_icerik(int sepetID, List<int> seciliUrunler, List<int> kaldirilanUrunler)
        {
            float tutar = 0;

            // Sepetteki mevcut ürünleri kontrol et
            SqlCommand commandGetExistingItems = new SqlCommand("SELECT Sepet_UrunID FROM Sepet_icerik_Table WHERE SepetinID = @psepetid", SqlConnectionClass.connection);
            commandGetExistingItems.Parameters.AddWithValue("@psepetid", sepetID);

            SqlConnectionClass.CheckConnection();
            SqlDataReader reader = commandGetExistingItems.ExecuteReader();
            HashSet<int> existingItems = new HashSet<int>();

            while (reader.Read())
            {
                existingItems.Add(reader.GetInt32(0));
            }

            reader.Close();

            // Seçili ürünleri ekle
            foreach (int urunID in seciliUrunler)
            {
                if (!existingItems.Contains(urunID))
                {
                    using (SqlCommand commandFiyat = new SqlCommand("SELECT Urun_Fiyat FROM Urunler WHERE UrunID = @urunID", SqlConnectionClass.connection))
                    {
                        commandFiyat.Parameters.AddWithValue("@urunID", urunID);
                        float urunFiyat = Convert.ToSingle(commandFiyat.ExecuteScalar());

                        using (SqlCommand commandSepeticerik = new SqlCommand("INSERT INTO Sepet_icerik_Table(SepetinID, Sepet_UrunID, Sepet_urun_fiyat, Urun_adet) VALUES (@psepetinid, @psepeturunid, @psepeturunfiyat, @purunadet)", SqlConnectionClass.connection))
                        {
                            int urunAdet = 1;

                            commandSepeticerik.Parameters.AddWithValue("@psepetinid", sepetID);
                            commandSepeticerik.Parameters.AddWithValue("@psepeturunid", urunID);
                            commandSepeticerik.Parameters.AddWithValue("@psepeturunfiyat", urunFiyat);
                            commandSepeticerik.Parameters.AddWithValue("@purunadet", urunAdet);

                            tutar += urunFiyat;
                            commandSepeticerik.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Seçimi kaldırılan ürünleri sil
            foreach (int urunID in kaldirilanUrunler)
            {
                if (existingItems.Contains(urunID))
                {
                    using (SqlCommand commandDelete = new SqlCommand("DELETE FROM Sepet_icerik_Table WHERE SepetinID = @psepetinid AND Sepet_UrunID = @psepeturunid", SqlConnectionClass.connection))
                    {
                        commandDelete.Parameters.AddWithValue("@psepetinid", sepetID);
                        commandDelete.Parameters.AddWithValue("@psepeturunid", urunID);
                        commandDelete.ExecuteNonQuery();
                    }
                }
            }

            // Sepet tutarını yeniden hesapla
            SqlCommand commandCalculateTotal = new SqlCommand("SELECT SUM(Sepet_urun_fiyat * Urun_adet) FROM Sepet_icerik_Table WHERE SepetinID = @psepetid", SqlConnectionClass.connection);
            commandCalculateTotal.Parameters.AddWithValue("@psepetid", sepetID);
            object totalResult = commandCalculateTotal.ExecuteScalar();

            if (totalResult != DBNull.Value)
            {
                tutar = Convert.ToSingle(totalResult);
            }

            return tutar;
        }


        protected void LoadSelectedItems()
        {
            if (Session["KullaniciID"] != null)
            {
                int kullaniciID = (int)Session["KullaniciID"];

                // Kullanıcıya ait sepeti veritabanından al
                SqlCommand commandCheckSepet = new SqlCommand("SELECT SepetID FROM Sepet_Table WHERE Sepet_OlusturanID = @psepetolusturan", SqlConnectionClass.connection);
                commandCheckSepet.Parameters.AddWithValue("@psepetolusturan", kullaniciID);

                SqlConnectionClass.CheckConnection();
                object result = commandCheckSepet.ExecuteScalar();

                if (result != null)
                {
                    int sepetID = Convert.ToInt32(result);

                    // Sepet içeriğini al
                    SqlCommand commandGetSepetIcerik = new SqlCommand("SELECT Sepet_UrunID FROM Sepet_icerik_Table WHERE SepetinID = @psepetid", SqlConnectionClass.connection);
                    commandGetSepetIcerik.Parameters.AddWithValue("@psepetid", sepetID);

                    SqlDataReader reader = commandGetSepetIcerik.ExecuteReader();
                    List<int> sepetUrunler = new List<int>();

                    while (reader.Read())
                    {
                        sepetUrunler.Add(reader.GetInt32(0));
                    }

                    reader.Close();

                    // DataList içindeki ürünlerle sepet içeriğini karşılaştır ve eşleşenleri seçili yap
                    foreach (DataListItem item in Datalist1.Items)
                    {
                        CheckBox cb = (CheckBox)item.FindControl("CheckBoxAddToCart");
                        HiddenField hf = (HiddenField)item.FindControl("HiddenFieldUrunID");

                        if (cb != null && sepetUrunler.Contains(int.Parse(hf.Value)))
                        {
                            cb.Checked = true;
                        }
                    }
                }
            }
        }



    }
}
