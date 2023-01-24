using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.DAL.Services.Interfaces;

namespace IdunnoAPI.DAL.Services
{
    public class PostsService : IPostsService, IDisposable
    {
        private bool disposedValue;

        public IPostRepository Posts { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Posts.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void GetPosts()
        {
            throw new NotImplementedException();
        }
    }
}
