using IdunnoAPI.Data;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly MySqlDbContext _context;

        private PostsManager pm;

        public PostsController(MySqlDbContext context)
        {
            _context = context;

            pm = new PostsManager(_context);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<Post> posts = await pm.GetPostsAsync();

            if(posts.Count == 0) { return NoContent(); }

            return Ok(posts);
        }

        [Route("Add")]
        [HttpPost]
        public void Add(int opId, string postDescription, string imagePath)
        {

        }
    }
}
