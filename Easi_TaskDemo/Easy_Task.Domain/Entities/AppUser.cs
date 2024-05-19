
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Easy_Task.Domain.Entities
{
    public class AppUser: IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();


    }
}
