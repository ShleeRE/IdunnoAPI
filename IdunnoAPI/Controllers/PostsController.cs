using IdunnoAPI.DAL.Services;
using IdunnoAPI.DAL.Services.Interfaces;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsSerivce;
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
        public ActionResult GetById([FromRoute]int postID)
        {
            return Ok(_postsSerivce.GetPostByID(postID));
        }


        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]Post post)
        {
            int newPostID = await _postsSerivce.AddPostAsync(post);
            return Created($"api/Posts/{newPostID}", post);
        }

        [Route("{postID}")]
        [HttpPost]
        public async Task<ActionResult> DeleteAsync([FromRoute]int postID)
        {
            await _postsSerivce.DeletePostAsync(postID);

            return NoContent();
        }


        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromBody] Post post)
        {
            await _postsSerivce.UpdatePostAsync(post);

            return Ok();
        }
    }
}
