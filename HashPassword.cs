using SmartPlant.Helpers;
using System;

// Kullanım: Bu dosyayı Program.cs içine ekle veya direkt çalıştır
// dotnet run -- hash-password admin123

namespace SmartPlant
{
    public class HashPasswordTool
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "hash-password")
            {
                string password = args.Length > 1 ? args[1] : "admin123";
                string hash = PasswordHelper.HashPassword(password);
                Console.WriteLine($"Password: {password}");
                Console.WriteLine($"Hash: {hash}");
                Console.WriteLine();
                Console.WriteLine("SQL Script:");
                Console.WriteLine($"INSERT INTO admins (username, name, surname, email, password, is_deleted, created_date)");
                Console.WriteLine($"VALUES ('admin', 'Admin', 'User', 'admin@smartplant.com', '{hash}', 0, GETDATE());");
            }
        }
    }
}