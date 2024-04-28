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
    public partial class resetPass : Form
    {
        public resetPass()
        {
            InitializeComponent();
        }

        private void resetPass_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string phonenum = this.phoneNum.Text;

            if (string.IsNullOrWhiteSpace(this.phoneNum.Text))
            {
                MessageBox.Show("Please Enter Phone Number.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.phoneNum.Focus();
                return;
            }
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                string sql = "SELECT COUNT(*) from admin where adminPhoneNum = @phonenum";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@phonenum", phonenum);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    resetNow resetNow = new resetNow(phonenum);
                    resetNow.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Phone Number is incorrect. Please enter valid Phone Number", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void phoneNum_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
