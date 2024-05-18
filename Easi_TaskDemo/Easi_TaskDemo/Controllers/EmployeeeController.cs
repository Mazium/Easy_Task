using AutoMapper;
using Easy_Task.Application.DTOs;
using Easy_Task.Application.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Easy_Task.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        
        [HttpPost("new")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            var response = await _employeeService.CreateEmployeeAsync(createEmployeeDto);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = response.Data.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            return Ok( await _employeeService.GetEmployeeByIdAsync(id));
        }


        
        [HttpGet("Get-All")]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok( await _employeeService.GetAllEmployeesAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {                   
            return Ok( await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {           
            return Ok(await _employeeService.DeleteEmployeeAsync(id));
        }
    }
}
