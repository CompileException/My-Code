using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Team_Ventic_Events.Dashboard;

namespace Team_Ventic_Events.Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            MySQLMethods.InitializeDB();
        }

        //EXIT BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Minimized Window
        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
        }

        private void noacc_Click(object sender, EventArgs e)
        {
            noacc.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            getexternalip();

            if (dbConn == null)
            {
                label2.Text = "No Connection";
                label2.ForeColor = Color.Red;
            }
            else
            {
                label2.Text = "Connection established";
                label2.ForeColor = Color.Green;
            }
        }

        //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*//
        //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*//
        //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*//
        //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*//
        //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*// 

        private MySqlConnection dbConn;

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

                if (externalip != Dns.GetHostName())
                {
                    MySQLMethods.InsertPCData(Dns.GetHostName());
                }
            }
            dbConn.Close();
        }

        private void loginmethods()
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
                String username = reader["username"].ToString();
                String password = reader["password"].ToString();

                if (username == textBox1.Text && password == textBox2.Text)
                {
                    Dashboard_cs dashboard_Cs = new Dashboard_cs();
                    this.Hide();
                    dashboard_Cs.Show();
                }
                else
                {
                    noacc.Show();
                }
            }
            dbConn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginmethods();
        }
    }
}
