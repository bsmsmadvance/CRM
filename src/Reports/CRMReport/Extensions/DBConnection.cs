using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMReport.Extensions
{
    public class DBConnection
    {
        public string dbConnection = ConfigurationManager.AppSettings["DBConnection"];
        public string dbUsername = ConfigurationManager.AppSettings["DBUsername"];
        public string dbPassword = ConfigurationManager.AppSettings["DBPassword"];

    }
}