﻿using AutoMapper;
using Easy_Task.Application.DTOs;
using Easy_Task.Application.Interface.Repositories;
using Easy_Task.Application.Interface.Services;
using Easy_Task.Domain.Entities;
using Easy_Task.Domain.ResponseSystem;
using FluentValidation;
using Microsoft.Extensions.Logging;
using static Easy_Task.Application.Validators.Validators;

namespace Easy_Task.Application.ServiceImplementation
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResponse<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                var validator = new CreateEmployeeDtoValidator();
                var validationResult = await validator.ValidateAsync(createEmployeeDto);

                if (!validationResult.IsValid)
                {
                    return ApiResponse<EmployeeDto>.Failed(validationResult.Errors.ConvertAll(x => x.ErrorMessage));
                }

                var employee = _mapper.Map<Employee>(createEmployeeDto);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                var employeeDto = _mapper.Map<EmployeeDto>(employee);
                return ApiResponse<EmployeeDto>.Success(employeeDto, "Employee created successfully", 201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the employee");
                return ApiResponse<EmployeeDto>.Failed(new List<string> { "An error occurred while creating the employee.", ex.Message });
            }
        }

        public async Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(string id)
        {
            try
            {
                var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    return ApiResponse<EmployeeDto>.Failed("Employee not found", 404, new List<string> { "Employee not found" });
                }

                var employeeDto = _mapper.Map<EmployeeDto>(employee);
                return ApiResponse<EmployeeDto>.Success(employeeDto, "Employee found", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the employee by id: {EmployeeId}", id);
                return ApiResponse<EmployeeDto>.Failed(new List<string> { "An error occurred while retrieving the employee.", ex.Message });
            }
        }


        public async Task<ApiResponse<List<EmployeeDto>>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
                var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
                return ApiResponse<List<EmployeeDto>>.Success(employeeDtos, "Employees retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all employees");
                return ApiResponse<List<EmployeeDto>>.Failed(new List<string> { "An error occurred while retrieving the employees.", ex.Message });
            }
        }

        public async Task<ApiResponse<EmployeeDto>> UpdateEmployeeAsync(string id, UpdateEmployeeDto updateEmployeeDto)
        {
            try
            {
                var validator = new UpdateEmployeeDtoValidator();
                var validationResult = await validator.ValidateAsync(updateEmployeeDto);

                if (!validationResult.IsValid)
                {
                    return ApiResponse<EmployeeDto>.Failed(validationResult.Errors.ConvertAll(x => x.ErrorMessage));
                }
                var existingEmployee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
                if (existingEmployee == null)
                {
                    return ApiResponse<EmployeeDto>.Failed("Employee not found", 404, new List<string> { "Employee not found" });
                }

                _mapper.Map(updateEmployeeDto, existingEmployee);
                _unitOfWork.EmployeeRepository.Update(existingEmployee);
                await _unitOfWork.SaveChangesAsync();

                var updatedEmployeeDto = _mapper.Map<EmployeeDto>(existingEmployee);
                return ApiResponse<EmployeeDto>.Success(updatedEmployeeDto, "Employee updated successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the employee with id: {EmployeeId}", id);
                return ApiResponse<EmployeeDto>.Failed(new List<string> { "An error occurred while updating the employee.", ex.Message });
            }
        }

        public async Task<ApiResponse> DeleteEmployeeAsync(string id)
        {
            try
            {
                var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    return ApiResponse.Failed("Employee not found", 404, new List<string> { "Employee not found" });
                }

                _unitOfWork.EmployeeRepository.DeleteAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponse.Success("Employee deleted successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the employee with id: {EmployeeId}", id);
                return ApiResponse.Failed(new List<string> { "An error occurred while deleting the employee.", ex.Message });
            }
        }
    }
}
