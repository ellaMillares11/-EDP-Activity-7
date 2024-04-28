using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace Internet_Billing_System
{
    public partial class userAccount : Form
    {
        private string loggedInUsername;
        public userAccount(string username)
        {
            InitializeComponent();
            this.loggedInUsername = username;

            LoadUserDetails();
            // Populate user details based on the logged-in username
        }
        private void LoadUserDetails()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            {
                try
                {
                    conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                    conn.Open();

                    string sql = "SELECT adminName, adminPhoneNum, adminAddress, profilePic FROM admin WHERE username = @username";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", loggedInUsername);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader["adminName"].ToString();
                            string phone = reader["adminPhoneNum"].ToString();
                            string address = reader["adminAddress"].ToString();

                            // Populate user details in form fields
                            userName.Text = name;
                            userPhone.Text = phone;
                            userAddress.Text = address;

            
                            byte[] imageData = (byte[])reader["profilePic"];
                            using (MemoryStream ms = new MemoryStream(imageData))
                           {
                                picProfile.Image = Image.FromStream(ms);
                        }

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        // Method to load user details based on the logged-in username

        public void ReloadUserDetails()
        {
            LoadUserDetails();
            this.Hide();
        }
        private void userAccount_Shown(object sender, EventArgs e)
        {
            ReloadUserDetails(); // Reload user details when the form is shown
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void userName_Click(object sender, EventArgs e)
        {

        }

        private void userPhone_Click(object sender, EventArgs e)
        {

        }

        private void userAddress_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dashboardForm dashboardForm = new dashboardForm(loggedInUsername);
            dashboardForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void logOutBtn_Click(object sender, EventArgs e)
        {

            // Pass the loggedInUsername to the LogoutHandler constructor
            LogoutHandler logoutHandler = new LogoutHandler(loggedInUsername);

            // Call the logout method from the LogoutHandler instance
            logoutHandler.Logout();

            // Close or hide the current form if needed
            this.Close(); // or this.Hide();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            updateAccount updateForm = new updateAccount(loggedInUsername, userName.Text, userPhone.Text, userAddress.Text, picProfile.Image);
            updateForm.Show();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            admin admin = new admin(loggedInUsername);
            admin.Show();
            this.Hide();
        }
    }
}
