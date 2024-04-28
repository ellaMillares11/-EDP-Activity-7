using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet_Billing_System
{
    public partial class resetNow : Form
    {
        private string phoneNum;
        public resetNow(string phoneNumber)
        {
            InitializeComponent();
            this.phoneNum = phoneNumber;
        }

        private void resetNow_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.newPassTtxt.Text))
            {
                MessageBox.Show("Please enter a new Password.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.newPassTtxt.Focus();
                return;
            }

            MySql.Data.MySqlClient.MySqlConnection conn=null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                string sql = "UPDATE admin SET password = @newPassword WHERE adminPhoneNum = @phoneNum";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@newPassword", this.newPassTtxt.Text);
                cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password updated successfully!");
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Password update failed. Phone number not found.");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close(); // Close the connection if not null
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

