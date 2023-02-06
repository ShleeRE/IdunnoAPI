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

        public async Task<ActionResult> RegisterAsync([FromBody]User user) // 200 OK not Created 204 we won't return User in request response due pure security.
        {
            await _usersService.RegisterUserAsync(user);

            return Ok("User has been registered.");
        }
    }
}
