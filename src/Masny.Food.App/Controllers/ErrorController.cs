using Masny.Food.App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Masny.Food.App.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error")]
        public IActionResult Index(int? statusCode = null)
        {

            if (statusCode.HasValue)
            {
                HttpContext.Response.StatusCode = statusCode.Value;
            }

            // TODO: to resources
            return statusCode switch
            {
                400 => View(new ErrorViewModel
                {
                    Description = "400 Bad Request",
                    Message = "The server cannot or will not process the request due to an apparent client error."
                }),
                404 => View(new ErrorViewModel
                {
                    Description = "404 Page not found",
                    Message = "The requested resource could not be found but may be available in the future."
                }),
                _ => View(new ErrorViewModel
                {
                    Description = "500 Internal Server Error",
                    Message = "Oops! Something went wrong."
                }),
            };
        }
    }
}
