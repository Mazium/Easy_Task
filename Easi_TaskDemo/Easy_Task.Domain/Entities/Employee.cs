using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Easy_Task.Domain.Entities
{
    public class Employee: BaseEntity
    {
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
    }

}

