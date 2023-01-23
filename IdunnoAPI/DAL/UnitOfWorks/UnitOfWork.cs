using IdunnoAPI.DAL.Repositories;

namespace IdunnoAPI.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IdunnoDbContext _context;
        public IPostRepository Posts { get; private set; }

        public IUserRepository Users {get; private set; }

        public UnitOfWork(IdunnoDbContext context)
        {
            _context = context;
            Posts = new PostRepository(_context);
            Users = new UserRepository(_context);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
