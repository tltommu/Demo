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
            if (Validation())
            {
                SqlConnection con = Connection.GetConnection();
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
                ResetRecord();
            }
        }

        
        public void Loadingdata()
        {
            SqlConnection con = Connection.GetConnection();
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
            button1.Text = "Update";
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Validation()) {
                SqlConnection con = Connection.GetConnection();
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
                ResetRecord();
            }
        }
        private void ResetRecord()
        {
            button1.Text = "Add";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetRecord();
        }
        private bool Validation()
        {
            bool result = false;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "SkuNumber Required");
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox2, "Name Required");
            }
            else if (string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "Quantity Required");
            }
            else if(string.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox4, "Location Required");
            }
            else if(string.IsNullOrEmpty(textBox5.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox5, "Description Required");
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
    
}
