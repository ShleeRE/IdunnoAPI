using IdunnoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IdunnoAPI.DAL
{
    public class IdunnoDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public IdunnoDbContext(DbContextOptions<IdunnoDbContext> options) : base(options)
        { 
        }
    }
}
