using IdunnoAPI.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _users;
        public UsersController(IUserRepository users)
        {
            _users = users;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult> GetNameByUserIdAsync([FromRoute]int userId)
        {
            return Ok(await _users.GetUserNameAsync(userId));
        }
    }
}
