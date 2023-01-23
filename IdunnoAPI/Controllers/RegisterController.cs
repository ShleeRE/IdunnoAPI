using IdunnoAPI.DAL.UnitOfWorks;
using IdunnoAPI.Data;
using IdunnoAPI.Helpers;
using IdunnoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]

        public async Task<ActionResult> RegisterAsync([FromBody]User user)
        {
            return Ok(0);
        }
    }
}
