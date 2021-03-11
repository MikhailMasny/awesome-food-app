using Masny.Food.App.Extensions;
using Masny.Food.App.ViewModels;
using Masny.Food.Logic.Enums;
using Masny.Food.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.App.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductManager _productManager;
        private readonly IProfileManager _profileManager;

        public CartController(
            ICartService cartService,
            IProductManager productManager,
            IProfileManager profileManager)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserIdByClaimsPrincipal();

            var cartDto = await _cartService.GetAsync(userId);

            var productViewModels = new List<ProductViewModel>();
            if (cartDto.ProductIds.Any())
            {
                var productDtos = await _productManager.GetAllProductsByIds(cartDto.ProductIds);

                foreach (var product in productDtos)
                {
                    productViewModels.Add(new ProductViewModel
                    {
                        Id = product.Id,
                        Price = product.Price,
                    });
                }
            }

            var profile = await _profileManager.GetProfileByUserIdAsync(userId);
            var cart = await _cartService.GetAsync(userId);
            //var products = await _productManager.GetAllProductsByIds(cart.ProductIds);
            //var totalPrice = products.Select(p => p.Price).Sum();

            var orderViewModel = new OrderViewModel
            {
                Name = profile.Name,

                TotalPrice = await _productManager.GetTotalPriceByProductIds(cart.ProductIds), // UNDONE: to calc service

                //Phone = orderDto.Phone,
                //Address = orderDto.Address,
            };

            var cartIndexViewModel = new CartIndexViewModel
            {
                Products = productViewModels,
                OrderViewModel = orderViewModel,
            };

            return View(cartIndexViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartAddViewModel model)
        {
            await _cartService.AddOrUpdateAsync(
                CartOperationType.Add,
                User.GetUserIdByClaimsPrincipal(),
                model.ProductId);

            return Ok();
        }
    }
}
