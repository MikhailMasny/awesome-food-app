namespace Masny.Food.Data.Models
{
    /// <summary>
    /// User delivery address.
    /// </summary>
    public class DeliveryAddress
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation property for user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Apartment.
        /// </summary>
        public int Apartment { get; set; }

        /// <summary>
        /// Floor.
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// Intercom.
        /// </summary>
        public int Intercom { get; set; }

        /// <summary>
        /// Entrance.
        /// </summary>
        public int Entrance { get; set; }
    }
}
