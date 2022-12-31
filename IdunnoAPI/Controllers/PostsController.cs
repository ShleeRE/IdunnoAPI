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
        public async Task<ActionResult> GetAllAsync()
        {
            List<Post> posts = await pm.GetPostsAsync();

            if(posts.Count == 0) { return NotFound(); }

            return Ok(posts);
        }

        [Route("GetById")]
        [HttpGet]
        public async Task<ActionResult> GetByIdAsync(int postID)
        {
            Post post = await pm.GetPostByIdAsync(postID);

            if (post == null) { return NotFound(); }

            return Ok(post);
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

        [Route("Update")]
        [HttpPost]
        public async Task<ActionResult> UpdateAsync(int postID, string postTitle, string postDescription, string imagePath)
        {
            if (await pm.UpdatePostAsync(postID, postTitle, postDescription, imagePath))
            {
                return NoContent();
            }

            return BadRequest("Error, couldn't update");

        }
    }
}
