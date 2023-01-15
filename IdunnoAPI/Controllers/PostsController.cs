using IdunnoAPI.Data;
using IdunnoAPI.Helpers;
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

            if(posts == null) 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, "Posts couldn't be received, sorry!");                 // INTERNAL SERVER ERROR
            } 

            return Ok(posts);
        }

        [Route("{postID}")]
        [HttpGet]
        public async Task<ActionResult> GetByIdAsync([FromRoute]int postID)
        {
            List<Post> posts = await pm.GetPostsAsync(postID);

            if (posts.Count == 0) { return NotFound("Post not found!"); }

            return Ok(posts[0]);
        }


        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]Post post)
        {
            ValidationResult result = await pm.AddPostAsync(post);

            if (!result.Succeded)
            {
                return StatusCode(result.StatusCode, result.Message);
            }

            return Created($"api/Posts/{post.PostID}", result.Message);
        }

        [Route("{postID}")]
        [HttpPost]
        public async Task<ActionResult> DeleteAsync([FromRoute]int postID)
        {
            ValidationResult result = await pm.DeletePostAsync(postID);

            return StatusCode(result.StatusCode, result.Message);

        }


        [Route("{postID}")]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromRoute] int postID, [FromBody] Post post)
        {
            ValidationResult result = await pm.UpdatePostAsync(postID, post);

            return StatusCode(result.StatusCode, result.Message);
        }
    }
}
