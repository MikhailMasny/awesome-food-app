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
        Task<IEnumerable<ProductDetailDto>> GetAllProductDetails();

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <param name="productDetailId">Product detail identifier.</param>
        /// <returns>List of product data transfer objects.</returns>
        Task<(IEnumerable<ProductDto> productDtos, string productName)> GetAllProductsByProductDetailId(int productDetailId);
    }
}
