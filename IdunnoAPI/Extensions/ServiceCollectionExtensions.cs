using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IdunnoAPI.DAL;
using IdunnoAPI.DAL.Services.Interfaces;
using IdunnoAPI.DAL.Services;

namespace IdunnoAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly IConfiguration _config;
        public static String policyName = "_myAllowSpecificOrigins";
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: policyName, policy => {
                    policy.WithOrigins("http://localhost:3000");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });
        }

        public static void AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
