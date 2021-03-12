using Masny.Food.Common.Enums;
using System;

namespace Masny.Food.Logic.Models
{
    /// <summary>
    /// Order data transfer object.
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Identifier
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
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// In place.
        /// </summary>
        public bool InPlace { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Promo code.
        /// </summary>
        public string PromoCode { get; set; }

        /// <summary>
        /// Payment.
        /// </summary>
        public PaymentType Payment { get; set; }

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
        public StatusType Status { get; set; }
    }
}
