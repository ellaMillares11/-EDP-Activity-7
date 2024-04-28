using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Internet_Billing_System
{
    public partial class updateAccount : Form
    {
        private byte[] imageBytes;
        private void updateAccount_Load(object sender, EventArgs e)
        {
            
        }
        public updateAccount()
        {
            InitializeComponent();
        }

        private string name;
        private string phone;
        private string address;
        private string username;
        private Image profilePic;

        public updateAccount(string username, string name, string phone, string address, Image profilePic)
        {
            InitializeComponent();

            // Initialize form with received user details
            this.username = username;
            this.name = name;
            this.phone = phone;
            this.address = address;
            this.profilePic = profilePic;

            // Populate input boxes with user details
            accUsername.Text = username;
            accName.Text = name;
            accPhone.Text = phone;
            accAddress.Text = address;

            // Display profile picture
            pictureBox1.Image = profilePic;
        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(imagePath); // Display selected image in PictureBox
                imageBytes = File.ReadAllBytes(imagePath);
            }
        }

        private void addAccBtn_Click(object sender, EventArgs e)
        {
            string accntName = accName.Text;
            string accntPhoneNum = accPhone.Text;
            string accntAddress = accAddress.Text;
            string username = accUsername.Text;
            string password = accPassword.Text;




            if (string.IsNullOrWhiteSpace(accntName) || string.IsNullOrWhiteSpace(accntPhoneNum) || string.IsNullOrWhiteSpace(accntAddress) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields. or Upload a Picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string imagePath = openFileDialog1.FileName;

                UpdateRecordToDatabase(accntName, accntPhoneNum, accntAddress, username, password, imagePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Please Upload a Picture Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ReloadUserDetailsOnAccountForm(username);

        }
        private void UpdateRecordToDatabase(string accntName, string accntPhoneNum, string accntAddress, string username, string password, string imagePath)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                byte[] imageBytes = File.ReadAllBytes(imagePath); // Read image bytes from file

                string sql = "UPDATE admin SET adminName = @accntName, adminPhoneNum = @accntPhoneNum, adminAddress = @accntAddress, username = @username, password = @password, profilePic = @profilePic WHERE username = @username;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@accntName", accntName);
                cmd.Parameters.AddWithValue("@accntPhoneNum", accntPhoneNum);
                cmd.Parameters.AddWithValue("@accntAddress", accntAddress);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@profilePic", imageBytes); // Add image bytes as parameter

                cmd.ExecuteNonQuery();

                MessageBox.Show("Record updated successfully!");
                userAccount userAccount = new userAccount(username);
                this.Hide();
                userAccount.Show();
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn?.Close();
            }
        }
        private void ReloadUserDetailsOnAccountForm(string username)
        {
            // Get the currently active instance of the account form
            userAccount accountForm = Application.OpenForms.OfType<userAccount>().FirstOrDefault();

            if (accountForm != null)
            {
                // Reload user details on the account form
                accountForm.ReloadUserDetails();
            }
        }

        private void cancelAccBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }

    
}

