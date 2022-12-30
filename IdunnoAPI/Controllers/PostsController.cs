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

        [Route("GetPosts")]
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            List<Post> posts = await pm.GetPostsAsync();

            if(posts.Count == 0) { return NotFound(); }

            return Ok(posts);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult> AddAsync(Post post)
        {
            if(await pm.AddPostAsync(post))
            {
                return NoContent();
            }

            return BadRequest("Error, couldn't add");

           
        }
        [Route("Delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteAsync(int postID)
        {
            if (await pm.DeletePostAsync(postID))
            {
                return NoContent();
            }

            return BadRequest("Error, couldn't delete");

        }
    }
}
