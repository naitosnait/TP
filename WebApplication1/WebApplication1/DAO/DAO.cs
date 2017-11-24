using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using WebApplication1.Models;

namespace WebApplication1.DAO
{
    public class DAO
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        public SqlConnection Connection { get; set; }

        public void Connect()
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Disconnect()
        {
            Connection.Close();
        }
    }
}