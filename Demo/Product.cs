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

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                SqlConnection con = Connection.GetConnection();
                //insert logic
                con.Open();
                bool status = false;
                if(comboBox1.SelectedIndex == 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                var sqlQuery = @"";

                if (IfProductExists(con, textBox1.Text))
                {
                    sqlQuery = @"UPDATE [SkuStatus] SET [ProductType] = '" + textBox2.Text + "' ,[Status]='" + status + "',[Weight]='" + textBox3.Text + "',[Material]='" + textBox4.Text + "',[ElectroPlating]='" + textBox5.Text + "',[Remark]='" + textBox6.Text + "',[SalePriceBronze]='" + textBox7.Text + "',[SalePriceSilver]='" + textBox8.Text + "'WHERE [SkuNumber] = '" + textBox1.Text + "'";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[SkuStatus]([SkuNumber],[ProductType],[Status],[Weight],[Material],[Electroplating],[Remark],[SalePriceBronze],[SalePriceSilver])VALUES
                                                ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
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
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from dbo.SkuStatus", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["SkuNumber"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductType"].ToString();
                if ((bool)item["Status"])
                {
                    dataGridView1.Rows[n].Cells[8].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[8].Value = "Deactive";
                }
                dataGridView1.Rows[n].Cells[2].Value = item["Weight"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Material"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Electroplating"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Remark"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["SalePriceBronze"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = item["SalePriceSilver"].ToString();

            }
        }

        private bool IfProductExists(SqlConnection con, string SkuNumber)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select 1 from dbo.SkuStatus WHERE [SkuNumber]= '" + SkuNumber + "'", con);
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

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete", "Message", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (Validation())
                {

                    SqlConnection con = Connection.GetConnection();
                    var sqlQuery = @"";

                    if (IfProductExists(con, textBox1.Text))
                    {
                        con.Open();
                        sqlQuery = @"DELETE FROM [SkuStatus] WHERE [SkuNumber] = '" + textBox1.Text + "'";
                        SqlCommand sqlCommand = new SqlCommand(sqlQuery, con);
                        SqlCommand cmd = sqlCommand;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Record deleted successfully");
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
        }
        private void ResetRecord()
        {
            button1.Text = "Add";
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
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
                errorProvider1.SetError(textBox2, "ProductType Required");
            }
            else if (comboBox1.SelectedIndex== -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(comboBox1, "Status Required");
            }

            else
            {
                result = true;
            }
            return result;
        }

        private void dataGridView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            button1.Text = "Update";
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[8].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            Loadingdata();
        }
    }
}

