using System.Text;
using System.Text.RegularExpressions;

namespace SmartPlant.Helpers
{
    public static class UsernameGenerator
    {
        /// <summary>
        /// Generates a username from name and surname
        /// Example: "Ahmet Y1lmaz" -> "ahmet.yilmaz"
        /// </summary>
        public static string Generate(string name, string surname)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Name and surname cannot be empty");

            // Convert to lowercase
            var username = $"{name}.{surname}".ToLower();

            // Remove Turkish characters
            username = RemoveTurkishCharacters(username);

            // Remove special characters except dots and underscores
            username = Regex.Replace(username, @"[^a-z0-9._]", "");

            // Remove multiple dots
            username = Regex.Replace(username, @"\.{2,}", ".");

            // Trim dots from start and end
            username = username.Trim('.');

            return username;
        }

        /// <summary>
        /// Generates a unique username by appending a number if needed
        /// </summary>
        public static string GenerateUnique(string name, string surname, Func<string, bool> usernameExists)
        {
            var baseUsername = Generate(name, surname);
            var username = baseUsername;
            var counter = 1;

            while (usernameExists(username))
            {
                username = $"{baseUsername}{counter}";
                counter++;
            }

            return username;
        }

        /// <summary>
        /// Removes Turkish characters and replaces with English equivalents
        /// </summary>
        private static string RemoveTurkishCharacters(string text)
        {
            var turkishChars = new Dictionary<char, char>
            {
                { 'ç', 'c' }, { 'Ç', 'c' },
                { 'ð', 'g' }, { 'Ð', 'g' },
                { 'ý', 'i' }, { 'I', 'i' },
                { 'ö', 'o' }, { 'Ö', 'o' },
                { 'þ', 's' }, { 'Þ', 's' },
                { 'ü', 'u' }, { 'Ü', 'u' }
            };

            var sb = new StringBuilder();
            foreach (var ch in text)
            {
                sb.Append(turkishChars.ContainsKey(ch) ? turkishChars[ch] : ch);
            }

            return sb.ToString();
        }
    }
}
