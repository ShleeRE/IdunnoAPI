namespace IdunnoAPI.DAL.Repositories
{
    public interface IPostRepository : IDisposable
    {
        void GetPosts();
        void AddPost();
        void UpdatePost();
        void DeletePost();
    }
}
