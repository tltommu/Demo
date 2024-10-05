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
    public partial class Stock : Form
    {
        public Stock()
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
                using (SqlConnection con = Connection.GetConnection())
                {
                    con.Open();
                    string sqlQuery = "";

                    if (IfProductUpdateName(con, textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text))
                    {
                        if (ConfirmUpdate("Name"))
                        {
                            sqlQuery = @"UPDATE [Skuinfo] SET [Name] = @Name, [Date] = @Date 
                                 WHERE [SkuNumber] = @SkuNumber AND [Quantity] = @Quantity 
                                 AND [Location] = @Location AND [Description] = @Description";
                            ExecuteUpdate(con, sqlQuery, textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text, dateTimePicker1.Value);
                        }
                    }
                    else if (IfProductUpdateQuantity(con, textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text))
                    {
                        if (ConfirmUpdate("Quantity"))
                        {
                            sqlQuery = @"UPDATE [Skuinfo] SET [Quantity] = @Quantity, [Date] = @Date 
                                 WHERE [SkuNumber] = @SkuNumber AND [Name] = @Name 
                                 AND [Location] = @Location AND [Description] = @Description";
                            ExecuteUpdate(con, sqlQuery, textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text, dateTimePicker1.Value);
                        }
                    }
                    else if (IfProductUpdateLocation(con, textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text))
                    {
                        if (ConfirmUpdate("Location"))
                        {
                            sqlQuery = @"UPDATE [Skuinfo] SET [Location] = @Location, [Date] = @Date 
                                 WHERE [SkuNumber] = @SkuNumber AND [Name] = @Name 
                                 AND [Quantity] = @Quantity AND [Description] = @Description";
                            ExecuteUpdate(con, sqlQuery, textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text, dateTimePicker1.Value);
                        }
                    }
                    else if (IfProductUpdateDescription(con, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text))
                    {
                        if (ConfirmUpdate("Description"))
                        {
                            sqlQuery = @"UPDATE [Skuinfo] SET [Description] = @Description, [Date] = @Date 
                                 WHERE [SkuNumber] = @SkuNumber AND [Name] = @Name 
                                 AND [Quantity] = @Quantity AND [Location] = @Location";
                            ExecuteUpdate(con, sqlQuery, textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text, dateTimePicker1.Value);
                        }
                    }
                    else
                    {
                        sqlQuery = @"INSERT INTO [dbo].[SkuInfo]([SkuNumber], [Name], [Quantity], [Location], [Description], [Date])
                             VALUES (@SkuNumber, @Name, @Quantity, @Location, @Description, @Date)";
                        ExecuteInsert(con, sqlQuery, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, dateTimePicker1.Value);
                    }

                    MessageBox.Show("Record saved successfully");
                    Loadingdata();
                    ResetRecord();
                }
            }
        }

        // Function to confirm the update process with the user
        private bool ConfirmUpdate(string field)
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to Update {field}?", "Message", MessageBoxButtons.YesNo);
            return dialogResult == DialogResult.Yes;
        }

        // Function to execute an update operation
        private void ExecuteUpdate(SqlConnection con, string sqlQuery, string Name, string SkuNumber, string Quantity, string Location, string Description, DateTime Date)
        {
            using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
            {
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@SkuNumber", SkuNumber);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@Location", Location);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Date", Date);

                cmd.ExecuteNonQuery();
                Loadingdata();
            }
        }

        // Function to execute an insert operation
        private void ExecuteInsert(SqlConnection con, string sqlQuery, string SkuNumber, string Name, string Quantity, string Location, string Description, DateTime Date)
        {
            using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
            {
                cmd.Parameters.AddWithValue("@SkuNumber", SkuNumber);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@Location", Location);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Date", Date);

                cmd.ExecuteNonQuery();
                Loadingdata();
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
                dataGridView1.Rows[n].Cells[0].Value = Convert.ToDateTime(item["Date"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[1].Value = item["SkuNumber"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Name"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Quantity"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Location"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Description"].ToString();
            }
            if (dataGridView1.Rows.Count > 0)
            {
                label1.Text=dataGridView1.Rows.Count.ToString();
                float totalQuantity = 0;
                for(int i = 0; i< dataGridView1.Rows.Count; ++i)
                {
                    totalQuantity += float.Parse(dataGridView1.Rows[i].Cells["Column3"].Value.ToString());
                    label6.Text = totalQuantity.ToString();
                }
            }
            else
            {
                label1.Text = "0";
                label6.Text = "0";
            }
            if (dataGridView3.Rows.Count > 0)
            {
                label10.Text = dataGridView3.Rows.Count.ToString();
                float ProductQuantity = 0;
                for (int i = 0; i < dataGridView3.Rows.Count; ++i)
                {
                    ProductQuantity += float.Parse(dataGridView3.Rows[i].Cells["Quantity"].Value.ToString());
                    label12.Text = ProductQuantity.ToString();
                }

            }
            else
            {
                label10.Text = "0";
                label12.Text = "0";
            }

        }

        private bool IfProductUpdateName(SqlConnection con, string SkuNumber, string Quantity, string Location, string Description)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select 1 from dbo.SkuInfo WHERE [SkuNumber]= '" + SkuNumber + "'AND [Quantity]='" + Quantity + "'And[Location]='" + Location + "'AND [Description]='" + Description + "'", con);
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

        private bool IfProductUpdateQuantity(SqlConnection con, string SkuNumber, string Name, string Location, string Description)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select 1 from dbo.SkuInfo WHERE [SkuNumber]= '" + SkuNumber + "'AND [Name]='" + Name + "'AND [Location]='" + Location + "'And[Description]='" + Description + "'", con);
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

        private bool IfProductUpdateLocation(SqlConnection con, string SkuNumber, string Name, string Quantity, string Description)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select 1 from dbo.SkuInfo WHERE [SkuNumber]= '" + SkuNumber + "'AND [Name]='" + Name + "'AND [Quantity]='" + Quantity + "'And[Description]='" + Description + "'", con);
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

        
        private bool IfProductUpdateDescription(SqlConnection con, string SkuNumber, string Name, string Quantity, string Location)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select 1 from dbo.SkuInfo WHERE [SkuNumber]= '" + SkuNumber + "'AND [Name]='" + Name + "'AND [Quantity]='" + Quantity + "'And[Location]='" + Location + "'", con);
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
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
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
                        sqlQuery = @"DELETE FROM [Skuinfo] WHERE [SkuNumber]= '" + textBox1.Text + "'AND [Name]='" + textBox2.Text + "'AND [Quantity]='" + textBox3.Text + "'And[Location]='" + textBox4.Text + "'And[Description]='" + textBox5.Text + "'";
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
            dateTimePicker1.Value = DateTime.Now;
            button1.Text = "Add";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox7.Clear();
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

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1!=null)
            {
                textBox1.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Focus();
            }
            else
            {
                textBox1.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1 != null)
            {
                textBox3.Focus();
            }
            else
            {
                textBox2.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1 != null)
            {
                textBox4.Focus();
            }
            else
            {
                textBox3.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1 != null)
            {
                textBox5.Focus();
            }
            else
            {
                textBox4.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1 != null)
            {
                SqlConnection con = Connection.GetConnection();
                con.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter("Select SkuNumber, Name From [SkuStatus] Where [SkuNumber] Like '" + textBox1.Text + "%'", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                dataGridView2.DataSource = dt1;
                SqlDataAdapter sda2 = new SqlDataAdapter("Select SkuNumber, Name, Quantity, Location, Description From [SkuInfo] Where [SkuNumber] Like '"+textBox1.Text+"%'", con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                dataGridView3.DataSource = dt2;
                Loadingdata();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = Connection.GetConnection())
            {
                if (IfProductUpdateQuantity(con, textBox1.Text, textBox2.Text, textBox4.Text, textBox5.Text))
                {
                    DialogResult dialogResult = MessageBox.Show($"Are you sure you want to Export?", "Message", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        con.Open();
                        string sqlQuery = @"UPDATE [Skuinfo] SET [Quantity] = @Quantity, [Date] = @Date 
                                 WHERE [SkuNumber] = @SkuNumber AND [Name] = @Name 
                                 AND [Location] = @Location AND [Description] = @Description";
                        int Stockqty = Int32.Parse(textBox3.Text) - Int32.Parse(textBox7.Text);
                        ExecuteUpdate(con, sqlQuery, textBox2.Text, textBox1.Text, Stockqty.ToString(), textBox4.Text, textBox5.Text, dateTimePicker1.Value);
                        Loadingdata();
                        ResetRecord();
                    }
                }
            }
        }

        
    }
    
}
