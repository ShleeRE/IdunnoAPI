using IdunnoAPI.DAL.Repositories;
using IdunnoAPI.DAL.Repositories.Interfaces;
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
        private readonly IUserRepository _usersService;
        public LoginController(IUserRepository usersService)
        {
            _usersService = usersService;
        }


        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] User user)
        {
            return Ok(2);
        }


    }
}
