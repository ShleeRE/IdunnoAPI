using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IdunnoAPI.DAL.Repositories
{
    public class PostRepository : IPostRepository, IDisposable
    {
        private readonly IdunnoDbContext _context;
        private bool disposedValue;

        public PostRepository(IdunnoDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return _context.Posts;
        }

        public async Task<bool> AddPostAsync(Post post)
        {
            await _context.AddAsync(post);

            return _context.SaveChangesAsync().Result != 0;
        }

        public async Task<bool> DeletePostAsync(Post post)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
