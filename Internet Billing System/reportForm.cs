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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Runtime.Remoting.Messaging;

namespace Internet_Billing_System
{
    public partial class reportForm : Form
    {
        private string loggedInUsername;
        public reportForm(string username)
        {
            this.loggedInUsername = username;
            InitializeComponent();
            LoadDataIntoDataGridView();
            SetupAutoComplete();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string searchTerm = comboBoxFilter.Text.Trim(); // Trim extra whitespace from the search term

            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT * FROM `billing and transaction report` WHERE CONCAT_WS('',month,year,custName,subsName,'','',billStatus,'','',paymentMethod) LIKE @searchTerm";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%"); // Wildcard search
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the filtered data to the DataGridView
                dataGridRecord.DataSource = dataTable;
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

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

                string sql = "SELECT * FROM `billing and transaction report`";
                string sql1 = "SELECT * FROM sales_per_month_per_plan";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                DataTable dataTable1 = new DataTable();
                adapter1.Fill(dataTable1);

                dataGridRecord.DataSource = dataTable;
                dataGridSales.DataSource = dataTable1;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            comboBoxFilter.Text = "";
            LoadDataIntoDataGridView();
        }

        private void SetupAutoComplete()
        {
            // Create a list to store suggestions
            List<string> suggestions = new List<string>();

            // Populate the list with unique values from the DataGridView
            foreach (DataGridViewRow row in dataGridRecord.Rows)
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
            comboBoxFilter.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxFilter.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBoxFilter.AutoCompleteCustomSource = autoComplete;
            comboBoxSales.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxSales.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBoxSales.AutoCompleteCustomSource = autoComplete;
        }

        private void dataGridSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxSales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void previewSalesBTN_Click(object sender, EventArgs e)
        {
            string searchTerm = comboBoxSales.Text.Trim(); // Trim extra whitespace from the search term

            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT * FROM sales_per_month_per_plan WHERE CONCAT_WS('-', Year, Month) LIKE @searchTerm";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%"); // Wildcard search
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the filtered data to the DataGridView
                dataGridSales.DataSource = dataTable;
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

        private void salesCancelBTN_Click(object sender, EventArgs e)
        {
            comboBoxSales.Text = "";
            LoadDataIntoDataGridView();
        }

        private void exportBTN_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT adminName FROM admin WHERE username = @loggedInUsername";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@loggedInUsername", loggedInUsername);


                // Load Excel Template
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook templateWorkbook = excelApp.Workbooks.Open(@"C:\Users\ASUS\Documents\3rd Year Subject\2nd Sem\Event Driven Programming\template\BillingReport.xlsm");
                Excel.Worksheet templateWorksheet = templateWorkbook.Sheets[1];

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string adminName = reader["adminName"].ToString();
                    templateWorksheet.Range["D24"].Value = adminName;
                }
                else
                {
                    Console.WriteLine("Admin name not found for the provided username.");
                }

                // Determine the number of rows of data
                int numRowsOfData = dataGridRecord.Rows.Count;

                // Check if template needs additional rows
                int rowsNeeded = numRowsOfData - 8;

                try
                {
                    if (rowsNeeded > 0)
                    {
                        Excel.Range range = templateWorksheet.Rows[19];
                        range.Resize[rowsNeeded].Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    }

                    // Load Data into Excel Worksheet
                    int startRow = 11;
                    int startColumn = 3;

                    for (int row = 0; row < numRowsOfData; row++)
                    {
                        for (int col = 0; col < dataGridRecord.Columns.Count; col++)
                        {
                            // Put data starting from cell C11 and below
                            templateWorksheet.Cells[startRow + row, startColumn + col] = dataGridRecord.Rows[row].Cells[col].Value;
                        }
                    }

                    MessageBox.Show("Excel file created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during row insertion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                // Save As New File
                templateWorkbook.SaveAs(@"C:\Users\ASUS\Documents\3rd Year Subject\2nd Sem\Event Driven Programming\template\New_BillingReport_example.xlsm");

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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void exportSalesBTN_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection conn = null;
            string myConnectionString;
            myConnectionString = "server=127.0.0.2;uid=root;" +
                "pwd=ella11;database=internet_billing_system";


               try{
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();

                string sql = "SELECT adminName FROM admin WHERE username = @loggedInUsername";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@loggedInUsername", loggedInUsername);

                // Load Excel Template
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook templateWorkbook = excelApp.Workbooks.Open(@"C:\Users\ASUS\Documents\3rd Year Subject\2nd Sem\Event Driven Programming\template\SalesReport.xlsm");
                Excel.Worksheet templateWorksheet = templateWorkbook.Sheets[1];

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string adminName = reader["adminName"].ToString();
                    templateWorksheet.Range["D26"].Value = adminName;
                }
                else
                {
                    Console.WriteLine("Admin name not found for the provided username.");
                }

                // Get the selected text from the ComboBox
                string comboBoxSelectedText = comboBoxSales.Text;

                // Split the selected text into year and month
                string[] parts = comboBoxSelectedText.Split(new string[] { "-" }, StringSplitOptions.None);

                if (parts.Length == 2)
                {
                    string year = parts[0]; 
                    string month = parts[1];

                    templateWorksheet.Range["E24"].Value = year;
                    templateWorksheet.Range["K9"].Value = month;
                }
                else
                {
                    MessageBox.Show("Invalid format for selected text.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Determine the number of rows of data
                int numRowsOfData = dataGridSales.Rows.Count;

                // Check if template needs additional rows
                int rowsNeeded = numRowsOfData - 12;

                try
                {
                    if (rowsNeeded > 0)
                    {
                        Excel.Range range = templateWorksheet.Rows[22];
                        range.Resize[rowsNeeded].Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    }

                    // Load Data into Excel Worksheet

                    int startRow = 11;
                    int startColumnSubsName = 4; 
                    int startColumnTotalSales = 10; 

                    for (int row = 0; row < numRowsOfData; row++)
                    {
                        // Get the "subsName" value from the corresponding DataGridView cell
                        string subsNameValue = dataGridSales.Rows[row].Cells["subsName"].Value?.ToString();

                        // Insert "subsName" value into the merged cells range in the Excel worksheet for "Subscription Plan"
                        templateWorksheet.Range[templateWorksheet.Cells[startRow + row, startColumnSubsName],
                                                templateWorksheet.Cells[startRow + row, startColumnSubsName + 5]].Merge(); 
                        templateWorksheet.Cells[startRow + row, startColumnSubsName].Value = subsNameValue;

                        // Get the "TotalSales" value from the corresponding DataGridView cell
                        string totalSalesValue = dataGridSales.Rows[row].Cells["TotalSales"].Value?.ToString();

                        // Insert "TotalSales" value into the merged cells range in the Excel worksheet for "TotalSales/Month"
                        templateWorksheet.Range[templateWorksheet.Cells[startRow + row, startColumnTotalSales],
                                                templateWorksheet.Cells[startRow + row, startColumnTotalSales + 2]].Merge(); 
                        templateWorksheet.Cells[startRow + row, startColumnTotalSales].Value = totalSalesValue;
                    }


                    MessageBox.Show("Excel file created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during row insertion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                // Save As New File
                templateWorkbook.SaveAs(@"C:\Users\ASUS\Documents\3rd Year Subject\2nd Sem\Event Driven Programming\template\New_SalesReport_example1.xlsm");

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

        }

        private void button9_Click(object sender, EventArgs e)
        {
            dashboardForm dashboardForm = new dashboardForm(loggedInUsername);
            dashboardForm.Show();
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
