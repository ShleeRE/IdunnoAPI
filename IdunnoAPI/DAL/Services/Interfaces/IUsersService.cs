using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.Models;

namespace IdunnoAPI.DAL.Services.Interfaces
{
    public interface IUsersService : IDisposable
    {
        IUserRepository Users { get; }
        Task<bool> RegisterUserAsync(User user);
    }
}
