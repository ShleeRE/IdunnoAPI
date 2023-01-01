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

            if(posts.Count == 0) { return StatusCode(500); } // INTERNAL SERVER ERROR

            return Ok(posts);
        }

        [Route("{postID}")]
        [HttpGet]
        public async Task<ActionResult> GetByIdAsync([FromRoute]int postID)
        {
            List<Post> posts = await pm.GetPostsAsync(postID);

            if (posts.Count == 0) { return NotFound(); }

            return Ok(posts[0]);
        }


        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]Post post)
        {
            if(await pm.AddPostAsync(post))
            {
                return Created($"/Posts/{post.PostID}", post); // CreatedResult - 201 Status Code with created resource in response body.
            }

            return StatusCode(500); // INTERNAL SERVER ERROR
        }

        [Route("{postID}")]
        [HttpPost]
        public async Task<ActionResult> DeleteAsync([FromRoute]int postID)
        {
            if (await pm.DeletePostAsync(postID))
            {
                return NoContent();
            }

            return StatusCode(500); // INTERNAL SERVER ERROR

        }


        [Route("{postID}")]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromRoute] int postID, [FromBody] Post post)
        {
            if (await pm.UpdatePostAsync(postID, post))
            {
                return NoContent();
            }

            return StatusCode(500); // INTERNAL SERVER ERROR

        }
    }
}
