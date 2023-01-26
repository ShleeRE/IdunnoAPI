using IdunnoAPI.DAL.Services.Interfaces;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public RegisterController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]

        public async Task<ActionResult> RegisterAsync([FromBody]User user)
        {
            return Ok(0);
        }
    }
}
