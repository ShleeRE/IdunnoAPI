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

        public void AddPost()
        {
            throw new NotImplementedException();
        }

        public void DeletePost()
        {
            throw new NotImplementedException();
        }

        public void GetPosts()
        {
            throw new NotImplementedException();
        }

        public void UpdatePost()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PostRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
