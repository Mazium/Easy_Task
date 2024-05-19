using Easy_Task.Domain.Entities;

namespace Easy_Task.Application.Interface.Repositories
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<List<Employee>> GetEmployeesByUserIdAsync(string userId);
    }
}
