using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        public IEnumerable<Post> GetPosts()
        {
            return _context.Posts;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            Post searchedPost = await _context.Posts.Where(p => p.PostID == id).FirstOrDefaultAsync();

            if(searchedPost == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "This post could not be found.");
            }

            return searchedPost;
        }

        public async Task<int> AddPostAsync(Post post)
        {
            _context.Posts.Add(post);

            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError, "Couldn't add post");
            }

            return post.PostID;
        }

        public async Task<bool> DeletePostAsync(int postID)
        {
            Post post = new Post { PostID = postID };

            _context.Posts.Attach(post);
            _context.Posts.Remove(post);

            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError, "Couldn't delete post");
            }

            return true;
        }


        public async Task<bool> UpdatePostAsync(Post post)
        {
            Post postToModify = await GetPostByIdAsync(post.PostID);

            postToModify.PostTitle = post.PostTitle;
            postToModify.PostDescription = post.PostDescription;

            int result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError, "Couldn't update post");
            }

            return true;
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
