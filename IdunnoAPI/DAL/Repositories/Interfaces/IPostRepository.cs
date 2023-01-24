namespace IdunnoAPI.DAL.Repositories.Interfaces
{
    public interface IPostRepository : IDisposable
    {
        void GetPosts();
        void AddPost();
        void UpdatePost();
        void DeletePost();
    }
}
