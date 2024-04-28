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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Internet_Billing_System
{
    public partial class dashboardForm : Form
    {
        private string loggedInUsername;
        public dashboardForm(string username)
        {
            InitializeComponent();
            loggedInUsername = username;

        }

        private void dashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            admin admin = new admin(loggedInUsername);
            admin.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            // Pass the loggedInUsername to the LogoutHandler constructor
            LogoutHandler logoutHandler = new LogoutHandler(loggedInUsername);

            // Call the logout method from the LogoutHandler instance
            logoutHandler.Logout();

            // Close or hide the current form if needed
            this.Close(); // or this.Hide();

    }

        private void acc_btn_Click(object sender, EventArgs e)
        {
            userAccount accountForm = new userAccount(loggedInUsername);
            accountForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reportForm reportForm = new reportForm(loggedInUsername);
            reportForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            billingForm billingForm = new billingForm(loggedInUsername);
            billingForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            transactionForm transactionForm = new transactionForm(loggedInUsername);
            transactionForm.Show();
            this.Hide();
        }
    }
}
