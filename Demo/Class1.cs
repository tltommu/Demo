﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public static class Connection
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString=System.Configuration.ConfigurationManager.ConnectionStrings["DBconn"].ConnectionString;
            return con;
        }
    }
}
