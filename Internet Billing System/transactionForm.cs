using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Excel = Microsoft.Office.Interop.Excel;


namespace Internet_Billing_System
{
    public partial class transactionForm : Form
    {
        private string loggedInUsername;
        public transactionForm(string username)
        {
            this.loggedInUsername = username;
            InitializeComponent();
            LoadDataIntoDataGridView();
            SetupAutoComplete();
            dataGridTrans.CellClick += dataGridTrans_CellClick;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void dataGridTrans_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid row is clicked
            if (e.RowIndex >= 0 && e.RowIndex < dataGridTrans.Rows.Count - 1)
            {
                DataGridViewRow selectedRow = dataGridTrans.Rows[e.RowIndex];

                // Populate TextBoxes with selected row's data for updating

                TransID.Text = selectedRow.Cells["transactID"].Value.ToString();
                textBoxDate.Text = selectedRow.Cells["dateIssue"].Value.ToString();
                textBoxAmount.Text = selectedRow.Cells["paidAmount"].Value.ToString();
                textBoxPayment.Text = selectedRow.Cells["paymentMethod"].Value.ToString();
                textBoxBillsNo.Text = selectedRow.Cells["billsNo"].Value.ToString();

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

                string sql = "SELECT transactID, dateIssue, paidAmount, paymentMethod, billsNo  FROM transaction";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridTrans.DataSource = dataTable;
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
            string dateIssue = textBoxDate.Text;
            string paidAmount = textBoxAmount.Text;
            string paymentMethod = textBoxPayment.Text;
            string billsNo = textBoxBillsNo.Text;

            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            if (string.IsNullOrWhiteSpace(dateIssue) || string.IsNullOrWhiteSpace(paidAmount) || string.IsNullOrWhiteSpace(paymentMethod) || string.IsNullOrWhiteSpace(billsNo))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();



                string sql = "INSERT INTO transaction (dateIssue, paidAmount, paymentMethod, billsNo) VALUES (@dateIssue, @paidAmount, @paymentMethod, @billsNo)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@dateIssue", dateIssue);
                cmd.Parameters.AddWithValue("@paidAmount", paidAmount);
                cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                cmd.Parameters.AddWithValue("@billsNo", billsNo);



                cmd.ExecuteNonQuery();

                MessageBox.Show("New record added successfully!");
                LoadDataIntoDataGridView();
              
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            int newTransNo = 0;

            // Execute first query to get the last inserted bill number
            string query = "SELECT LAST_INSERT_ID() AS newTransNo FROM transaction;";
            MySqlCommand command = new MySqlCommand(query, conn);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                newTransNo = Convert.ToInt32(reader["newTransNo"]);
            }
            reader.Close();

            // Execute second query to retrieve customer and subscription data
            query = "SELECT c.custID, c.custName, c.custAddress, s.subsName, s.subsCost, b.month " +
                    "FROM customer c " +
                    "INNER JOIN bills b ON c.custID = b.custID " +
                    "INNER JOIN transaction t ON b.billsNo = t.billsNo " +
                    "INNER JOIN subscription s ON c.subsID = s.subsID " +
                    "WHERE t.billsNo = @billsNo;";

            command = new MySqlCommand(query, conn);
            string txtBillNo = textBoxBillsNo.Text;
            command.Parameters.AddWithValue("@billsNo", txtBillNo);

            reader = command.ExecuteReader(); // Use a new reader for the second query

            if (reader.Read())
            {
                string custID1 = reader["custID"].ToString();
                string custName = reader["custName"].ToString();
                string custAddress = reader["custAddress"].ToString();
                string subsName = reader["subsName"].ToString();
                string subsCost = reader["subsCost"].ToString();
                string month = reader["month"].ToString();

                reader.Close();

                // Execute admin query to retrieve admin name
                string adminQuery = "SELECT adminName FROM admin WHERE username = @loggedInUsername";
                MySqlCommand cmdAdmin = new MySqlCommand(adminQuery, conn);
                cmdAdmin.Parameters.AddWithValue("@loggedInUsername", loggedInUsername);

                MySqlDataReader adminReader = cmdAdmin.ExecuteReader();
                if (adminReader.Read())
                {
                    string adminName = adminReader["adminName"].ToString();
                    try
                    {
                        // Load Excel Template
                        Excel.Application excelApp = new Excel.Application();
                        Excel.Workbook templateWorkbook = excelApp.Workbooks.Open(@"C:\Users\ASUS\Documents\3rd Year Subject\2nd Sem\Event Driven Programming\template\New_Receipt_example2.xlsx");
                        Excel.Worksheet templateWorksheet = templateWorkbook.Sheets[2];

                        // Get the selected text from the ComboBox
                        string txtDate = textBoxDate.Text;
                        string txtAmount = textBoxAmount.Text;

                        templateWorksheet.Range["F7"].Value = custID1;
                        templateWorksheet.Range["L22"].Value = txtDate;
                        templateWorksheet.Range["J19"].Value = txtAmount;
                        templateWorksheet.Range["L8"].Value = newTransNo;
                        templateWorksheet.Range["L9"].Value = month;
                        templateWorksheet.Range["F8"].Value = custName;
                        templateWorksheet.Range["F9"].Value = custAddress;
                        templateWorksheet.Range["D14"].Value = subsName;
                        templateWorksheet.Range["J14"].Value = subsCost;
                        templateWorksheet.Range["J18"].Value = subsCost;
                        templateWorksheet.Range["D25"].Value = adminName;


                        templateWorkbook.Save();
                        MessageBox.Show("Receipt Successfully Generated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Close Excel Application
                        templateWorkbook.Close();
                        excelApp.Quit();

                        // Release COM objects to avoid memory leaks
                        Marshal.ReleaseComObject(templateWorksheet);
                        Marshal.ReleaseComObject(templateWorkbook);
                        Marshal.ReleaseComObject(excelApp);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn?.Close();
                    }
                }
                adminReader.Close();
                clearTextBox();
            }
            reader.Close();
        }
        private void clearTextBox()
        {          
            textBoxDate.Text = "";
            textBoxAmount.Text = "";
            textBoxPayment.Text = "";
            textBoxBillsNo.Text = "";
            TransID.Text = "";
        }

        private void cancelBTN_Click(object sender, EventArgs e)
        {
            clearTextBox();
        }

        private void dataGridTrans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void SetupAutoComplete()
        {
            // Create a list to store suggestions
            List<string> suggestions = new List<string>();

            // Populate the list with unique values from the DataGridView
            foreach (DataGridViewRow row in dataGridTrans.Rows)
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
            searchTrans.AutoCompleteMode = AutoCompleteMode.Suggest;
            searchTrans.AutoCompleteSource = AutoCompleteSource.CustomSource;
            searchTrans.AutoCompleteCustomSource = autoComplete;
        }
        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTrans.Text.Trim(); // Trim extra whitespace from the search term

            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT transactID, dateIssue, paidAmount, paymentMethod, billsNo FROM transaction WHERE CONCAT_WS(transactID, dateIssue, paidAmount, paymentMethod, billsNo) LIKE @searchTerm";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%"); // Wildcard search
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the filtered data to the DataGridView
                dataGridTrans.DataSource = dataTable;
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
            searchTrans.Text = "";
            LoadDataIntoDataGridView();
        }

        private void deleteBTN_Click(object sender, EventArgs e)
        {
            string transID = TransID.Text;

            MessageBox.Show("Are you sure you want to delete this transaction?", "Warning", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                if (MessageBox.Show("Are you sure you want to delete this transaction?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                    conn.Open();

                    string sql = "DELETE FROM transaction WHERE transactID = @transID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@transID", transID);

                    cmd.ExecuteNonQuery();
                    LoadDataIntoDataGridView();
                    clearTextBox();
                }

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

        private void button3_Click(object sender, EventArgs e)
        {
            billingForm billingForm = new billingForm(loggedInUsername);
            billingForm.Show();
            this.Hide();
        }
    }
}
