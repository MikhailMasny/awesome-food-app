using Masny.Food.App.ViewModels;
using Masny.Food.Common.Resources;
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

            return statusCode switch
            {
                400 => View(new ErrorViewModel
                {
                    Description = CommonResource.Error400Description,
                    Message = CommonResource.Error400Message
                }),
                404 => View(new ErrorViewModel
                {
                    Description = CommonResource.Error404Description,
                    Message = CommonResource.Error404Message
                }),
                _ => View(new ErrorViewModel
                {
                    Description = CommonResource.Error500Description,
                    Message = CommonResource.Error500Message
                }),
            };
        }
    }
}
