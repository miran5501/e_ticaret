using E_ticaret.classlar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_ticaret.Admin
{
    public partial class Siparis_Detay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
            {
                int siparisID = Convert.ToInt32(Request.QueryString["SiparisID"]);
                SiparisUrunler(siparisID);
                ButonDurum(siparisID);
                Labellar();
            }

        }
        private void SiparisUrunler(int siparisID)
        {
            SqlCommand commandSiparisDetaylari = new SqlCommand("SELECT * FROM Siparis_Urunler_Table INNER JOIN Urunler ON Siparis_urunID=UrunID WHERE SiparisID=@siparisID", SqlConnectionClass.connection);
            SqlConnectionClass.CheckConnection();
            commandSiparisDetaylari.Parameters.AddWithValue("@siparisID", siparisID);
            SqlDataReader dr = commandSiparisDetaylari.ExecuteReader();

            Repeater1.DataSource = dr;
            Repeater1.DataBind();
            dr.Close();
        }
        private void ButonDurum(int siparisID)
        {
            SqlCommand commandSiparis = new SqlCommand("SELECT Siparis_Durumu FROM Siparisler WHERE SiparislerID=@siparisID", SqlConnectionClass.connection);
            SqlConnectionClass.CheckConnection();
            commandSiparis.Parameters.AddWithValue("@siparisID", siparisID);
            SqlDataReader reader = commandSiparis.ExecuteReader();
            reader.Read();
            String siparisDurumu = reader["Siparis_Durumu"].ToString();
            if ("Onaylandı" == siparisDurumu)
            {
                btnSepetOnayla.Visible = false;
            }
            else if("Bekliyor"==siparisDurumu)
            {
                btnTeslimEdildi.Visible = false;
            }
            else
            {
                btnSepetOnayla.Visible = false;
                btnTeslimEdildi.Visible = false;
                btnSepetIptal.Visible = false;
            }
            reader.Close();


        }
        protected void BtnSepetOnayla_Click(object sender, EventArgs e)
        {
            int siparisID = Convert.ToInt32(Request.QueryString["SiparisID"]);
            SiparisDurum("Onaylandı", siparisID);
            ButonDurum(siparisID);
            Labellar();
        }

        protected void BtnSepetIptal_Click(object sender, EventArgs e)
        {
            int siparisID = Convert.ToInt32(Request.QueryString["SiparisID"]);
            SiparisDurum("İptal Edildi", siparisID);
            ButonDurum(siparisID);
            Labellar();
        }
        protected void BtnTeslimEdildi_Click(object sender, EventArgs e)
        {
            int siparisID = Convert.ToInt32(Request.QueryString["SiparisID"]);
            SiparisDurum("Teslim Edildi", siparisID);
            ButonDurum(siparisID);
            Labellar();
        }

        private void SiparisDurum(String SiparisDurumu, int siparisID)
        {
            SqlCommand commandSiparis = new SqlCommand("UPDATE Siparisler SET Siparis_Durumu=@yeniSiparisDurumu WHERE SiparislerID=@siparisID", SqlConnectionClass.connection);
            SqlConnectionClass.CheckConnection();
            commandSiparis.Parameters.AddWithValue("@siparisID", siparisID);
            commandSiparis.Parameters.AddWithValue("@yeniSiparisDurumu", SiparisDurumu);
            commandSiparis.ExecuteNonQuery();

        }
        private void Labellar()
        {
            int siparisID = Convert.ToInt32(Request.QueryString["SiparisID"]);

            string query = @"SELECT s.Siparis_Olusturma_Tarihi, s.Siparis_Durumu, s.Siparis_Tutari, s.Siparis_Teslim_Adresi, s.Siparis_Notu, 
                             k.KullaniciAdSoyad
                      FROM Siparisler s
                      INNER JOIN Kullanicilar_Table k ON s.Siparis_OlusturanID = k.KullaniciID
                      WHERE s.SiparislerID = @siparisID";

            SqlCommand command = new SqlCommand(query, SqlConnectionClass.connection);
            command.Parameters.AddWithValue("@siparisID", siparisID);

            SqlConnectionClass.CheckConnection();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                lblSiparisVeren.Text = "<strong>Sipariş Veren:</strong> " + reader["KullaniciAdSoyad"].ToString();
                lblSiparisTarihi.Text = "<strong>Sipariş Tarihi:</strong> " + reader["Siparis_Olusturma_Tarihi"].ToString();
                lblSiparisTeslimDurumu.Text = "<strong>Sipariş Durumu:</strong> " + reader["Siparis_Durumu"].ToString();
                lblSiparisTutari.Text = "<strong>Sipariş Tutarı:</strong> " + reader["Siparis_Tutari"].ToString();
                lblSiparisTeslimAdresi.Text = "<strong>Sipariş Teslim Adresi:</strong> " + reader["Siparis_Teslim_Adresi"].ToString();
                lblSiparisNotu.Text = "<strong>Sipariş Notu:</strong> " + reader["Siparis_Notu"].ToString();
            }

            reader.Close();
        }

    }
}