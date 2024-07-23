using E_ticaret.classlar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_ticaret.Musteri
{
    public partial class Profilim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kullanıcı oturum kontrolü
            if (Session["KullaniciID"] != null)
            {
                int kullaniciID = (int)Session["KullaniciID"];
                Response.Write("Giriş yapan KullaniciID: " + kullaniciID);
            }
            else
            {
                Response.Write("Kullanıcı oturum açmamış.");
                Response.Redirect("/Giris_Kayit/GirisYap.aspx");
            }


            if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
            {
                // Kullanıcı ID'sini Session'dan alıp integer'a çevirme işlemi
                int kullaniciid = Convert.ToInt32(Session["KullaniciID"]);

                // SQL sorgusunu oluşturma, kullanıcı ID'sini doğru şekilde yerine koyma
                string sqlQuery = "SELECT * FROM Kullanicilar_Table WHERE KullaniciID=@KullaniciID";
                SqlCommand commandKullanici = new SqlCommand(sqlQuery, SqlConnectionClass.connection);
                commandKullanici.Parameters.AddWithValue("@KullaniciID", kullaniciid);

                // Bağlantıyı kontrol etme ve veri okuma işlemi
                SqlConnectionClass.CheckConnection();
                SqlDataReader dr = commandKullanici.ExecuteReader();

                if (dr.Read())
                {
                    txtAdSoyad.Text = dr["KullaniciAdSoyad"].ToString();
                    txtEmail.Text = dr["E_mail"].ToString();
                    txtTelefon.Text = dr["Telefon_No"].ToString();
                }



                dr.Close(); // SqlDataReader'ı kapat
            }
        }
        protected void BtnBilgileriGuncelle_Click(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];
            string sqlQuery = "UPDATE Kullanicilar_Table SET KullaniciAdSoyad=@padsoyad, E_mail=@pemail, Telefon_No=@ptelefonno WHERE KullaniciID=@pkullaniciID";
            SqlCommand commandUpdate = new SqlCommand(sqlQuery, SqlConnectionClass.connection);
            SqlConnectionClass.CheckConnection();

            commandUpdate.Parameters.AddWithValue("@padsoyad", txtAdSoyad.Text);
            commandUpdate.Parameters.AddWithValue("@pemail", txtEmail.Text);
            commandUpdate.Parameters.AddWithValue("@ptelefonno", txtTelefon.Text);
            commandUpdate.Parameters.AddWithValue("@pkullaniciID", kullaniciID);

            int rowsAffected = commandUpdate.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                // Başarılı güncelleme durumunda bir mesaj veya işlem yapabilirsiniz
                Response.Write("Bilgileriniz başarıyla güncellendi.");
            }
            else
            {
                // Güncelleme başarısız ise bir mesaj veya işlem yapabilirsiniz
                Response.Write("Bilgileriniz güncellenirken bir hata oluştu.");
            }
        }
        protected void BtnYeniAdres_Click(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];
            string sqlQuery = "Insert into Adresler(AdresSahibiID, AdresBasligi, Adres) values (@pkullaniciID,@padresBasligi,@padres)";
            SqlCommand commandAdres = new SqlCommand(sqlQuery, SqlConnectionClass.connection);
            SqlConnectionClass.CheckConnection();
            commandAdres.Parameters.AddWithValue("@pkullaniciID", kullaniciID);
            commandAdres.Parameters.AddWithValue("@padresBasligi", txtAdresBasligi.Text);
            commandAdres.Parameters.AddWithValue("@padres", txtAdres.Text);
            

            int rowsAffected = commandAdres.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                // Başarılı güncelleme durumunda bir mesaj veya işlem yapabilirsiniz
                Response.Write("Bilgileriniz başarıyla güncellendi.");
            }
            else
            {
                // Güncelleme başarısız ise bir mesaj veya işlem yapabilirsiniz
                Response.Write("Bilgileriniz güncellenirken bir hata oluştu.");
            }
        }
        protected void BtnSifreyiDegistir_Click(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];
            SqlCommand commandSifreKontrol = new SqlCommand("SELECT Sifre FROM Kullanicilar_Table WHERE KullaniciID=@kullaniciID", SqlConnectionClass.connection);
            commandSifreKontrol.Parameters.AddWithValue("@kullaniciID", kullaniciID);
            SqlConnectionClass.CheckConnection();
            string eskiSifreKontrol = Sha256Converter.ComputeSha256Hash(txtEskiSifre.Text);
            string sifreVeritabanindan = (string)commandSifreKontrol.ExecuteScalar();
            if (eskiSifreKontrol==sifreVeritabanindan)
            {
                if(txtYeniSifre.Text==txtYeniSifreTekrar.Text)
                {
                    string yeniSifre = Sha256Converter.ComputeSha256Hash(txtYeniSifre.Text);
                    // Şifre güncelleme sorgusu
                    string sqlQuery = "UPDATE Kullanicilar_Table SET Sifre=@psifre WHERE KullaniciID=@pkullaniciID";
                    SqlCommand commandUpdate = new SqlCommand(sqlQuery, SqlConnectionClass.connection);

                    // Parametreleri ekleyerek güncelleme sorgusunu hazırlama
                    commandUpdate.Parameters.AddWithValue("@psifre", yeniSifre);
                    commandUpdate.Parameters.AddWithValue("@pkullaniciID", kullaniciID);

                    // Bağlantıyı kontrol etme ve güncelleme işlemini gerçekleştirme
                    SqlConnectionClass.CheckConnection();
                    int rowsAffected = commandUpdate.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Başarılı güncelleme durumunda bir mesaj veya işlem yapabilirsiniz
                        Response.Write("Şifreniz başarıyla güncellendi.");
                    }
                    else
                    {
                        // Güncelleme başarısız ise bir mesaj veya işlem yapabilirsiniz
                        Response.Write("Şifreniz güncellenirken bir hata oluştu.");
                    }
                }
                else
                {
                    Response.Write("şifreler eşleşmiyor");
                }
                
            }
            else
            {
                Response.Write("Eski şifre yanlış");
            }
            

            // Yeni şifre bilgisini alın
            
        }
    }
}