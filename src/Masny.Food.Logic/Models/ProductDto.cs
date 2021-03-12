using Masny.Food.Common.Enums;

namespace Masny.Food.Logic.Models
{
    /// <summary>
    /// Product data transfer object.
    /// </summary>
    public class ProductDto
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
        /// Is archived.
        /// </summary>
        public bool IsArchived { get; set; }
    }
}
