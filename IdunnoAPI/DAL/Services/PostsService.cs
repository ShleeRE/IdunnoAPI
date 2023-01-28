using IdunnoAPI.DAL.Repositories;
using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.DAL.Services.Interfaces;
using IdunnoAPI.Models;

namespace IdunnoAPI.DAL.Services
{
    /// <summary>   Service layer. Return types should be taken with a grain of salt. Error checking is already handled by repository layer by throwing exceptions.
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

        public IEnumerable<Post> GetPosts()
        {
            IEnumerable<Post> posts = Posts.GetPosts();

            return posts;
        }
        public Post GetPostByID(int id)
        {
            return Posts.GetPostByID(id);
        }
        public async Task<int> AddPostAsync(Post post)
        {
            int newPostID = await Posts.AddPostAsync(post);

            return newPostID;
        }

        public async Task<bool> DeletePostAsync(int postID)
        {
            bool deleted = await Posts.DeletePostAsync(postID);

            return deleted;
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            bool modified = await Posts.UpdatePostAsync(post);

            return modified;
        }
    }
}
