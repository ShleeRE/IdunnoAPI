using IdunnoAPI.Models;
using MySqlConnector;

namespace IdunnoAPI.Data
{
    public class PostsManager
    {
        private readonly MySqlDbContext _context;

        public PostsManager(MySqlDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            List<Post> posts = new List<Post>();
            Post temp = new Post();

            try
            {
                await _context.conn.OpenAsync();

                MySqlCommand cmd = new MySqlCommand("USE idunnodb; SELECT * FROM Posts;", _context.conn);

                await using MySqlDataReader reader = await cmd.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    temp.PostID = (int)reader[0];
                    temp.UserID = (int)reader[1];
                    temp.PostDate = reader[2].ToString();
                    temp.PostTitle = reader[3].ToString();
                    temp.PostDescription = reader[4].ToString();
                    temp.ImagePath = reader[5].ToString();

                    posts.Add(temp);

                    temp = new Post();
                }

                await _context.conn.CloseAsync();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Post>().ToList();
            }

            return posts;
        }

    }
}
