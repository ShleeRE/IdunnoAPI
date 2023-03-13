using IdunnoAPI.Models;

namespace IdunnoAPI.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        Task<string> GetUserNameAsync(int userId);
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> CheckIfExists(User user);
        Task<bool> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> UpdateUserAsync(User user);
    }
}
