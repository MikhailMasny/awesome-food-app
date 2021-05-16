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
        /// Get product details.
        /// </summary>
        /// <returns>Product detail data transfer objects.</returns>
        Task<IEnumerable<ProductDetailDto>> GetAllProductDetailsAsync();

        /// <summary>
        /// Get products.
        /// </summary>
        /// <param name="productDetailId">Product detail identifier.</param>
        /// <returns>Product list with name data transfer obejct.</returns>
        Task<ProductsAndNameDto> GetAllProductsByProductDetailIdAsync(int productDetailId);

        /// <summary>
        /// Get products by identifiers.
        /// </summary>
        /// <param name="ids">Identifiers.</param>
        /// <returns>Product data transfer objects.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsByIdsAsync(IEnumerable<int> ids);
    }
}
