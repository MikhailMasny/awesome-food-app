using Masny.Food.Data.Enums;
using System.Collections.Generic;

namespace Masny.Food.Data.Models
{
    /// <summary>
    /// Product.
    /// </summary>
    public class Product
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
        /// Navigation property for product detail.
        /// </summary>
        public ProductDetail ProductDetail { get; set; }

        /// <summary>
        /// Photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Energy.
        /// </summary>
        public double Energy { get; set; }

        /// <summary>
        /// Protein.
        /// </summary>
        public double Protein { get; set; }

        /// <summary>
        /// Fat.
        /// </summary>
        public double Fat { get; set; }

        /// <summary>
        /// Carbohydrate.
        /// </summary>
        public double Carbohydrate { get; set; }

        /// <summary>
        /// Weight.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Diameter.
        /// </summary>
        public DiameterType Diameter { get; set; }

        /// <summary>
        /// Kind.
        /// </summary>
        public KindType Kind { get; set; }

        /// <summary>
        /// Navigation property for order product.
        /// </summary>
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
