using IdunnoAPI.Models;

namespace IdunnoAPI.Data
{
    public class PostsManager
    {
        private readonly MySqlDbContext _context;

        public PostsManager(MySqlDbContext context)
        {
            _context = context;
        }

        public void GetPosts()
        {
            try
            {

            }catch(Exception ex)
            {

            }
        }

    }
}
