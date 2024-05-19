using Easy_Task.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Easy_Task.Common.Utilities
{
    public static class Seeder
    {
        public static async Task SeedRolesAndSuperAdmin(IServiceProvider serviceProvider)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                    var roles = new[] { "Admin", "User" };
                    var roleTasks = roles
                        .Where(role => !roleManager.RoleExistsAsync(role).Result)
                        .Select(role => roleManager.CreateAsync(new IdentityRole(role)));

                    var roleResults = await Task.WhenAll(roleTasks);

                    foreach (var result in roleResults)
                    {
                        if (!result.Succeeded)
                        {
                            LogErrors(result.Errors);
                        }
                    }

                    var adminUser = await userManager.FindByNameAsync("Admin");
                    if (adminUser == null)
                    {
                        adminUser = new AppUser
                        {
                            UserName = "Admin",
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            LastName = "User", // Ensure you have LastName if needed
                            CreatedAt = DateTime.UtcNow
                        };

                        var userResult = await RegisterUserAsync(userManager, adminUser, "Password@123");
                        if (!userResult.Succeeded)
                        {
                            LogErrors(userResult.Errors);
                        }
                    }

                    var rolesToAssign = new[] { "Admin", "User" };
                    var roleAssignmentTasks = rolesToAssign
                        .Where(role => !userManager.IsInRoleAsync(adminUser, role).Result)
                        .Select(role => userManager.AddToRoleAsync(adminUser, role));

                    var roleAssignmentResults = await Task.WhenAll(roleAssignmentTasks);

                    foreach (var result in roleAssignmentResults)
                    {
                        if (!result.Succeeded)
                        {
                            LogErrors(result.Errors);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private static async Task<IdentityResult> RegisterUserAsync(UserManager<AppUser> userManager, AppUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Additional logic for successful registration, like sending confirmation email, etc.
            }
            return result;
        }

        private static void LogErrors(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
        }

        private static void LogException(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                LogException(ex.InnerException);
            }
        }
    }
}
