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
        private readonly IPostsService _postsSerivce;
        private readonly IPostRepository _postRepository;
        public PostsController(IPostsService postsService)
        {
            _postsSerivce = postsService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            IEnumerable<Post> posts = _postsSerivce.GetPosts();

            return Ok(posts);
        }

        [Route("{postID}")]
        [HttpGet]
        public async Task<ActionResult> GetByIdAsync([FromRoute]int postID)
        {
            return Ok(await _postsSerivce.GetPostByIdAsync(postID));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]Post post)
        {
            int newPostID = await _postsSerivce.AddPostAsync(post);
            return Created($"api/Posts/{newPostID}", post);
        }

        [Route("{postID}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromRoute]int postID)
        {
            await _postsSerivce.DeletePostAsync(postID);

            return Ok();
        }


        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] Post post)
        {
            await _postsSerivce.UpdatePostAsync(post);

            return NoContent();
        }
    }
}
