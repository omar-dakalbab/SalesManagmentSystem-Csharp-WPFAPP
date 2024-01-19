using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.utils;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxType.SelectedItem != null)
            {
                int selectedType = comboBoxType.SelectedIndex + 1;

                DataTable usersTable = DbConnection.GetQuery($"SELECT * FROM users WHERE type = '{selectedType}'");

                foreach (DataRow row in usersTable.Rows)
                {
                    string username = row["username"].ToString();
                    string passwordFromDB = row["password"].ToString();

                    if (username.Equals(usernameTxt.Text) &&
                        passwordFromDB.Equals(
                            HelperMethods.HashPassword(
                                passwordTxt.Password.ToString()
                            )))
                    {
                        if(selectedType == 1)
                        {
                            customerWindow customer = new();
                            customer.Show();
                        }
                        else
                        {
                            Dashboard dashboard = new();
                            dashboard.Show();
                        }
                   
                        this.Close();
                        return;
                    }
                }

                MessageBox.Show("Invalid username or password");
            }
            else
            {
                MessageBox.Show("Please select a user type.");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Signup signup = new();
            signup.Show();
            this.Close();
        }

    }
}