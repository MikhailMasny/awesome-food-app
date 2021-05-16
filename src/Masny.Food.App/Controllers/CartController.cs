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

            if (!cartDto.ProductIds.Any())
            {
                return View(new CartIndexViewModel
                {
                    Products = new List<ProductViewModel>(),
                });
            }

            var profileDto = await _profileManager.GetProfileByUserIdAsync(userId);
            var selectedProductDtos = await _productManager
                .GetAllProductsByIdsAsync(cartDto.ProductIds);

            IEnumerable<ProductViewModel> GetProductViewModels()
            {
                foreach (var selectedProductDto in selectedProductDtos)
                {
                    yield return new ProductViewModel
                    {
                        Id = selectedProductDto.Id,
                        Name = selectedProductDto.Name,
                        Weight = selectedProductDto.Weight,
                        Diameter = selectedProductDto.Diameter,
                        Kind = selectedProductDto.Kind,
                        Price = selectedProductDto.Price,
                    };
                }
            }

            return View(new CartIndexViewModel
            {
                Products = GetProductViewModels(),
                OrderViewModel = new OrderViewModel
                {
                    Name = profileDto.Name,
                    Address = profileDto.Address,
                },
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartAddOrRemoveViewModel model)
        {
            await _cartService.AddOrRemoveAsync(
                CartOperationType.Add,
                User.GetUserIdByClaimsPrincipal(),
                model.ProductId);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Remove([FromBody] CartAddOrRemoveViewModel model)
        {
            await _cartService.AddOrRemoveAsync(
                CartOperationType.Remove,
                User.GetUserIdByClaimsPrincipal(),
                model.ProductId);

            return Ok();
        }
    }
}
