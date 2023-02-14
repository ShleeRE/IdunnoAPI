using IdunnoAPI.Models;
using IdunnoAPI.DAL.Repositories.Interfaces;

namespace IdunnoAPI.DAL.Services.Interfaces
{
    public interface IPostsService : IDisposable
    {
        IPostRepository Posts { get; }
    }
}
