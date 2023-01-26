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
            RequestException exception = (RequestException)HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error;

            return StatusCode(exception.StatusCode, exception.Message);
        }
    }
}
