namespace IdunnoAPI.DAL.Repositories.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        void GetUsers();
        void AddUser();
        void UpdateUser();
        void DeleteUser();
    }
}
