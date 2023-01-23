using IdunnoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IdunnoAPI.DAL
{
    public class IdunnoDbContext : DbContext
    {
        private DbSet<Post> _posts;
        private DbSet<User> _users;
        public IdunnoDbContext(DbContextOptions<IdunnoDbContext> options) : base(options)
        { 
        }
    }
}
