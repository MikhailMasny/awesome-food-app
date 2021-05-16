﻿namespace Masny.Food.Logic.Models
{
    /// <summary>
    /// Promo code data transfer object.
    /// </summary>
    public class PromoCodeDto
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
