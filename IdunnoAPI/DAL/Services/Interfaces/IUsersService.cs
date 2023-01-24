using IdunnoAPI.DAL.Repositories.Interfaces;

namespace IdunnoAPI.DAL.Services.Interfaces
{
    public interface IUsersService
    {
        IUserRepository Users { get; }
        void GetUsers();
    }
}
