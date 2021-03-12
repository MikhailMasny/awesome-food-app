using Masny.Food.App.ViewModels;
using Masny.Food.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// TODO:
// Promocode and totalprice by js
// Admin panel
// Common project
// sql scripts

namespace Masny.Food.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductManager _productManager;

        public HomeController(IProductManager productManager)
        {
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        }

        public async Task<IActionResult> Index()
        {
            var productDetailDtos = await _productManager.GetAllProductDetailsAsync();

            var productDetailViewModels = new List<ProductDetailViewModel>();
            foreach (var productDetailDto in productDetailDtos)
            {
                productDetailViewModels.Add(new ProductDetailViewModel
                {
                    Id = productDetailDto.Id,
                    Name = productDetailDto.Name,
                    Description = productDetailDto.Description,
                    Comment = productDetailDto.Comment,
                });
            }

            return View(productDetailViewModels);
        }
    }
}
