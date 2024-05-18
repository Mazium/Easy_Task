using Easy_Task.Application.Interface.Repositories;
using Easy_Task.Application.Interface.Services;
using Easy_Task.Application.ServiceImplementation;
using Easy_Task.Domain.Entities;
using Easy_Task.Persistence.Context;
using Easy_Task.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using static Easy_Task.Application.Validators.Validators;

namespace Easy_Task.Persistence.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<EasyTaskDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Support string to enum conversions
            //services.AddControllers().AddJsonOptions(opt =>
            //{
            //    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //});


            // Specify identity requirements
            //services.AddIdentity<AppUser, IdentityRole>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = false;
            //    options.User.RequireUniqueEmail = true;
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //})
            //   .AddEntityFrameworkStores<EasyTaskDbContext>()
            //   .AddDefaultTokenProviders();


            // Register generic repository
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddValidatorsFromAssemblyContaining<CreateEmployeeDtoValidator>();
            services.AddScoped<IEmployeeService, EmployeeService >();
           // services.AddScoped<TokenService, TokenService >();
            services.AddScoped<IAuthService, AuthService >();

        }
    }
}
