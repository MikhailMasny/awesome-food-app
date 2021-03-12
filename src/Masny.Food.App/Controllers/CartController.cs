using Masny.Food.App.Extensions;
using Masny.Food.App.ViewModels;
using Masny.Food.Common.Enums;
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
        private readonly IProductManager _productManager;
        private readonly IProfileManager _profileManager;
        private readonly ICartService _cartService;

        public CartController(
            IProductManager productManager,
            IProfileManager profileManager,
            ICartService cartService)
        {
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserIdByClaimsPrincipal();

            var cartDto = await _cartService.GetAsync(userId);
            var productDtos = await _productManager.GetAllProductsAsync();

            var productViewModels = new List<ProductViewModel>();
            if (cartDto.ProductIds.Any())
            {
                foreach (var id in cartDto.ProductIds)
                {
                    var productDto = productDtos.First(p => p.Id == id);

                    productViewModels.Add(new ProductViewModel
                    {
                        Id = productDto.Id,
                        Name = productDto.Name,
                        Weight = productDto.Weight,
                        Diameter = productDto.Diameter,
                        Kind = productDto.Kind,
                        Price = productDto.Price,
                    });
                }
            }

            var profileDto = await _profileManager.GetProfileByUserIdAsync(userId);

            var orderViewModel = new OrderViewModel
            {
                Name = profileDto.Name,
                Address = profileDto.Address,
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Remove([FromBody] CartAddViewModel model)
        {
            await _cartService.AddOrUpdateAsync(
                CartOperationType.Remove,
                User.GetUserIdByClaimsPrincipal(),
                model.ProductId);

            return Ok();
        }
    }
}
