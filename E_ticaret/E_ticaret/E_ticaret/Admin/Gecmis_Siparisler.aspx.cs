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
    public partial class Gecmis_Siparisler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
            {
                Siparisleri_Goruntule();
            }
        }


        public void Siparisleri_Goruntule()
        {
            // Ürünleri veritabanından getirerek Datalist1'e bağla
            SqlCommand commandSiparis = new SqlCommand(@"SELECT st.SiparislerID, st.Siparis_OlusturanID, st.Siparis_Teslim_Adresi, st.Siparis_Olusturma_Tarihi, st.Siparis_Tutari, st.Siparis_Notu, st.Siparis_Durumu, kt.KullaniciAdSoyad FROM Siparisler st INNER JOIN Kullanicilar_Table kt ON st.Siparis_OlusturanID = kt.KullaniciID WHERE st.Siparis_Durumu IN ('İptal Edildi', 'Teslim Edildi')", SqlConnectionClass.connection);

            SqlConnectionClass.CheckConnection();

            SqlDataReader dr = commandSiparis.ExecuteReader();

            Repeater1.DataSource = dr;
            Repeater1.DataBind();
            dr.Close(); // SqlDataReader'ı kapat
        }
        protected void BtnSepetDetay_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int siparisID = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"Siparis_Detay.aspx?SiparisID={siparisID}");

        }
    }
}