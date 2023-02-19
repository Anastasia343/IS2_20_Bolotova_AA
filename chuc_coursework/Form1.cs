using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace chuc_coursework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            authorization auth = new authorization();
            this.Hide();
            auth.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData() 
        {
            MySqlConnection conn = new MySqlConnection(authorization.connStr);
            conn.Open();
            //MySqlDataAdapter adapter = new MySqlDataAdapter();
            string sql = "SELECT * FROM Provider ORDER BY id";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<string[]> list = new List<string[]>();
            while (reader.Read())
            {
                list.Add(new string[10]);
                list[list.Count - 1][0] = reader[0].ToString();
                list[list.Count - 1][1] = reader[1].ToString();
                list[list.Count - 1][2] = reader[2].ToString();
                list[list.Count - 1][3] = reader[3].ToString();
                list[list.Count - 1][4] = reader[4].ToString();
                list[list.Count - 1][5] = reader[5].ToString();
                list[list.Count - 1][6] = reader[6].ToString();
                list[list.Count - 1][7] = reader[7].ToString();
                list[list.Count - 1][8] = reader[8].ToString();
            }
            foreach (string[] s in list)
                dataGridView1.Rows.Add(s);
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(authorization.connStr);
                conn.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO Provider (id, name, address, telephone, score, mail, INN, OGRN, KPP) values (@id, @name, @address, @telephone, @score, @mail, @INN, @OGRN, @KPP)");
                command.Connection = conn;
                command.Parameters.AddWithValue("id", textBox1.Text);
                command.Parameters.AddWithValue("name", textBox2.Text);
                command.Parameters.AddWithValue("address", textBox3.Text);
                command.Parameters.AddWithValue("telephone", textBox4.Text);
                command.Parameters.AddWithValue("score", textBox5.Text);
                command.Parameters.AddWithValue("mail", textBox6.Text);
                command.Parameters.AddWithValue("INN", textBox7.Text);
                command.Parameters.AddWithValue("OGRN", textBox8.Text);
                command.Parameters.AddWithValue("KPP", textBox9.Text);
                command.ExecuteNonQuery();
                conn.Close();
                
            }
            catch (MySql.Data.MySqlClient.MySqlException) 
            {
                MessageBox.Show("данное id уже присутствует или поле не заполнено");
            }
            finally
            { 
                textBox1.Text = String.Empty;
            }

            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new Form1(); //?
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(authorization.connStr);
            string id = textBox11.Text;
            string MySQL = string.Format("DELETE FROM Provider WHERE (id = {0})", id);
            conn.Open();
            MySqlCommand command = new MySqlCommand(MySQL, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i=0; i < dataGridView1.Rows.Count; i++)
            {
                string test = dataGridView1[1, i].Value.ToString();
                Regex reg = new Regex($@"{textBox12.Text}", RegexOptions.IgnoreCase);
                MatchCollection matches = reg.Matches(test);
                if (matches.Count > 0)
                {
                    foreach(Match item in matches) 
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
