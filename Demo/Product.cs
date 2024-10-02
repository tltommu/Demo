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

namespace Demo
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            Loadingdata();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D9AGS5A\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            //insert logic
            con.Open();
            var sqlQuery = @"";

            if (IfProductExists(con, textBox1.Text))
            {
                sqlQuery = @"UPDATE [Skuinfo] SET [Name] = '" + textBox2.Text + "' ,[Quantity] = '" + textBox3.Text + "',[Location] = '" + textBox4.Text + "' ,[Description] = '" + textBox5.Text + "'WHERE [SkuNumber] = '" + textBox1.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO [dbo].[SkuInfo]([SkuNumber],[Name],[Quantity],[Location],[Description])VALUES
                                                ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
            }
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, con);
            SqlCommand cmd = sqlCommand;
            cmd.ExecuteNonQuery();
            con.Close();

            //Reading Data
            Loadingdata();
        }

        
        public void Loadingdata()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D9AGS5A\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from dbo.SkuInfo", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["SkuNumber"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Name"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Quantity"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Location"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Description"].ToString();
            }
        }

        private bool IfProductExists(SqlConnection con, string SkuNumber)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select 1 from dbo.SkuInfo WHERE [SkuNumber]= '" + SkuNumber+ "'", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D9AGS5A\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            var sqlQuery = @"";

            if (IfProductExists(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Skuinfo] WHERE [SkuNumber] = '" + textBox1.Text + "'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, con);
                SqlCommand cmd = sqlCommand;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("SkuNumber does not exist");
            }
            

            //Reading Data
            Loadingdata();
        }
    }
    
}
