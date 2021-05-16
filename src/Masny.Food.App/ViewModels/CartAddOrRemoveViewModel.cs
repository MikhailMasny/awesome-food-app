using System.ComponentModel.DataAnnotations;

namespace Masny.Food.App.ViewModels
{
    /// <summary>
    /// Cart add view model.
    /// </summary>
    public class CartAddOrRemoveViewModel
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }
}
