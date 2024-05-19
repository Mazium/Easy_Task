using Easy_Task.Application.DTOs;

namespace Easy_Task.Application.Interface.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Register(RegisterDto signup, string role);
        Task<(int, string)> Login(LoginDto model);
    }
}
