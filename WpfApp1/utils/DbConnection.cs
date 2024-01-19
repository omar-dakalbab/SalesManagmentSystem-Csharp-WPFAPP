using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApp1.utils
{
    public static class DbConnection
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=salesSystem;Integrated Security=True";
        public static DataTable GetQuery(string query)
        {
            using SqlConnection connection = new(connectionString);
            using SqlDataAdapter adapter = new(query, connection);

            DataTable dataTable = new();
            adapter.Fill(dataTable);

            return dataTable;
        }

        public static bool IsUsernameTaken(string username)
        {
            DataTable usersTable = GetQuery($"SELECT * FROM users WHERE username = '{username}'");
            return usersTable.Rows.Count > 0;
        }

        public static bool InsertUser(string username, string password)
        {
            string hashedPassword = HelperMethods.HashPassword(password);
            int userType = 1;
            var id = HelperMethods.GenerateUserId();
            string query = $"INSERT INTO users (id, username, password, type) VALUES" +
                 $" ('{id}','{username}', '{hashedPassword}', '{userType}')";

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool SendQuery(string query, string successMessage)
        {
       
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show(successMessage);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool DeleteData(string id, string tableName)
        {
            string query = $"DELETE FROM {tableName} WHERE id = {id}";
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully");
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return false;
            }
        }
    }
}
