using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Main : Form
    {
        

        public Main()
        {
            InitializeComponent();
        }


        bool close = true;
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    close = false;
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void productToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Stock pro = new Stock();
            pro.MdiParent = this;
            pro.StartPosition = FormStartPosition.CenterScreen;
            pro.Show();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product stk = new Product();
            stk.MdiParent = this;
            stk.StartPosition = FormStartPosition.CenterScreen;
            stk.Show();
        }

        private void productListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm.ProductReport prod = new ReportForm.ProductReport();
            prod.MdiParent = this;
            prod.StartPosition = FormStartPosition.CenterScreen;
            prod.Show();
        }

        private void stockListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm.StockReport Stock = new ReportForm.StockReport();
            Stock.MdiParent = this;
            Stock.StartPosition = FormStartPosition.CenterScreen;
            Stock.Show();
        }
    }
}
