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
        private readonly IRepositoryManager<Product> _productRepository;
        private readonly IRepositoryManager<ProductDetail> _productDetailRepository;

        private IQueryable<Product> ProductQuery =>
            _productRepository.GetAll();

        public ProductManager(
                    IRepositoryManager<Product> productRepository,
            IRepositoryManager<ProductDetail> productDetailRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productDetailRepository = productDetailRepository ?? throw new ArgumentNullException(nameof(productDetailRepository));
        }

        public async Task<IEnumerable<ProductDetailDto>> GetAllProductDetailsAsync()
        {
            var productDetails = await _productDetailRepository
                .GetAll()
                .Where(productDetail => productDetail.Products.Any(p => !p.IsArchived))
                .ToListAsync();

            IEnumerable<ProductDetailDto> GetProductDetails()
            {
                foreach (var productDetail in productDetails)
                {
                    yield return new ProductDetailDto
                    {
                        Id = productDetail.Id,
                        Name = productDetail.Name,
                        Description = productDetail.Description,
                        Comment = productDetail.Comment,
                    };
                }
            }

            return GetProductDetails();
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsByIdsAsync(IEnumerable<int> ids) =>
            GetProducts(await ProductQuery
                .Include(p => p.ProductDetail)
                .Where(p => ids.Contains(p.Id))
                .ToListAsync());

        public async Task<ProductsAndNameDto> GetAllProductsByProductDetailIdAsync(int productDetailId)
        {
            var products = await ProductQuery
                .Include(p => p.ProductDetail)
                .Where(p => p.ProductDetailId == productDetailId)
                .ToListAsync();

            if (!products.Any())
            {
                return new ProductsAndNameDto
                {
                    Name = string.Empty,
                    Products = new List<ProductDto>(),
                };
            }

            return new ProductsAndNameDto
            {
                Name = products.FirstOrDefault().ProductDetail.Name,
                Products = GetProducts(products),
            };
        }

        private IEnumerable<ProductDto> GetProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                yield return new ProductDto
                {
                    Id = product.Id,
                    Name = product.ProductDetail.Name,
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
                    IsArchived = product.IsArchived,
                };
            }
        }
    }
}
