using System.Collections.Generic;

namespace Masny.Food.Logic.Models
{
    /// <summary>
    /// Cart data transfer object.
    /// </summary>
    public class CartDto
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// List of product identifiers.
        /// </summary>
        public IList<int> ProductIds { get; set; }
    }
}
