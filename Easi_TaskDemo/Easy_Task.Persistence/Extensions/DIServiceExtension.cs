using Easy_Task.Application.Interface.Repositories;
using Easy_Task.Application.Interface.Services;
using Easy_Task.Application.ServiceImplementation;
using Easy_Task.Domain.Entities;
using Easy_Task.Persistence.Context;
using Easy_Task.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
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

            services
                 .AddIdentity<AppUser, IdentityRole>()
                 .AddEntityFrameworkStores<EasyTaskDbContext>()
                 .AddDefaultTokenProviders();



            // Register generic repository
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddValidatorsFromAssemblyContaining<CreateEmployeeDtoValidator>();
            services.AddScoped<IEmployeeService, EmployeeService >();
            services.AddTransient<IAuthService, AuthService>();
          
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using EasyTaskDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<EasyTaskDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
