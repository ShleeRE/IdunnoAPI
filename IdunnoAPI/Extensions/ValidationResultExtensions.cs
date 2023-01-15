using IdunnoAPI.Helpers;
using Microsoft.AspNetCore.Http;

namespace IdunnoAPI.Extensions
{
    public static class ValidationResultExtensions
    {
        public static ValidationResult RetInternelServerError(this ValidationResult valResult)
        {
            valResult.Succeded = false;
            valResult.Message = "Failed to register user - SERVER ERROR.";
            valResult.StatusCode = StatusCodes.Status500InternalServerError;

            return valResult;
        }

        public static ValidationResult FormatReturn(this ValidationResult valResult, bool succeded, string msg, int statusCode)
        {
            valResult.Succeded = succeded;
            valResult.Message = msg;
            valResult.StatusCode = statusCode;

            return valResult;
        }
    }
}
