using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace IdunnoAPI.DAL.Services.Interfaces
{
    public interface IUsersService : IDisposable
    {
        IUserRepository Users { get; }
        Task<string> AuthenticateUser(User user, HttpResponse response);
    }
}
