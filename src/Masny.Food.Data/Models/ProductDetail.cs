using System.Collections.Generic;

namespace Masny.Food.Data.Models
{
    /// <summary>
    /// Product detail entity.
    /// </summary>
    public class ProductDetail
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Navigation property for product.
        /// </summary>
        public ICollection<Product> Products { get; set; }

        /// <summary>
        /// Navigation property for product ingredient.
        /// </summary>
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
