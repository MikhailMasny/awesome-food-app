using Masny.Food.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Masny.Food.App.ViewModels
{
    /// <summary>
    /// Order view model.
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// Number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Date with time of creation.
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Required]
        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        /// <summary>
        /// Phone.
        /// </summary
        [Required]
        [Display(Name = nameof(Phone))]
        public string Phone { get; set; }

        /// <summary>
        /// In place.
        /// </summary>
        public bool InPlace { get; set; }

        /// <summary>
        /// Address.
        /// </summary>
        [Required]
        [Display(Name = nameof(Address))]
        public string Address { get; set; }

        /// <summary>
        /// Promo code.
        /// </summary>
        public string PromoCode { get; set; }

        /// <summary>
        /// Total price.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        [Display(Name = nameof(Comment))]
        public string Comment { get; set; }

        // TODO: replace to common project

        /// <summary>
        /// Status.
        /// </summary>
        public StatusType Status { get; set; }
    }
}
