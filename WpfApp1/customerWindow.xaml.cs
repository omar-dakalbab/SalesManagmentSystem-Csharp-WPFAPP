using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.utils;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for customerWindow.xaml
    /// </summary>
    public partial class customerWindow : Window
    {
        private DataTable productsTable;
        private DataTable salesTable;
        public customerWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            LoadDataGrids();
        }

        private void LoadDataGrids()
        {
            productsTable = DbConnection.GetQuery("SELECT * FROM products");
            salesTable = DbConnection.GetQuery("SELECT * FROM sales");

            SetDataGridItemsSource(productsDatagrid, productsTable);
            SetDataGridItemsSource(salesDataGrid, salesTable);

            productsDatagrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;
            salesDataGrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;

            productsCombo.ItemsSource = productsTable.DefaultView;

            DataTable usersTable = DbConnection.GetQuery("SELECT username FROM users");
            usersCombo.ItemsSource = usersTable.DefaultView;
        }

        private void SetDataGridItemsSource(DataGrid dataGrid, DataTable table)
        {
            dataGrid.ItemsSource = table?.DefaultView;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "id" || e.Column.Header.ToString() == "password")
            {
                e.Cancel = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = HelperMethods.GenerateUserId();

            if (productsCombo.SelectedItem != null && usersCombo.SelectedItem != null && !string.IsNullOrEmpty(quantityText.Text))
            {
                string productName = (productsCombo.SelectedItem as DataRowView)["name"].ToString();
                string userName = (usersCombo.SelectedItem as DataRowView)["username"].ToString();
                int quantity = int.Parse(quantityText.Text);

                decimal price = GetProductPrice(productName);

                if (price > 0)
                {
                    
                    decimal totalPrice = quantity * price;

               
                    string saleQuery = $"INSERT INTO sales (id, product, customer, quantity, totalPrice) VALUES" +
                        $" ('{id}', '{productName}', '{userName}', {quantity}, {totalPrice})";

                    DbConnection.SendQuery(saleQuery, "Sale Added Successfully");
                    LoadDataGrids();
                }
                else
                {
                    MessageBox.Show("Product price not found or invalid.");
                }
            }
            else
            {
                MessageBox.Show("Please select a product, a user, and enter a quantity.");
            }
        }

        private static decimal GetProductPrice(string productName)
        {
            string query = $"SELECT price FROM products WHERE name = '{productName}'";
            DataTable priceTable = DbConnection.GetQuery(query);

            if (priceTable != null && priceTable.Rows.Count > 0 && priceTable.Rows[0]["price"] != DBNull.Value)
            {
                return Convert.ToDecimal(priceTable.Rows[0]["price"]);
            }

            return 0;
        }


        private void SalesSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = salesSearch.Text;
            DataView dv = salesTable.DefaultView;
            var filterColumn = SalesFilterCombo.Text.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                dv.RowFilter = $"{filterColumn} LIKE '%{searchText}%' OR customer LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = "";
            }
        }

        private void ProductsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = productsSearch.Text.ToString();
            DataView dv = productsTable.DefaultView;
            var filterColumn = productsFilterCombo.Text.ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                if (dv.Table.Columns.Contains(filterColumn))
                {
                    if (dv.Table.Columns[filterColumn].DataType == typeof(string))
                    {
                        dv.RowFilter = $"{filterColumn} LIKE '%{searchText}%'";
                    }
                    else
                    {
                        dv.RowFilter = $"{filterColumn} = {searchText}";
                    }
                }
                else
                {
                    MessageBox.Show($"Column '{filterColumn}' not found.");
                }
            }
            else
            {
                dv.RowFilter = "";
            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
