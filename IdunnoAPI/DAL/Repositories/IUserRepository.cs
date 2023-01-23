namespace IdunnoAPI.DAL.Repositories
{
    public interface IUserRepository : IDisposable
    {
        void GetUsers();
        void AddUser();
        void UpdateUser();
        void DeleteUser();
    }
}
