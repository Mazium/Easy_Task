using Microsoft.AspNetCore.Identity;

namespace Easy_Task.Domain.Entities
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime DateModified { get; set; }= DateTime.UtcNow;
    }
}
