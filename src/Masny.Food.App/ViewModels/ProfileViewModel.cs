using Masny.Food.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Masny.Food.App.ViewModels
{
    /// <summary>
    /// Profile view model.
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [Display(Name = nameof(Name))]
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
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Avatar from file.
        /// </summary>
        public IFormFile AvatarFile { get; set; }
    }
}
