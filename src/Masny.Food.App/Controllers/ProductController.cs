using Masny.Food.App.ViewModels;
using Masny.Food.Common.Resources;
using Masny.Food.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> List(int id)
        {
            var result = await _productManager.GetAllProductsByProductDetailIdAsync(id);

            if (!result.Products.Any())
            {
                return View(new ProductListViewModel
                {
                    Name = CommonResource.ProductNotFound,
                    Products = new List<ProductViewModel>(),
                });
            }

            IEnumerable<ProductViewModel> ProductViewModels()
            {
                foreach (var productDto in result.Products)
                {
                    if (!productDto.IsArchived)
                    {
                        yield return new ProductViewModel
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
                        };
                    }
                }
            }

            return View(new ProductListViewModel
            {
                Name = result.Name,
                Products = ProductViewModels(),
            });
        }
    }
}
