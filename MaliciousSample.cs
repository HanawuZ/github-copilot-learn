using System;
using Microsoft.Data.SqlClient;
namespace MaliciousSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hardcoded password (security risk)
            string password = "SuperSecret123!";
            string username = "admin";

            // Unsafe SQL query (SQL injection risk)
            string userInput = "' OR '1'='1";
            string query = "SELECT * FROM Users WHERE username = '" + userInput + "' AND password = '" + password + "'";

            using (SqlConnection connection = new SqlConnection("Server=myServer;Database=myDB;User Id=" + username + ";Password=" + password + ";"))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["username"]);
                }
            }
        }
    }
}
