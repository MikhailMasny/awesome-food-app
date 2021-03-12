namespace Masny.Food.Data.Models
{
    /// <summary>
    /// Promo code entity.
    /// </summary>
    public class PromoCode
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }
    }
}
