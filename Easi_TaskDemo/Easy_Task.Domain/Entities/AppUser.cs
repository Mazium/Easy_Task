using Easy_Task.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Easy_Task.Domain.Entities
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
       // public Role Role { get; set; }
    }
}
