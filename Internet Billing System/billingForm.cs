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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Internet_Billing_System
{
    public partial class billingForm : Form
    {
        private string loggedInUsername;
        public billingForm(string username)
        {
            this.loggedInUsername = username;
            InitializeComponent();
            LoadDataIntoDataGridView();
            SetupAutoComplete();
            dataGridBills.CellClick += dataGridBills_CellClick;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridBills_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            addBTN.Enabled = false;

            // Check if a valid row is clicked
            if (e.RowIndex >= 0 && e.RowIndex < dataGridBills.Rows.Count - 1)
            {
                DataGridViewRow selectedRow = dataGridBills.Rows[e.RowIndex];

                // Populate TextBoxes with selected row's data for updating
                billsID.Text = selectedRow.Cells["billsNo"].Value.ToString();
                textBoxMonth.Text = selectedRow.Cells["month"].Value.ToString();
                textBoxYear.Text = selectedRow.Cells["year"].Value.ToString();
                textBoxCost.Text = selectedRow.Cells["billsCost"].Value.ToString();
                textBoxDue.Text = selectedRow.Cells["billsDueDate"].Value.ToString();
                textBoxStatus.Text = selectedRow.Cells["billStatus"].Value.ToString();
                textBoxCustID.Text = selectedRow.Cells["custID"].Value.ToString();

            }
        }
        private void LoadDataIntoDataGridView()
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT billsNo, month, year, billsCost, billsDueDate, billStatus, custID  FROM bills";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridBills.DataSource = dataTable;
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

        private void addBTN_Click(object sender, EventArgs e)
        {
            string month = textBoxMonth.Text;
            string year = textBoxYear.Text;
            string cost = textBoxCost.Text;
            string dueDate = textBoxDue.Text;
            string status = textBoxStatus.Text;
            string custID = textBoxCustID.Text;

            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            if (string.IsNullOrWhiteSpace(month) || string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(cost) || string.IsNullOrWhiteSpace(dueDate) || string.IsNullOrWhiteSpace(status) || string.IsNullOrWhiteSpace(custID))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();



                string sql = "INSERT INTO bills (month, year, billsCost, billsDueDate, billStatus, custID) VALUES (@month, @year, @cost, @dueDate, @status, @custID )";
                MySqlCommand cmd = new MySqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@cost", cost);
                cmd.Parameters.AddWithValue("@dueDate", dueDate);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@custID", custID);


                cmd.ExecuteNonQuery();

                MessageBox.Show("New record added successfully!");
                LoadDataIntoDataGridView();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            int newBillNo = 0;

            // Execute first query to get the last inserted bill number
            string query = "SELECT LAST_INSERT_ID() AS NewBillNo FROM bills;";
            MySqlCommand command = new MySqlCommand(query, conn);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                newBillNo = Convert.ToInt32(reader["NewBillNo"]);
            }
            reader.Close();

            // Close the first reader before executing the second query
            reader.Close();

            // Execute second query to retrieve customer and subscription data
            query = "SELECT c.custID, c.custName, c.custAddress, s.subsName, s.subsCost FROM customer c " +
                    "INNER JOIN bills b ON c.custID = b.custID " +
                    "INNER JOIN subscription s ON c.subsID = s.subsID " +
                    "WHERE b.billsNo = @billsNo;";
            command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@billsNo", newBillNo);

            reader = command.ExecuteReader(); // Use a new reader for the second query

            if (reader.Read())
            {
                string custID1 = reader["custID"].ToString();
                string custName = reader["custName"].ToString();
                string custAddress = reader["custAddress"].ToString();
                string subsName = reader["subsName"].ToString();
                string subsCost = reader["subsCost"].ToString();

                // Close the second reader before executing the admin query
                reader.Close();

                // Execute admin query to retrieve admin name
                string adminQuery = "SELECT adminName FROM admin WHERE username = @loggedInUsername";
                MySqlCommand cmdAdmin = new MySqlCommand(adminQuery, conn);
                cmdAdmin.Parameters.AddWithValue("@loggedInUsername", loggedInUsername);

                MySqlDataReader adminReader = cmdAdmin.ExecuteReader();
                if (adminReader.Read())
                {
                    string adminName = adminReader["adminName"].ToString();

                    // Load Excel Template
                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook templateWorkbook = excelApp.Workbooks.Open(@"C:\Users\ASUS\Documents\3rd Year Subject\2nd Sem\Event Driven Programming\template\Receipt.xlsx");
                    Excel.Worksheet templateWorksheet = templateWorkbook.Sheets[1];

                    // Get the selected text from the ComboBox
                    string txtCustID = textBoxCustID.Text;
                    string txtMonth = textBoxMonth.Text;
                    string txtCost = textBoxCost.Text;
                    string txtDue = textBoxDue.Text;

                    templateWorksheet.Range["F8"].Value = txtCustID;
                    templateWorksheet.Range["L10"].Value = txtMonth;
                    templateWorksheet.Range["J19"].Value = txtCost;
                    templateWorksheet.Range["L23"].Value = txtDue;
                    templateWorksheet.Range["L9"].Value = newBillNo;
                    templateWorksheet.Range["F9"].Value = custName;
                    templateWorksheet.Range["F10"].Value = custAddress;
                    templateWorksheet.Range["D15"].Value = subsName;
                    templateWorksheet.Range["J15"].Value = subsCost;
                    templateWorksheet.Range["D26"].Value = adminName;
                    templateWorksheet.Range["I26"].Value = custName;

                    // Save As New File
                    templateWorkbook.SaveAs(@"C:\Users\ASUS\Documents\3rd Year Subject\2nd Sem\Event Driven Programming\template\New_Receipt_example2.xlsx");
                    MessageBox.Show("Receipt Successfully Generated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Close Excel Application
                    templateWorkbook.Close();
                    excelApp.Quit();

                    // Release COM objects to avoid memory leaks
                    Marshal.ReleaseComObject(templateWorksheet);
                    Marshal.ReleaseComObject(templateWorkbook);
                    Marshal.ReleaseComObject(excelApp);
                }
                adminReader.Close();
            }

            reader.Close(); // Close the second reader

            conn?.Close();
            clearTextBox();



        }
        private void clearTextBox()
        {
            billsID.Text = "";
            textBoxMonth.Text = "";
            textBoxYear.Text = "";
            textBoxCost.Text = "";
            textBoxDue.Text = "";
            textBoxStatus.Text = "";
            textBoxCustID.Text = "";
        }

        private void cancelBTN_Click(object sender, EventArgs e)
        {
            clearTextBox();
            addBTN.Enabled = true;
        }

        private void editBTN_Click(object sender, EventArgs e)
        {
            string billsNo = billsID.Text;
            string month = textBoxMonth.Text;
            string year = textBoxYear.Text;
            string cost = textBoxCost.Text;
            string dueDate = textBoxDue.Text;
            string status = textBoxStatus.Text;
            string custID = textBoxCustID.Text;

            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            if (string.IsNullOrWhiteSpace(month) || string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(cost) || string.IsNullOrWhiteSpace(dueDate) || string.IsNullOrWhiteSpace(status) || string.IsNullOrWhiteSpace(custID))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "UPDATE bills SET month = @month, year = @year, billsCost = @cost, billsDueDate = @dueDate, billStatus = @status, custID = @custID WHERE billsNo = @billsNo";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@billsNo", billsNo);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@cost", cost);
                cmd.Parameters.AddWithValue("@dueDate", dueDate);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@custID", custID);


                cmd.ExecuteNonQuery();

                MessageBox.Show("Record updated successfully!");
                LoadDataIntoDataGridView();
                clearTextBox();
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

        private void clearSrchBtn_Click(object sender, EventArgs e)
        {
            searchBills.Text = "";
            LoadDataIntoDataGridView();
        }
        private void SetupAutoComplete()
        {
            // Create a list to store suggestions
            List<string> suggestions = new List<string>();

            // Populate the list with unique values from the DataGridView
            foreach (DataGridViewRow row in dataGridBills.Rows)
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
            searchBills.AutoCompleteMode = AutoCompleteMode.Suggest;
            searchBills.AutoCompleteSource = AutoCompleteSource.CustomSource;
            searchBills.AutoCompleteCustomSource = autoComplete;
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchBills.Text.Trim(); // Trim extra whitespace from the search term

            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT billsNo, month, year, billsCost, billsDueDate, billStatus, custID  FROM bills WHERE CONCAT_WS(billsNo, month, year, billsCost, billsDueDate, billStatus, custID) LIKE @searchTerm";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%"); // Wildcard search
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the filtered data to the DataGridView
                dataGridBills.DataSource = dataTable;
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

        private void searchBills_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupAutoComplete();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dashboardForm dashboardForm = new dashboardForm(loggedInUsername);
            dashboardForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reportForm reportForm = new reportForm(loggedInUsername);
            reportForm.Show();
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
