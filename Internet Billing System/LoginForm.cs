using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Internet_Billing_System
{
    public partial class LoginForm : Form
    {
        private string loggedInUsername;
        public LoginForm()
        {
            InitializeComponent();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var resetForm = new resetPass();
            resetForm.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtpassword.Text = "";
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string myuser = this.txtusername.Text;
            string mypass = this.txtpassword.Text;

            if (string.IsNullOrWhiteSpace(this.txtusername.Text))
            {
                MessageBox.Show("Please Enter Username.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtusername.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtpassword.Text))
            {
                MessageBox.Show("Please Enter Password.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtpassword.Focus();
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
                string sql = "SELECT COUNT(*) from admin where username = @myuser and password =@mypass";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@myuser", myuser);
                cmd.Parameters.AddWithValue("@mypass", mypass);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {

                    loggedInUsername = myuser;

                    dashboardForm dashboardForm = new dashboardForm(loggedInUsername);
                    dashboardForm.Show();
                    this.Hide();

                    string updatesql = "UPDATE admin SET status = 'Active' WHERE username = @myuser";
                    cmd = new MySqlCommand(updatesql, conn);
                    cmd.Parameters.AddWithValue("@myuser", myuser);
                    cmd.ExecuteNonQuery();
                


            }else
                {
                    MessageBox.Show("Username or Password is incorrect. Please enter valid username and password", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        
    }

     
        private void createAccLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            addAccount addAccount = new addAccount();
            addAccount.Show();
            this.Hide();
        }
    }
}
