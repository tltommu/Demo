using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class StockReport : Form
    {
        ReportDocument cryrpt = new ReportDocument();
        public StockReport()
        {
            InitializeComponent();
        }

        DataSet dst = new DataSet();
        private void button1_Click(object sender, EventArgs e)
        {
            cryrpt.Load(@"C:\Users\Tommy Lam\source\repos\Demo\Demo\Reports\Stock.rpt");
            SqlConnection con = Connection.GetConnection();
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [SkuInfo] where Cast( Date as Date) between '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' and '" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "'", con);
            sda.Fill(dst, "SkuInfo");
            cryrpt.SetDataSource(dst);
            cryrpt.SetParameterValue("FromDate", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            cryrpt.SetParameterValue("ToDate", dateTimePicker2.Value.ToString("dd/MM/yyyy"));
            crystalReportViewer1.ReportSource = cryrpt;
            crystalReportViewer1.ReportSource = cryrpt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dst.Clear();
        }
    }
}
