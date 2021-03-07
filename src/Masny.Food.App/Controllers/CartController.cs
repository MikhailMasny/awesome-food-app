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

        public CartController(
            ICartService cartService,
            IProductManager productManager)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cartDto = await _cartService.GetAsync(User.GetUserIdByClaimsPrincipal());

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

            return View(productViewModels);
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
