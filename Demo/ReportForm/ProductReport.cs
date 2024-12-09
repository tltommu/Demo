using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.ReportForm
{
    public partial class ProductReport : Form
    {
        
        public ProductReport()
        {
            InitializeComponent();
        }

        Product myReport1 = new Product();
        private void ProductReport_Load(object sender, EventArgs e)
        {
            
            SqlConnection con = Connection.GetConnection();
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter Sdaaa = new SqlDataAdapter("Select*From[SkuStatus]", con);
            DataTable dt = new DataTable();
            Sdaaa.Fill(dt);
            myReport1.SetDataSource(dt);
            crystalReportViewer1.ReportSource = myReport1;
        }

    }
}
