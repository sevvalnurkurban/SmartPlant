namespace SmartPlant.Models.ViewModels
{
    public class UserAdminViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "User" veya "Admin"
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int PlantCount { get; set; }
    }
}
