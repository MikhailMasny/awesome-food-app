using Masny.Food.Data.Enums;
using System;

namespace Masny.Food.Data.Models
{
    /// <summary>
    /// User profile entity.
    /// </summary>
    public class Profile
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
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gender type.
        /// </summary>
        public GenderType Gender { get; set; }

        /// <summary>
        /// Birthdate.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }
    }
}
