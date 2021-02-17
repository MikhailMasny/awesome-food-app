using Masny.Pizza.App.ViewModels;
using Masny.Pizza.Data.Contexts;
using Masny.Pizza.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Pizza.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly PizzaAppContext _pizzaAppContext;
        private readonly ICartService _cartService;

        public HomeController(PizzaAppContext pizzaAppContext,
            ICartService cartService)
        {
            _pizzaAppContext = pizzaAppContext ?? throw new ArgumentNullException(nameof(pizzaAppContext));
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            Console.WriteLine(HttpContext.User.Identity.Name);

            var cartServiceModel = _cartService.Get(HttpContext.User.Identity.Name.ToString());

            return View(new CommonViewModel
            {
                Products = await _pizzaAppContext.Products.AsNoTracking().ToListAsync(),
                Count = cartServiceModel.Products.Count
            });
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var pdm = _pizzaAppContext.ProductDetails.AsNoTracking().FirstOrDefault(pd => pd.ProductId == id);
            _cartService.AddOrUpdate(1, HttpContext.User.Identity.Name, pdm);

            //var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
            //await _todoManager.DeleteAsync(id, userId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Secret()
        {
            return View();
        }
    }
}
