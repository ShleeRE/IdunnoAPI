using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IdunnoAPI.DAL;
using IdunnoAPI.DAL.Services.Interfaces;
using IdunnoAPI.DAL.Services;
using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.DAL.Repositories;

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
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddAuth(this IServiceCollection services, ConfigurationManager cfg)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = options.DefaultChallengeScheme = options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["JWT:Key"]))
                };
            });
        }
    }
}
