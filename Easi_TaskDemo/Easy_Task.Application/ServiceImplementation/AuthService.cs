using Easy_Task.Application.DTOs;
using Easy_Task.Application.Interface.Services;
using Easy_Task.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Easy_Task.Application.ServiceImplementation
{
    public class AuthService: IAuthService
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<(int, string)> Register(RegisterDto signup, string role)
        {
            // check user exists or not
            var user = await _userManager.FindByEmailAsync(signup.Email);
            if (user != null)
            {
                return (0, "User is already exists");
            }
            AppUser appUser = new()
            {
                FirstName = signup.FirstName,
                LastName = signup.LastName,
                UserName = signup.Email,
                Email = signup.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var createdUserResult = await _userManager.CreateAsync(appUser, signup.Password);
            if (!createdUserResult.Succeeded)
            {
                return (0, "User creation failed! Please check user details and try again.");
            }
            // create role if does not exists
            bool isRoleExists = await _roleManager.RoleExistsAsync(role);
            if (!isRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            // add user's role
            await _userManager.AddToRoleAsync(appUser, role);
            return (1, "User created successfully!");
        }

        public async Task<(int, string)> Login(LoginDto model)
        {
            // find user by email
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return (0, "Invalid Email");
            }
            // match password
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                return (0, "Invalid Password");
            }

            // get user's roles
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            // create claims
            List<Claim> claims = [
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ];
            foreach (string role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string token = GenerateToken(claims);
            // generate token
            return (1, token);
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JwtSettings:ValidIssuer"],
                Audience = _configuration["JwtSettings:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(1), 
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

