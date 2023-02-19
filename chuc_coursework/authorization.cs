using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace chuc_coursework
{
    public partial class authorization : Form
    {
        public static string connStr = "server = chuc.caseum.ru;port = 33333;user = st_2_20_4;database=is_2_20_st4_KURS;password=65655604;";
        Form1 form1 = new Form1();
        //authorization authh =new authorization();
        public authorization()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        void Authoriza()
        {
            MySqlConnection conn;
            conn = new MySqlConnection(connStr);
            string sql = "SELECT * FROM User WHERE login = @un and password= @up";
            conn.Open();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            command.Parameters.Add("@up", MySqlDbType.VarChar, 25);
            command.Parameters["@un"].Value = textBox1.Text;
            command.Parameters["@up"].Value = sha256(textBox2.Text);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                Auth.auth = true;
            }
            else
            {
                MessageBox.Show("Неверные данные авторизации!");
            }
            conn.Open();
            string SelectRole = $"SELECT `post` FROM `User` WHERE `login` = \"{textBox1.Text}\"";
            MySqlCommand cmd = new MySqlCommand(SelectRole, conn);
            string Role = Convert.ToString(cmd.ExecuteScalar());
            switch (Role)
            {
                case "commodity_expert":
                    Invoke(new Action(() =>
                    {
                        this.Hide();
                        form1.Show();
                    }));
                    break;
                //case "admin":
                //    MessageBox.Show("admin Rakova");
                //    break;
                //default:
                //    conn.Close(); // висит форма 1
                //    break;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Thread avtori = new Thread(Authoriza);
            avtori.Start();
        } 

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;  
            }
            else 
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
