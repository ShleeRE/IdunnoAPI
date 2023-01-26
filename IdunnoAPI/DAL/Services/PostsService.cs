using IdunnoAPI.DAL.Repositories;
using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.DAL.Services.Interfaces;
using IdunnoAPI.Models;

namespace IdunnoAPI.DAL.Services
{
    public class PostsService : IPostsService, IDisposable
    {
        private bool disposedValue;

        public IPostRepository Posts { get; private set; }

        public PostsService(IdunnoDbContext context)
        {
            Posts = new PostRepository(context);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if(Posts != null)
                    {
                        Posts.Dispose();
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

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            IEnumerable<Post> posts = await Posts.GetPostsAsync();

            return posts;
        }
        public void AddPost(Post post)
        {
            Posts.AddPostAsync(post).Wait();
        }
    }
}
