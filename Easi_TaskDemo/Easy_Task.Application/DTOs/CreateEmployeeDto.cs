namespace Easy_Task.Application.DTOs
{
    public class CreateEmployeeDto
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
    }
}
