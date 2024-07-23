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
    public partial class Urunleri_Yonet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Sayfa ilk kez yükleniyorsa
            {
                UrunleriYukle();

            }
        }
        protected void BtnUrunGuncelle_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int UrunID = Convert.ToInt32(btn.CommandArgument);

            Response.Redirect($"Urun_Guncelle.aspx?UrunID={UrunID}");

        }

        protected void BtnUrunSil_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int UrunID = Convert.ToInt32(btn.CommandArgument);

            // Kullanıcıyı veritabanından sil
            SqlCommand command = new SqlCommand("DELETE FROM Urunler WHERE UrunID=@urunID", SqlConnectionClass.connection);
            command.Parameters.AddWithValue("@urunID", UrunID);
            SqlConnectionClass.CheckConnection();
            command.ExecuteNonQuery();
            UrunleriYukle();
        }
        public void UrunleriYukle()
        {

            // Ürünleri veritabanından getirerek Datalist1'e bağla
            SqlCommand commandList = new SqlCommand("SELECT UrunID, Urun_Adi, Urun_Fiyat, Urun_Resmi, Urun_Aciklamasi, Urun_Stok_Durum FROM Urunler", SqlConnectionClass.connection);

            SqlConnectionClass.CheckConnection();

            SqlDataReader dr = commandList.ExecuteReader();

            Datalist1.DataSource = dr;
            Datalist1.DataBind();
            dr.Close(); // SqlDataReader'ı kapat

        }
    }
}