using IdunnoAPI.Models;

namespace IdunnoAPI.DAL.Repositories.Interfaces
{
    public interface IPostRepository : IDisposable
    {
        IEnumerable<Post> GetPosts();
        Task<IEnumerable<Post>> GetPostsByMatchAsync(string match);
        Task<Post> GetPostByIdAsync(int id);
        Task<int> AddPostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int postID);
    }
}
