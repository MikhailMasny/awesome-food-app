using System.Collections.Generic;

namespace Masny.Food.Data.Models
{
    /// <summary>
    /// Ingredient.
    /// </summary>
    public class Ingredient
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
        /// Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Navigation property for product ingredient.
        /// </summary>
        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
