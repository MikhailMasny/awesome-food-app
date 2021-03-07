﻿using Masny.Food.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;

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
        /// Avatar from file.
        /// </summary>
        public IFormFile AvatarFile { get; set; }
    }
}