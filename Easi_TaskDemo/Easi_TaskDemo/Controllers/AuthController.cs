using Easy_Task.Application.DTOs;
using Easy_Task.Application.Interface.Services;
using Easy_Task.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easi_TaskDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                //(int status, string message) = await _authService.Register(model, UserRoles.User);
                // For Admin
                 (int status, string message) = await _authService.Register(model, UserRoles.Admin);
                if (status == 1)
                {
                    return Ok(new { model.Email, model.FirstName, model.LastName });
                }
                else
                {
                    return BadRequest(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                (int status, string message) = await _authService.Login(model);
                if (status == 1)
                {
                    return Ok(new LoginResponseDto(AccessToken: message));
                }
                else
                {
                    return BadRequest(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
    }
}
