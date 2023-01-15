using IdunnoAPI.Controllers;
using IdunnoAPI.Data;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdunnoAPI.Extensions
{
    public static class LoginControllerExtensions
    {
        public static async Task<ValidationResult> AuthUserAsync(this LoginController loginController, User user, UsersManager usersManager, IConfiguration config)
        {
            ValidationResult temp = new ValidationResult();
            temp = await usersManager.FindUserAsync(user);

            if (!temp.Succeded)
            {
                return temp;
            }

            string token = GenerateToken(user, config);

            return temp.FormatReturn(true, token, StatusCodes.Status200OK);
        }

        private static string GenerateToken(User user, IConfiguration config)
        {
            SymmetricSecurityKey ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            SigningCredentials sc = new SigningCredentials(ssk, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.Username), new Claim(ClaimTypes.Role, user.Role) };

            DateTime expire = DateTime.Now.AddMinutes(15);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(config["JWT:Issuer"], config["JWT:Audience"], claims, null, expire, sc );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
