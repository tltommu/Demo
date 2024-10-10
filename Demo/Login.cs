using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Login : Form
    {
        public static string UserName = "1";
        public Login()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            SqlConnection con = Connection.GetConnection();
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *FROM [dbo].[Login] Where UserName='" + textBox1.Text + "' and Password = '" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1) {
                this.Hide();
                Main main = new Main();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid Login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear_Click(sender, e);
            }
                     
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Contact your database administrator");
        }
    }
}
