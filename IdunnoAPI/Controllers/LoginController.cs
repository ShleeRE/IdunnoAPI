using IdunnoAPI.Data;
using IdunnoAPI.Extensions;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MySqlDbContext _context;

        private UsersManager usersManager;
        public LoginController(MySqlDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

            usersManager = new UsersManager(_context);
        }


        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] User user)
        {
            string result = await this.AuthUserAsync(user, usersManager, _config);

            if(result != null)
            {
                return Ok(result);
            }

            return NotFound("User not found");
        }


    }
}
