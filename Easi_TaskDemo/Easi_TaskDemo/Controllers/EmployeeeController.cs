using AutoMapper;
using Easy_Task.Application.DTOs;
using Easy_Task.Application.Interface.Services;
using Easy_Task.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Easy_Task.API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


       
        [HttpPost("create-new")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var response = await _employeeService.CreateEmployeeAsync(createEmployeeDto, User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            return CreatedAtAction(nameof(GetEmployeeById), new { id = response.Data.Id }, response);
        }
        [HttpGet("get-by-UserId/{userId}")]
        public async Task<IActionResult> GetEmployeeByUserId(string userId)
        {
            var response = await _employeeService.GetEmployeesByUserIdAsync(userId);
            if (response.Succeeded)
            {
                return Ok(response);
            }

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            return Ok( await _employeeService.GetEmployeeByIdAsync(id));
        }

        
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok( await _employeeService.GetAllEmployeesAsync());
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {                   
            return Ok( await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {           
            return Ok(await _employeeService.DeleteEmployeeAsync(id));
        }
    }
}
