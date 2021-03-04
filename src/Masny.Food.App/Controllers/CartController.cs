using Masny.Food.Data.Contexts;
using Masny.Food.Logic.Enums;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Masny.Food.App.Controllers
{
    public class CartController : Controller
    {
        private readonly FoodAppContext _foodAppContext;
        private readonly IMemoryCache memoryCache;
        private readonly ICartService cartService;

        public CartController(FoodAppContext foodAppContext,
            IMemoryCache memoryCache,
            ICartService cartService)
        {
            _foodAppContext = foodAppContext;
            this.memoryCache = memoryCache;
            this.cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            //cartService.Get(HttpContext.User)

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartProducts = await cartService.GetAsync(userId);

            //if (cartProducts is null)
            //{
            //    cartProducts = new Logic.Models.CartDto();
            //}


            return View(cartProducts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Model model)
        {
            var pdm = _foodAppContext.Products.AsNoTracking().FirstOrDefault(pd => pd.Id == model.Id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // TODO: To extension

            var pdmDto = new ProductDto
            {
                Id = pdm.Id,
                ProductDetailId = pdm.ProductDetailId,
                Photo = pdm.Photo,
                Price = pdm.Price,
                Energy = pdm.Energy,
                Protein = pdm.Protein,
                Fat = pdm.Fat,
                Carbohydrate = pdm.Carbohydrate,
                Weight = pdm.Weight,
                Comment = pdm.Comment,
                Diameter = pdm.Diameter,
                Kind = pdm.Kind

            };

            await cartService.AddOrUpdateAsync(CartOperationType.Add, userId, pdmDto);

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
