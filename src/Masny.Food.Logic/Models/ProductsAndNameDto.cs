using System.Collections.Generic;

namespace Masny.Food.Logic.Models
{
    /// <summary>
    /// Product list with name data transfer obejct.
    /// </summary>
    public class ProductsAndNameDto
    {
        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product data transfer obejcts.
        /// </summary>
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
