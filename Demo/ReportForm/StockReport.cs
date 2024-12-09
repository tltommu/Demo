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
        
        public StockReport()
        {
            InitializeComponent();
        }

        DataSet dst = new DataSet();
        Stock Myreport = new Stock();
        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = Connection.GetConnection();
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [SkuInfo] where Cast( Date as Date) between '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' and '" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "'", con);
            sda.Fill(dst, "SkuInfo");
            Myreport.SetDataSource(dst);
            Myreport.SetParameterValue("FromDate", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            Myreport.SetParameterValue("ToDate", dateTimePicker2.Value.ToString("dd/MM/yyyy"));
            crystalReportViewer1.ReportSource = Myreport;
            
            con.Close();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dst.Clear();
        }
    }
}
