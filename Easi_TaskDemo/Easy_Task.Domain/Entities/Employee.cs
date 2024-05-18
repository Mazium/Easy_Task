using Microsoft.AspNetCore.Identity;

namespace Easy_Task.Domain.Entities
{
    public class Employee: BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
       
    }
}
