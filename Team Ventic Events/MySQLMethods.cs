using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team_Ventic_Events
{
    class MySQLMethods
    {//database stuff
        public const String SERVER = "localhost";
        public const String DATABASE = "venticevent";
        public const String UID = "root";
        public const String PASSWORD = "";
        private static MySqlConnection dbConn;

        public static bool connected;

        // User class stuff
        public String Group { get; private set; }
        public String Username { get; private set; }
        public String Password { get; private set; }
        public String Registerdate { get; private set; }


        private MySQLMethods(String g, String u, String p, String rd)
        {
            Group = g;
            Username = u;
            Password = p;
            Registerdate = rd;
        }

        public static void InitializeDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.UserID = UID;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;

            String connString = builder.ToString();

            builder = null;

            Console.WriteLine(connString);

            dbConn = new MySqlConnection(connString);

            connected = true;

            Application.ApplicationExit += (sender, args) =>
            {
                if (dbConn != null)
                {
                    dbConn.Dispose();
                    dbConn = null;
                }
            };

            if (dbConn == null)
            {
                connected = false;
            }
        }

        public static List<MySQLMethods> GetUsers()
        {

            List<MySQLMethods> users = new List<MySQLMethods>();

            String query = "SELECT * FROM users WHERE externalip= '" + Dns.GetHostName() + "'";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String group = reader["group"].ToString();
                String username = reader["username"].ToString();
                String password = reader["password"].ToString();
                String date = reader["registerdate"].ToString();

                MySQLMethods u = new MySQLMethods(group, username, password, date);

                users.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return users;
        }

        public static void InsertPCData(String externalip)
        {
            String query = string.Format("INSERT INTO users SET externalip= '" + externalip + "'");

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
    }
}
