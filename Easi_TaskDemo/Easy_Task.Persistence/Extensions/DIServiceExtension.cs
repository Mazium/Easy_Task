using Easy_Task.Application.Interface.Repositories;
using Easy_Task.Persistence.Context;
using Easy_Task.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Easy_Task.Common.Utilities.Validators.Validators;

namespace Easy_Task.Persistence.Extensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<EasyTaskDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
                    
            // Register generic repository
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddValidatorsFromAssemblyContaining<CreateEmployeeDtoValidator>();

        }
    }
}
