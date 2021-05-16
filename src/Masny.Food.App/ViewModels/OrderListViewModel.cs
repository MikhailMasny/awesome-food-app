using System.Collections.Generic;

namespace Masny.Food.App.ViewModels
{
    /// <summary>
    /// Order list view model.
    /// </summary>
    public class OrderListViewModel
    {
        /// <summary>
        /// Orders.
        /// </summary>
        public IEnumerable<OrderViewModel> Orders { get; set; }

        /// <summary>
        /// Current status.
        /// </summary>
        public int CurrentStatus { get; set; }
    }
}
