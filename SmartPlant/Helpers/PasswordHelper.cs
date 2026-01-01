namespace SmartPlant.Helpers
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Hashes a password using BCrypt
        /// </summary>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies a password against a hash
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates password strength (min 6 characters)
        /// </summary>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return password.Length >= 6;
        }

        /// <summary>
        /// Generates a random password reset token
        /// </summary>
        public static string GenerateResetToken()
        {
            return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        }
    }
}
