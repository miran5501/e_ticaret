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
    public partial class Kayit_ol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnKayitOl_Click(object sender, EventArgs e)
        {
            SqlConnectionClass.CheckConnection();

            // Kullanıcı adı kontrolü
            SqlCommand commandCheckUsername = new SqlCommand("SELECT COUNT(*) FROM Kullanicilar_Table WHERE KullaniciAdi=@pkullaniciadi", SqlConnectionClass.connection);
            commandCheckUsername.Parameters.AddWithValue("@pkullaniciadi", txtKullaniciAdi.Text);

            int userCount = (int)commandCheckUsername.ExecuteScalar();

            if (userCount > 0)
            {
                // Kullanıcı adı zaten kullanılıyorsa
                Response.Write("Bu kullanıcı adı zaten kullanılıyor.");
                return;
            }

            if (txtSifreTekrar.Text == txtSifre.Text)
            {
                SqlCommand commandRegister = new SqlCommand("INSERT INTO Kullanicilar_Table (KullaniciAdSoyad, KullaniciAdi, Sifre, E_mail, Telefon_No, KullaniciYetki) VALUES (@padsoyad, @pkullaniciadi, @psifre, @pemail, @ptelefonno, @pyetki)", SqlConnectionClass.connection);

                string newPass = Sha256Converter.ComputeSha256Hash(txtSifre.Text);

                commandRegister.Parameters.AddWithValue("@padsoyad", txtAdSoyad.Text);
                commandRegister.Parameters.AddWithValue("@pkullaniciadi", txtKullaniciAdi.Text);
                commandRegister.Parameters.AddWithValue("@psifre", newPass);
                commandRegister.Parameters.AddWithValue("@pemail", txtEmail.Text);
                commandRegister.Parameters.AddWithValue("@ptelefonno", txtTelefon.Text);
                commandRegister.Parameters.AddWithValue("@pyetki", 2);

                commandRegister.ExecuteNonQuery();

                Response.Write("Kayıt başarıyla tamamlandı.");
                Response.Redirect("GirisYap.aspx");
            }
            else
            {
                Response.Write("Şifreler aynı olmalı.");
            }
        }


    }
}