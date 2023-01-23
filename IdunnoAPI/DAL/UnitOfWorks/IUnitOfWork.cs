using IdunnoAPI.DAL.Repositories;

namespace IdunnoAPI.DAL.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        IUserRepository Users { get; }
        int Save();
    }
}
