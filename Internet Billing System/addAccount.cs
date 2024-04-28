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

namespace Internet_Billing_System
{
    public partial class addAccount : Form
    {
        private byte[] imageBytes;
        public addAccount()
        {
            InitializeComponent();
        }

        private void AddRecordToDatabase(string accntName, string accntPhoneNum, string accntAddress, string username, string password, string imagePath)
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

                string sql = "INSERT INTO admin (adminName, adminPhoneNum, adminAddress, username, password, profilePic) VALUES (@accntName, @accntPhoneNum, @accntAddress, @username, @password, @profilePic)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@accntName", accntName);
                cmd.Parameters.AddWithValue("@accntPhoneNum", accntPhoneNum);
                cmd.Parameters.AddWithValue("@accntAddress", accntAddress);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@profilePic", imageBytes); // Add image bytes as parameter

                cmd.ExecuteNonQuery();

                MessageBox.Show("New record added successfully!");
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
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

            private void button9_Click(object sender, EventArgs e)
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
                string imagePath = openFileDialog1.FileName; // Get the image path

                // Call AddRecordToDatabase with imagePath as parameter
                AddRecordToDatabase(accntName, accntPhoneNum, accntAddress, username, password, imagePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Please Upload a Picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cancelAccBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
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
    }
}
