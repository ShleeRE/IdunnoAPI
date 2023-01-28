using IdunnoAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdunnoAPI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ActionResult Error()
        {
            RequestException exception = new RequestException(StatusCodes.Status500InternalServerError, "Server could not perform your request.");

            try
            {
                exception = (RequestException)HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error;
            }catch(InvalidCastException ice)
            {
            }
            

            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
}
