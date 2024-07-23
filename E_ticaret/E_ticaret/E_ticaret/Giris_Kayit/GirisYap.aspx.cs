using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using E_ticaret.classlar;
using System.Data;

namespace E_ticaret
{
    public partial class GirisYap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGirisYap_Click(object sender, EventArgs e)
        {
            int Admin = 1;
            int Musteri = 2;

            SqlCommand commandLogin = new SqlCommand("Select KullaniciID, KullaniciAdi, Sifre, KullaniciYetki from Kullanicilar_Table where KullaniciAdi=@pkullaniciadi and Sifre=@psifre", SqlConnectionClass.connection);

            SqlConnectionClass.CheckConnection();

            string newsifre = Sha256Converter.ComputeSha256Hash(txtSifre_giris.Text);

            commandLogin.Parameters.AddWithValue("@pkullaniciadi", txtKullaniciAdi_giris.Text);
            commandLogin.Parameters.AddWithValue("@psifre", newsifre); // newsifre yaz yukarıdaki yorum satırını kaldırınca

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(commandLogin);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // KullaniciID'yi değişkene atayın
                int kullaniciID = (int)dt.Rows[0]["KullaniciID"];

                Session["KullaniciYetki"] = dt.Rows[0]["KullaniciYetki"];
                int kullaniciYetki = (int)Session["KullaniciYetki"];

                // KullaniciID'yi Session'a atayın
                Session["KullaniciID"] = kullaniciID;

                if (kullaniciYetki == Admin)
                {
                    Response.Write("Admin paneline giriş yapıldı");
                    Response.Redirect("/Admin/Admin_paneli.aspx");
                }
                else if (kullaniciYetki == Musteri)
                {
                    Response.Write("Giriş yapıldı");
                    Response.Redirect("/Musteri/Urunler.aspx");
                }
            }
            else
            {
                Response.Write("Kullanıcı adı veya şifre yanlış");
            }
        }
    }
}
