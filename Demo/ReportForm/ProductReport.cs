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
        ReportDocument cryrpt = new ReportDocument();
        public ProductReport()
        {
            InitializeComponent();
        }

        private void ProductReport_Load(object sender, EventArgs e)
        {
            cryrpt.Load(@"C:\Users\Tommy Lam\source\repos\Demo\Demo\Reports\Product.rpt");
            SqlConnection con = Connection.GetConnection();
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter Sdaaa = new SqlDataAdapter("Select*From[SkuStatus]", con);
            DataTable dt = new DataTable();
            Sdaaa.Fill(dt);
            cryrpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = cryrpt;
        }

        
    }
}
