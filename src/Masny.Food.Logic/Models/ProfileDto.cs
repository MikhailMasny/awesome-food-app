using Masny.Food.Common.Enums;
using System;

namespace Masny.Food.Logic.Models
{
    /// <summary>
    /// Profile data transfer object.
    /// </summary>
    public class ProfileDto
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        // TODO: to common project

        /// <summary>
        /// Gender type.
        /// </summary>
        public GenderType Gender { get; set; }

        /// <summary>
        /// Birthdate.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }
    }
}
