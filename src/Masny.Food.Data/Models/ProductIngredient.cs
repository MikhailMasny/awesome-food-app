namespace Masny.Food.Data.Models
{
    /// <summary>
    /// Link table Product - Ingredient.
    /// </summary>
    public class ProductIngredient
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product detail identifier.
        /// </summary>
        public int ProductDetailId { get; set; }

        /// <summary>
        /// Navigation property for order.
        /// </summary>
        public ProductDetail ProductDetail { get; set; }

        /// <summary>
        /// Ingredient identifier.
        /// </summary>
        public int IngredientId { get; set; }

        /// <summary>
        /// Navigation property for ingredient.
        /// </summary>
        public Ingredient Ingredient { get; set; }
    }
}
