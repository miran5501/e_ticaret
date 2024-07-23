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
    public partial class Siparisler : System.Web.UI.Page
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
                Response.Redirect("/Giris_Kayit/GirisYap.aspx");
            }
            if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
            {
                // Siparişleri veritabanından getirerek Repeater1'e bağla
                SqlCommand commandSiparisler = new SqlCommand(@"
                SELECT SiparislerID, Siparis_Tutari, Siparis_Olusturma_Tarihi, Siparis_Durumu, Siparis_Teslim_Adresi 
                FROM Siparisler 
                WHERE Siparis_OlusturanID = @kullaniciID", SqlConnectionClass.connection);

                commandSiparisler.Parameters.AddWithValue("@kullaniciID", Convert.ToInt32(Session["KullaniciID"]));
                SqlConnectionClass.CheckConnection();

                SqlDataReader dr = commandSiparisler.ExecuteReader();

                Repeater1.DataSource = dr;
                Repeater1.DataBind();
                dr.Close(); // SqlDataReader'ı kapat
            }

        }
    }
}