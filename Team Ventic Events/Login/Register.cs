using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team_Ventic_Events.Login
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            MySQLMethods.InitializeDB();

            //MySQLMethods.InsertPCData(Dns.GetHostName());
        }

        /*
         * 
         * MYSQL METHODS
         * CREATE ACCOUNT BUTTON
        */


        private MySqlConnection dbConn;

        private void createUser(String username, String email, String password)
        {
            DateTime date1 = DateTime.Now;

            String query = string.Format("INSERT INTO users(externalip, email, username, password, registerdate) VALUES ('" + Dns.GetHostName().ToString() + "', '" + email + "', '" + username + "', '" + password + "', '" + date1 + "')");

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = MySQLMethods.SERVER;
            builder.UserID = MySQLMethods.UID;
            builder.Password = MySQLMethods.PASSWORD;
            builder.Database = MySQLMethods.DATABASE;

            String connString = builder.ToString();

            dbConn = new MySqlConnection(connString);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();
            dbConn.Close();
        }

        private void getexternalip()
        {
            String query = string.Format("SELECT * FROM users WHERE externalip= '" + Dns.GetHostName() + "'");

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = MySQLMethods.SERVER;
            builder.UserID = MySQLMethods.UID;
            builder.Password = MySQLMethods.PASSWORD;
            builder.Database = MySQLMethods.DATABASE;

            String connString = builder.ToString();

            dbConn = new MySqlConnection(connString);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String externalip = reader["externalip"].ToString();

                if (externalip == Dns.GetHostName())
                {
                    button4.Show();
                }
                else
                {
                    MySQLMethods.InsertPCData(Dns.GetHostName());
                    button4.Hide();
                }

            }
            dbConn.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (usernametxt.Text != "" && emailtxt.Text != "" && pwtxt.Text != "" && repeatpwtxt.Text != "")
            {
                createUser(usernametxt.Text, emailtxt.Text, pwtxt.Text);
                Console.WriteLine("User: " + usernametxt.Text + " WITH PASSWORD: " + pwtxt.Text + " AND EMAIL: " + emailtxt.Text + " CREATED!");
                Login login = new Login();
                login.Show();
                this.Hide();

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            button4.Hide();

            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Register_Load_1(object sender, EventArgs e)
        {
            if (MySQLMethods.connected == true)
            {
                label2.Text = "Connected!";
                label2.ForeColor = Color.Green;
                button5.Hide();
                button2.Show();
            }
            else
            {
                label2.Text = "Not Connected!";
                label2.ForeColor = Color.Red;
                button5.Show();
                button2.Hide();
            }

            getexternalip();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (dbConn == null)
            {
                MySQLMethods.InitializeDB();

            }
            else
            {
                button5.Hide();
                button2.Show();
            }
        }
    }
}
