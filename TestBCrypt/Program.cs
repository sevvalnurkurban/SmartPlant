using BCrypt.Net;
using System.Data.SqlClient;

// Test 1: Veritabanından hash'i al
string connectionString = @"Server=(localdb)\mssqllocaldb;Database=SmartPlantDb;Trusted_Connection=True;";
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    var command = new SqlCommand("SELECT username, password FROM admins WHERE username='admin'", connection);
    using (var reader = command.ExecuteReader())
    {
        if (reader.Read())
        {
            string username = reader.GetString(0);
            string hashFromDb = reader.GetString(1);

            Console.WriteLine($"Username: {username}");
            Console.WriteLine($"Hash from DB: {hashFromDb}");
            Console.WriteLine($"Hash length: {hashFromDb.Length}");
            Console.WriteLine();

            // Test verification
            string password = "admin123";
            Console.WriteLine($"Testing password: {password}");

            try
            {
                bool isValid = BCrypt.Net.BCrypt.Verify(password, hashFromDb);
                Console.WriteLine($"Verification result: {isValid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Admin user not found!");
        }
    }
}

Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();