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
        /// Get all products.
        /// </summary>
        /// <returns>List of product data transfer objects.</returns>
        Task<IEnumerable<ProductDetailDto>> GetAll();
    }
}
