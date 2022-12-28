using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "get method";
        }

        [Route("Add")]
        [HttpPost]
        public void Add(int opId, string postDescription, string imagePath)
        {

        }
    }
}
