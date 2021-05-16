using System;
using System.ComponentModel.DataAnnotations;

namespace Masny.Food.App.ViewModels
{
    /// <summary>
    /// Order edit view model.
    /// </summary>
    public class OrderEditViewModel
    {
        /// <summary>
        /// Order identifier.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        [Required]
        [Range(0, 3, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Status { get; set; }
    }
}
