using Easy_Task.Application.Interface.Repositories;
using Easy_Task.Domain.Entities;
using Easy_Task.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Easy_Task.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly EasyTaskDbContext _context;
        public EmployeeRepository(EasyTaskDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesByUserIdAsync(string userId)
        {
            return await _context.Employees
                                 .Where(e => e.AppUserId == userId)
                                 .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
