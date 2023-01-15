using IdunnoAPI.Data;
using IdunnoAPI.Extensions;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Mvc;


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
            ValidationResult valResult = new ValidationResult();
            valResult = await this.AuthUserAsync(user, usersManager, _config);

            return StatusCode(valResult.StatusCode, valResult.Message);
        }


    }
}
