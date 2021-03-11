using System.Collections.Generic;

namespace Masny.Food.App.ViewModels
{
    /// <summary>
    /// Cart index view model.
    /// </summary>
    public class CartIndexViewModel
    {
        /// <summary>
        /// Product view models.
        /// </summary>
        public IEnumerable<ProductViewModel> Products { get; set; }

        /// <summary>
        /// Order view model.
        /// </summary>
        public OrderViewModel OrderViewModel { get; set; }
    }
}
