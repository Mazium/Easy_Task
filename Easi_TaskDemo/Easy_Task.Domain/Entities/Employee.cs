namespace Easy_Task.Domain.Entities
{
    public class Employee: BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public decimal Salary { get; set; }
    }
}
