using Masny.Food.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Masny.Food.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly FoodAppContext _foodAppContext;

        public ProductController(FoodAppContext foodAppContext)
        {
            _foodAppContext = foodAppContext;
        }

        public async Task<IActionResult> DetailAsync(int id)
        {
            


            var pdm = await _foodAppContext.Products.Include(pd => pd.ProductDetail).AsNoTracking().Where(pd => pd.ProductDetailId == id).ToListAsync();
            //_cartService.AddOrUpdate(1, HttpContext.User.Identity.Name, pdm);

            return View(pdm);
        }
    }
}
