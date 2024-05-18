using Easy_Task.Application.DTOs;
using Easy_Task.Domain.ResponseSystem;

namespace Easy_Task.Application.Interface.Services
{
    public interface IEmployeeService
    {
        Task<ApiResponse> DeleteEmployeeAsync(string id);
        Task<ApiResponse<EmployeeDto>> UpdateEmployeeAsync(string id, UpdateEmployeeDto updateEmployeeDto);
        Task<ApiResponse<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(string id);
        Task<ApiResponse<List<EmployeeDto>>> GetAllEmployeesAsync();
       

    }
}
