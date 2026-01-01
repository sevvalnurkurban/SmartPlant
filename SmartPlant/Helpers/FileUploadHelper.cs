namespace SmartPlant.Helpers
{
    public static class FileUploadHelper
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB

        /// <summary>
        /// Uploads a file to wwwroot/uploads folder
        /// </summary>
        public static async Task<string> UploadFileAsync(IFormFile file, string folderName = "uploads")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            // Validate file size
            if (file.Length > MaxFileSize)
                throw new ArgumentException($"File size cannot exceed {MaxFileSize / 1024 / 1024} MB");

            // Validate file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                throw new ArgumentException($"File type not allowed. Allowed types: {string.Join(", ", AllowedExtensions)}");

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{extension}";

            // Create upload directory if not exists
            var uploadPath = Path.Combine("wwwroot", folderName);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // Save file
            var filePath = Path.Combine(uploadPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for database
            return $"/{folderName}/{fileName}";
        }

        /// <summary>
        /// Deletes a file from wwwroot
        /// </summary>
        public static void DeleteFile(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return;

            try
            {
                // Remove leading slash
                relativePath = relativePath.TrimStart('/');

                var filePath = Path.Combine("wwwroot", relativePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
                // Ignore errors when deleting files
            }
        }

        /// <summary>
        /// Validates if a file is an image
        /// </summary>
        public static bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            if (file.Length > MaxFileSize)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return AllowedExtensions.Contains(extension);
        }

        /// <summary>
        /// Gets file size in human readable format
        /// </summary>
        public static string GetFileSizeString(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}
