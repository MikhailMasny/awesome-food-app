using Masny.Food.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Masny.Food.Logic.Interfaces
{
    /// <summary>
    /// Product manager.
    /// </summary>
    public interface IProductManager
    {
        /// <summary>
        /// Get all product details.
        /// </summary>
        /// <returns>List of product detail data transfer objects.</returns>
        Task<IEnumerable<ProductDetailDto>> GetAllProductDetailsAsync();

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <param name="productDetailId">Product detail identifier.</param>
        /// <returns>List of product data transfer objects and product name.</returns>
        Task<(IEnumerable<ProductDto> productDtos, string productName)> GetAllProductsByProductDetailIdAsync(int productDetailId);

        /// <summary>
        /// Get all products by identifiers.
        /// </summary>
        /// <param name="ids">Identifiers.</param>
        /// <returns>List of product data transfer objects.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsByIdsAsync(IEnumerable<int> ids);

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>List of product data transfer objects.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
