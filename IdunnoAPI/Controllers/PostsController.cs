using IdunnoAPI.DAL.Repositories.Interfaces;
using IdunnoAPI.DAL.Services;
using IdunnoAPI.DAL.Services.Interfaces;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly IPostRepository _posts;
        public PostsController(IPostsService postsService, IPostRepository postsRepo)
        {
            _postsService = postsService;
            _posts = postsRepo;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            IEnumerable<Post> posts = _posts.GetPosts();

            return Ok(posts);
        }

        [Route("ByMatch")]
        [HttpGet]
        public async Task<ActionResult> GetPostsByMatchAsync([FromQuery]string title, [FromQuery]string description)
        {
            IEnumerable<Post> posts = await _posts.GetPostsByMatchAsync(title, description);

            return Ok(posts);
        }

        [Route("{postID}")]
        [HttpGet]
        public async Task<ActionResult> GetByIdAsync([FromRoute]int postID)
        {
            return Ok(await _posts.GetPostByIdAsync(postID));
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]Post post)
        {
            int newPostID = await _posts.AddPostAsync(post);
            return Created($"api/Posts/{newPostID}", post);
        }

        [Route("{postID}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromRoute]int postID)
        {
            await _posts.DeletePostAsync(postID);

            return Ok(); // to check
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] Post post)
        {
            await _posts.UpdatePostAsync(post);

            return NoContent(); // to check
        }
    }
}
