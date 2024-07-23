using E_ticaret.classlar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_ticaret
{
    public partial class Kullanicilari_Yonet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KullaniciID"] != null)
            {
                int kullaniciID = (int)Session["KullaniciID"];
                Response.Write("Giriş yapan KullaniciID: " + kullaniciID);
            }
            else
            {
                Response.Write("Kullanıcı oturum açmamış.");
                //Response.Redirect("/Giris_Kayit/GirisYap.aspx");
            }
            //ctrl+k ctrl+u yorum satırı toplu kaldırır seçilileri

            if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
            {
                KullaniciListesiniYukle();
            }
        }
        protected void BtnKullaniciSil_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int kullaniciID = Convert.ToInt32(btn.CommandArgument);

            // Kullanıcıyı veritabanından sil
            SqlCommand command = new SqlCommand("DELETE FROM Kullanicilar_Table WHERE KullaniciID=@KullaniciID", SqlConnectionClass.connection);
            command.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            SqlConnectionClass.CheckConnection();
            command.ExecuteNonQuery();
            KullaniciListesiniYukle();
        }
        public void KullaniciListesiniYukle()
        {
            // Ürünleri veritabanından getirerek Datalist1'e bağla
            SqlCommand commandKullanicilar = new SqlCommand("SELECT KullaniciID,KullaniciAdSoyad, KullaniciAdi, E_mail, Telefon_No, KullaniciYetki FROM Kullanicilar_Table", SqlConnectionClass.connection);

            SqlConnectionClass.CheckConnection();

            SqlDataReader dr = commandKullanicilar.ExecuteReader();

            Repeater1.DataSource = dr;
            Repeater1.DataBind();
            dr.Close(); // SqlDataReader'ı kapat
        }
    }
}