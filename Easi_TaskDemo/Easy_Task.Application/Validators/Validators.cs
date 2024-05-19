using Easy_Task.Application.DTOs;
using FluentValidation;

namespace Easy_Task.Application.Validators
{
    public static class Validators
    {
        public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
        {
            public CreateEmployeeDtoValidator()
            {
                RuleFor(dto => dto.FirstName).NotEmpty().WithMessage("First name is required");
                RuleFor(dto => dto.LastName).NotEmpty().WithMessage("Last name is required");
                RuleFor(dto => dto.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email address");
                RuleFor(dto => dto.Salary).NotEmpty().WithMessage("Salary is required").GreaterThan(0).WithMessage("Salary must be a positive number");
            
                RuleFor(dto => dto.PhoneNumber)
                    .NotEmpty().WithMessage("Phone number is required")
                    .Matches(@"^\+?\d{11}(?:\-\d{4,5}\-?\d{5,6})?$").WithMessage("Invalid phone number");
            }
        }

        public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
        {
            public UpdateEmployeeDtoValidator()
            {
                RuleFor(dto => dto.FirstName).NotEmpty().WithMessage("First name is required");
                RuleFor(dto => dto.LastName).NotEmpty().WithMessage("Last name is required");
                RuleFor(dto => dto.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email address");
                RuleFor(dto => dto.Salary).NotEmpty().WithMessage("Salary is required").GreaterThan(0).WithMessage("Salary must be a positive number");

                RuleFor(dto => dto.PhoneNumber)
                    .NotEmpty().WithMessage("Phone number is required")
                    .Matches(@"^\+?\d{11}(?:\-\d{4,5}\-?\d{5,6})?$").WithMessage("Invalid phone number");
            }
        }
    }
}
