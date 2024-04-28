using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet_Billing_System
{

    public class LogoutHandler
    {
        private string loggedInUsername;

        public LogoutHandler(string loggedInUsername)
        {
            this.loggedInUsername = loggedInUsername;
        }

        public void Logout()
        {

            string myuser = loggedInUsername; // Get the currently logged-in username

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";


            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                string sql = "UPDATE admin SET status = 'NotActive' WHERE username = @myuser;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@myuser", myuser);

                cmd.ExecuteNonQuery();

                // For example, navigate to the login form

                LoginForm loginForm = new LoginForm();
                loginForm.Show();


            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

