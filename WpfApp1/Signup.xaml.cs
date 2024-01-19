using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new();
            login.Show();
            this.Close();
        }

        private void SignupBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTxt.Text;
            string password = passwordTxt.Password.ToString();
            string passwordConfirm = passwordTxt_Confirm.Password.ToString(); 
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
              if(password == passwordConfirm)
                {
                    if (!DbConnection.IsUsernameTaken(username))
                    {
                        bool success = DbConnection.InsertUser(username, password);
                        if (success)
                        {
                            MessageBox.Show("Registration successful!");
                        }
                        else
                        {
                            MessageBox.Show("Registration failed. Please try again.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username already exists. Please choose another.");
                    }
                }
                else
                {
                    MessageBox.Show("Please confirm your password");
                }
            }
            else
            {
                MessageBox.Show("Please enter both username and password.");
            }
        }
    }
}
