using IdunnoAPI.Models;
using IdunnoAPI.DAL.Repositories.Interfaces;

namespace IdunnoAPI.DAL.Services.Interfaces
{
    public interface IPostsService : IDisposable
    {
        IPostRepository Posts { get; }
        IEnumerable<Post> GetPosts();
        Post GetPostByID(int id);
        Task<int> AddPostAsync(Post post);
        Task<bool> DeletePostAsync(int postID);

        Task<bool> UpdatePostAsync(Post post);
    }
}
