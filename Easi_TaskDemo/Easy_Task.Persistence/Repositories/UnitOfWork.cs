using Easy_Task.Application.Interface.Repositories;
using Easy_Task.Persistence.Context;

namespace Easy_Task.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EasyTaskDbContext _context;
        private IEmployeeRepository _employeeRepository;

        public UnitOfWork(EasyTaskDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository ??= new EmployeeRepository(_context);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
