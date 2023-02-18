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
            //Тут происходит криптографическая магия. Смысл данного метода заключается в том, что строка залетает в метод
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
        
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            
            MySqlConnection conn;
            conn = new MySqlConnection(connStr);
            //string login = LoginUser.Text;
            //string password  = PasswordUser.Text;

            //MySqlCommand command = new MySqlCommand("SELECT * FROM `user`WHERE`login = @LU AND `password = `@PU ` ");

            //command.Parameters.Add("@LU", MySqlDbType.VarChar).Value = LoginUser;
            //command.Parameters.Add("@PU", MySqlDbType.VarChar).Value = PasswordUser;

            //adapter.SelectCommand = command;

            //Запрос в БД на предмет того, если ли строка с подходящим логином и паролем
            string sql = "SELECT * FROM User WHERE login = @un and password= @up";
            //Открытие соединения
            conn.Open();
            //Объявляем таблицу
            DataTable table = new DataTable();
            //Объявляем адаптер
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            //Объявляем команду
            MySqlCommand command = new MySqlCommand(sql, conn);
            //Определяем параметры
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            command.Parameters.Add("@up", MySqlDbType.VarChar, 25);
            //Присваиваем параметрам значение
            command.Parameters["@un"].Value = textBox1.Text;
            command.Parameters["@up"].Value = sha256(textBox2.Text);
            //Заносим команду в адаптер
            adapter.SelectCommand = command;
            //Заполняем таблицу
            adapter.Fill(table);
            //Закрываем соединение
            conn.Close();
            //Если вернулась больше 0 строк, значит такой пользователь существует
            if (table.Rows.Count > 0)
            {
                //Присваеваем глобальный признак авторизации
                Auth.auth = true;
                //добавляем метод,который будет открывать разные формы в зависимости от пользователя
                
                ////Достаем данные пользователя в случае успеха
                ////GetUserInfo(textBox1.Text);
                ////Закрываем форму
                //this.Hide();
                //store gg = new store();
                //gg.Show();
            }
            else
            {
                //Отобразить сообщение о том, что авторизаия неуспешна
                MessageBox.Show("Неверные данные авторизации!");
            }
            conn.Open();
            string SelectRole = $"SELECT `post` FROM `User` WHERE `login` = \"{textBox1.Text}\"";
            MySqlCommand cmd = new MySqlCommand(SelectRole, conn);
            string Role = Convert.ToString(cmd.ExecuteScalar());
            switch (Role)
            {
                case "commodity_expert":
                    this.Hide();
                    form1.Show();
                    break;
                case "admin":
                    MessageBox.Show("admin Rakova");
                    break;
                default:
                    conn.Close(); // висит форма 1
                    break;
            }

        } //возможно можно добавить админа и он будет удалять 
        // добавить обработчик ошибок чтоб при добавлении в таблицу если 9 есть аидишник и чел решит добавить с 9 еще то программа не будет хлопатся а просто будет вывоть в меседжбокс что ты ебалай и не должен добавлять тоже самое;)
        // добавить поточность в авторизации
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
