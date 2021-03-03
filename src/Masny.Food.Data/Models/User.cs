using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Masny.Food.Data.Models
{
    /// <summary>
    /// User (by ASP.NET Identity).
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Navigation property for order.
        /// </summary>
        public ICollection<Order> Orders { get; set; }

        /// <summary>
        /// Navigation property for delivery address.
        /// </summary>
        public ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        /// <summary>
        /// Navigation property for profile.
        /// </summary>
        public Profile Profile { get; set; }
    }
}
