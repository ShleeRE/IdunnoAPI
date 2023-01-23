using IdunnoAPI.DAL.Repositories;
using IdunnoAPI.DAL.UnitOfWorks;
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
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] User user)
        {
            return Ok(2);
        }


    }
}
