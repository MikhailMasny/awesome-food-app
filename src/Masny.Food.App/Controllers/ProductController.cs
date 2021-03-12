using Masny.Food.App.ViewModels;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Masny.Food.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        }

        [Authorize]
        public async Task<IActionResult> List(int id)
        {
            (IEnumerable<ProductDto> productDtos, string productName) = await _productManager.GetAllProductsByProductDetailIdAsync(id);

            var productViewModels = new List<ProductViewModel>();
            foreach (var productDto in productDtos)
            {
                if (!productDto.IsArchived)
                {
                    productViewModels.Add(new ProductViewModel
                    {
                        Id = productDto.Id,
                        Photo = productDto.Photo,
                        Price = productDto.Price,
                        Energy = productDto.Energy,
                        Protein = productDto.Protein,
                        Fat = productDto.Fat,
                        Carbohydrate = productDto.Carbohydrate,
                        Weight = productDto.Weight,
                        Comment = productDto.Comment,
                        Diameter = productDto.Diameter,
                        Kind = productDto.Kind,
                    });
                }
            }

            var productListViewModel = new ProductListViewModel
            {
                Name = productName,
                Products = productViewModels,
            };

            return View(productListViewModel);
        }
    }
}
