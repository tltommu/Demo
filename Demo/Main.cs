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

      

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void productToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Product pro = new Product();
            pro.MdiParent = this;
            pro.Show();
        }
    }
}
