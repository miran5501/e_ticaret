using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace E_ticaret.classlar
{
    public class SqlConnectionClass
    {
        public static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-SM5NVO6\\SQLEXPRESS;Initial Catalog=web_sitesi;Persist Security Info=True;User ID=sa;Password=miran0155;Encrypt=True;TrustServerCertificate=True");

        public static void CheckConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            else
            {

            }
        }
    
    
    
    
    }
    
}