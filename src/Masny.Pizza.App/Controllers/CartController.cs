using Masny.Pizza.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Pizza.App.Controllers
{
    public class CartController : Controller
    {
        private readonly PizzaAppContext _pizzaAppContext;

        public CartController(PizzaAppContext pizzaAppContext)
        {
            _pizzaAppContext = pizzaAppContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderAsync([FromBody] Model model)
        {
            var list = new List<int>();
            foreach (var item in model.Array)
            {
                list.Add(Convert.ToInt32(item));
            }

            var pdm = await _pizzaAppContext.ProductDetails.Include(p => p.Product).AsNoTracking().Where(p => list.Contains(p.Id)).ToListAsync();

            return Ok(pdm);
            //return View();
        }
    }

    public class Model
    {
        public string[] Array { get; set; }

        
    }
}
