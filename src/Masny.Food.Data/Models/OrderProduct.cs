namespace Masny.Food.Data.Models
{
    /// <summary>
    /// Link table Order - Product.
    /// </summary>
    public class OrderProduct
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Order identifier.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Navigation property for order.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Navigation property for product.
        /// </summary>
        public Product Product { get; set; }
    }
}
