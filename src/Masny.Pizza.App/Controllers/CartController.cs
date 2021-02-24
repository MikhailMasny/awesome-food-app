using Masny.Pizza.Data.Contexts;
using Masny.Pizza.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Masny.Pizza.App.Controllers
{
    public class CartController : Controller
    {
        private readonly PizzaAppContext _pizzaAppContext;
        private readonly IMemoryCache memoryCache;
        private readonly ICartService cartService;

        public CartController(PizzaAppContext pizzaAppContext,
            IMemoryCache memoryCache,
            ICartService cartService)
        {
            _pizzaAppContext = pizzaAppContext;
            this.memoryCache = memoryCache;
            this.cartService = cartService;
        }

        [Authorize]
        public IActionResult Index()
        {
            //cartService.Get(HttpContext.User)

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartProducts = cartService.Get(userId);

            //if (cartProducts is null)
            //{
            //    cartProducts = new Logic.Models.CartDto();
            //}


            return View(cartProducts);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromBody] Model model)
        {
            var pdm = _pizzaAppContext.ProductDetails.AsNoTracking().FirstOrDefault(pd => pd.Id == model.Id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // TODO: To extension
            cartService.AddOrUpdate(1, userId, pdm);

            //var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
            //await _todoManager.DeleteAsync(id, userId);

            return Ok();

            //return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> OrderAsync([FromBody] Model model)
        {
            //var list = new List<int>();
            //foreach (var item in model.Array)
            //{
            //    list.Add(Convert.ToInt32(item));
            //}

            //var pdm = await _pizzaAppContext.ProductDetails.Include(p => p.Product).AsNoTracking().Where(p => list.Contains(p.Id)).ToListAsync();

            //return Ok(pdm);
            //return View();

            return Ok();
        }
    }

    public class Model
    {
        public int Id { get; set; }

        
    }
}
