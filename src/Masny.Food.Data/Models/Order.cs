using System;
using System.Collections.Generic;

namespace Masny.Food.Data.Models
{
    /// <summary>
    /// Order.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Date with time of creation.
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation property for user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Promo code.
        /// </summary>
        public string PromoCode { get; set; }

        /// <summary>
        /// Total price.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        public int Status { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
