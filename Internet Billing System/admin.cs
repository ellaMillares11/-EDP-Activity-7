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
    public partial class admin : Form
    {
        private string loggedInUsername;
        public admin(string username)
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
            SetupAutoComplete();
            this.loggedInUsername = username;

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dashboardForm dashboardForm = new dashboardForm(null);
            dashboardForm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
         private void LoadDataIntoDataGridView()
        {
            MySql.Data.MySqlClient.MySqlConnection conn=null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            try
                {
                    conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                    conn.Open();

                    string sql = "SELECT status, adminID, adminName, adminPhoneNum, adminAddress, profilePic  FROM admin";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (MySqlException ex)
                {
                 MessageBox.Show("Error: " + ex.Message);
            }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

        private void addBtn_Click(object sender, EventArgs e)
        {

        }

        private void updateBtn_Click(object sender, EventArgs e)
        {

        }

        private void SetupAutoComplete()
        {
            // Create a list to store suggestions
            List<string> suggestions = new List<string>();

            // Populate the list with unique values from the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = cell.Value?.ToString();
                    if (!string.IsNullOrEmpty(value) && !suggestions.Contains(value))
                    {
                        suggestions.Add(value);
                    }
                }
            }

            // Configure AutoComplete mode and add suggestions to the TextBox
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(suggestions.ToArray());
            searchAdmin.AutoCompleteMode = AutoCompleteMode.Suggest;
            searchAdmin.AutoCompleteSource = AutoCompleteSource.CustomSource;
            searchAdmin.AutoCompleteCustomSource = autoComplete;
        }

        private void searchAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupAutoComplete();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchAdmin.Text.Trim(); // Trim extra whitespace from the search term

            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT status, adminID, adminName, adminPhoneNum, adminAddress, profilePic FROM admin WHERE CONCAT_WS('','','', adminName, adminPhoneNum, adminAddress, '') LIKE @searchTerm";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%"); // Wildcard search
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the filtered data to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void clearSrchBtn_Click(object sender, EventArgs e)
        {
            searchAdmin.Text = "";
            LoadDataIntoDataGridView();
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
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "status")
            {
                string status = Convert.ToString(e.Value);
                if (status == "Active")
                {
                    e.CellStyle.BackColor = Color.Green; // Set color to green for active accounts
                }
                else if (status == "NotActive")
                {
                    e.CellStyle.BackColor = Color.Red; // Set color to red for inactive accounts
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            admin admin = new admin(loggedInUsername);
            
        }
    }

}
