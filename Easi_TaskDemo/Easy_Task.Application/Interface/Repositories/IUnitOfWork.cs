namespace Easy_Task.Application.Interface.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
