using IdunnoAPI.Data;
using IdunnoAPI.Extensions;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly MySqlDbContext _context;

        private UsersManager usersManager;


        public RegisterController(MySqlDbContext context)
        {
            _context = context;

            usersManager = new UsersManager(_context);
        }

        [HttpPost]

        public async Task<ActionResult> RegisterAsync([FromBody]User user)
        {
             ValidationResult result = await usersManager.RegisterUserAsync(user);


            return StatusCode(result.StatusCode);
        }
    }
}
