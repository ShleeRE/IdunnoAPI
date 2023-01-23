using IdunnoAPI.DAL.UnitOfWorks;
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
        private readonly IUnitOfWork _unitOfWork;
        public PostsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            return Ok(0);
        }

        [Route("{postID}")]
        [HttpGet]
        public async Task<ActionResult> GetByIdAsync([FromRoute]int postID)
        {
            return Ok(0);
        }


        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody]Post post)
        {
            return Ok(0);
        }

        [Route("{postID}")]
        [HttpPost]
        public async Task<ActionResult> DeleteAsync([FromRoute]int postID)
        {
            return Ok(0);

        }


        [Route("{postID}")]
        [HttpPatch]
        public async Task<ActionResult> UpdateAsync([FromRoute] int postID, [FromBody] Post post)
        {
            return Ok(0);
        }
    }
}
