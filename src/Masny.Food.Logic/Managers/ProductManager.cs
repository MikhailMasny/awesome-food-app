using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Managers
{
    /// <inheritdoc cref="IProductManager"/>
    public class ProductManager : IProductManager
    {
        private readonly IRepositoryManager<Product> _productManager;
        private readonly IRepositoryManager<ProductDetail> _productDetailManager;

        public ProductManager(
            IRepositoryManager<Product> productManager,
            IRepositoryManager<ProductDetail> productDetailManager)
        {
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
            _productDetailManager = productDetailManager ?? throw new ArgumentNullException(nameof(productDetailManager));
        }

        public async Task<IEnumerable<ProductDetailDto>> GetAllProductDetails()
        {
            var productDetails = await _productDetailManager.GetAll().ToListAsync();

            var productDetailDtos = new List<ProductDetailDto>();
            foreach (var productDetail in productDetails)
            {
                // TODO: change to yield return
                productDetailDtos.Add(new ProductDetailDto
                {
                    Id = productDetail.Id,
                    Name = productDetail.Name,
                    Description = productDetail.Description,
                    Comment = productDetail.Comment,
                });
            }

            return productDetailDtos;
        }

        public async Task<(IEnumerable<ProductDto> productDtos, string productName)> GetAllProductsByProductDetailId(int productDetailId)
        {
            var products = await _productManager
                .GetAll()
                .Include(p => p.ProductDetail)
                .Where(p => p.ProductDetailId == productDetailId)
                .ToListAsync();

            var productDtos = new List<ProductDto>();
            foreach (var product in products)
            {
                // TODO: change to yield return
                productDtos.Add(new ProductDto
                {
                    Id = product.Id,
                    Photo = product.Photo,
                    Price = product.Price,
                    Energy = product.Energy,
                    Protein = product.Protein,
                    Fat = product.Fat,
                    Carbohydrate = product.Carbohydrate,
                    Weight = product.Weight,
                    Comment = product.Comment,
                    Diameter = product.Diameter,
                    Kind = product.Kind,
                });
            }

            return (productDtos, products.FirstOrDefault().ProductDetail.Name);
        }
    }
}
