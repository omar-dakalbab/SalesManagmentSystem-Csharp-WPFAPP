using System.Data;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.utils;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            LoadDataGrids();
            InitializeComboBox();
            GetTableCounts();
        }
        private void InitializeComboBox()
        {
            typeComboBox.ItemsSource = new List<UserType>
            {
                new() { DisplayText = "Customer", Tag = 1 },
                new() { DisplayText = "Employee", Tag = 2 },
                new() { DisplayText = "Admin", Tag = 3 }
            };
        }
        private void GetTableCounts()
        {
            int usersC = RowCount("SELECT COUNT(*) FROM users");
            int productsC = RowCount("SELECT COUNT(*) FROM products");
            int salesC = RowCount("SELECT COUNT(*) FROM sales");
            float totalSales = GetTotalSales();

            customersCount.Content = $"{usersC}";
            productsCount.Content = $"{productsC}";
            salesMade.Content = $"{salesC}";
            totalRevenue.Content = $"{totalSales:F2}";
        }
        private static int RowCount(string query)
        {
            DataTable dt = DbConnection.GetQuery(query);
            return dt != null && dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        private static float GetTotalSales()
        {
            DataTable dt = DbConnection.GetQuery("SELECT SUM(totalPrice) FROM sales");
            return dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value
                ? Convert.ToSingle(dt.Rows[0][0])
                : 0.0f;
        }

        private void LoadDataGrids()
        {
            SetDataGridItemsSource(customersDatagrid, "SELECT * FROM users");
            SetDataGridItemsSource(productsDatagrid, "SELECT * FROM products");
            SetDataGridItemsSource(salesDatagrid, "SELECT * FROM sales");

            customersDatagrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;
            productsDatagrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;
            salesDatagrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;

            customersDatagrid.LoadingRow += DataGrid_LoadingRow;
        }

        private static void SetDataGridItemsSource(DataGrid dataGrid, string query)
        {
            DataTable dt = DbConnection.GetQuery(query);
            dataGrid.ItemsSource = dt?.DefaultView;
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "id" || e.Column.Header.ToString() == "password")
            {
                e.Cancel = true;
            }
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataRowView rowView = e.Row.Item as DataRowView;
            if (rowView != null)
            {
                DataRow row = rowView.Row;
                string userType = row["type"].ToString();
                row["type"] = UserTypeString(userType);
            }
        }

        private static string UserTypeString(string userType)
        {
            return userType switch
            {
                "1" => "Customer",
                "2" => "Employee",
                "3" => "Admin",
                _ => userType,
            };
        }

        private void DeleteProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (productsDatagrid.SelectedItem is DataRowView selectedRow)
            {
                string productId = selectedRow["id"].ToString();
                if (!string.IsNullOrEmpty(productId))
                {
                    DbConnection.DeleteData(productId, "products");
                    LoadDataGrids();
                }
                else
                {
                    MessageBox.Show("No product selected");
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete");
            }
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            //CALLING A STATIC CLASS
            var id = HelperMethods.GenerateUserId();

            float parsedPrice = float.Parse(price.Text);
            int parsedQuantity = int.Parse(quantity.Text);

            string productName = name.Text.ToString();
            string productDescription = description.Text.ToString();
            string productCategory = category.Text.ToString();

            string query = $"INSERT INTO products (id, name, price, description, quantity, category) VALUES" +
                 $" ('{id}','{productName}', '{parsedPrice}', '{productDescription}', '{parsedQuantity}', '{productCategory}')";

            //CALLING A STATIC CLASS
            DbConnection.SendQuery(query, "Added Successfully");
            LoadDataGrids();
        }

        private void UpdateProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (productsDatagrid.SelectedItem is DataRowView selectedRow)
            {
                float parsedPrice = float.Parse(price.Text);
                int parsedQuantity = int.Parse(quantity.Text);

                string productName = name.Text;
                string productDescription = description.Text;
                string productCategory = category.Text;

                string productId = selectedRow["id"].ToString();

                if (!string.IsNullOrEmpty(productId))
                {
                    string query = $"UPDATE products SET name = '{productName}', " +
                                   $"price = {parsedPrice}, " +
                                   $"description = '{productDescription}', " +
                                   $"quantity = {parsedQuantity}, " +
                                   $"category = '{productCategory}' " +
                                   $"WHERE id = {productId}";

                    //CALLING A STATIC CLASS
                    DbConnection.SendQuery(query, "Updated Successfully");
                    LoadDataGrids();
                }
                else
                {
                    MessageBox.Show("No product selected");
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update.");
            }
        }


        private void ProductsDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (productsDatagrid.SelectedItem is DataRowView selectedRow)
            {
                name.Text = selectedRow["name"].ToString();
                price.Text = selectedRow["price"].ToString();
                description.Text = selectedRow["description"].ToString();
                quantity.Text = selectedRow["quantity"].ToString();
                category.Text = selectedRow["category"].ToString();
            }
        }
        private void CustomersDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (customersDatagrid.SelectedItem is DataRowView selectedRow)
            {
                username.Text = selectedRow["username"].ToString();
                string userType = selectedRow["type"].ToString();

                var selectedUserType = typeComboBox.Items.Cast<UserType>().FirstOrDefault(item => item.DisplayText == userType);

                if (selectedUserType != null)
                {
                    typeComboBox.SelectedItem = selectedUserType;
                }
                else
                {
                    MessageBox.Show("User type not found in ComboBox.");
                }
            }
        }




        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (customersDatagrid.SelectedItem is DataRowView selectedRow)
            {
                string userId = selectedRow["id"].ToString();
                if (!string.IsNullOrEmpty(userId))
                {
                    //CALLING A STATIC CLASS
                    DbConnection.DeleteData(userId, "users");
                    LoadDataGrids();
                }
                else
                {
                    MessageBox.Show("No user selected or invalid user ID.");
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void UpdateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (customersDatagrid.SelectedItem is DataRowView selectedRow)
            {
                if (typeComboBox.SelectedItem is UserType selectedType)
                {
                    string userId = selectedRow["id"].ToString();
                    string usernameTxt = username.Text;

                    int typeValue = selectedType.Tag;

                    if (!string.IsNullOrEmpty(userId))
                    {
                        string query = $"UPDATE users SET username = '{usernameTxt}', " +
                                       $"type = {typeValue} WHERE id = {userId}";

                        DbConnection.SendQuery(query, "User Updated Successfully");
                        LoadDataGrids();
                    }
                    else
                    {
                        MessageBox.Show("No User selected");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid type for the user.");
                }
            }
            else
            {
                MessageBox.Show("Please select a user to update.");
            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }
    }
}
