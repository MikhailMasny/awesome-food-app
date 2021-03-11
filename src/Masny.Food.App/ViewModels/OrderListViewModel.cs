using Microsoft.AspNetCore.Mvc.Rendering;
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
        /// Statuses.
        /// </summary>
        public SelectList Statuses { get; set; }

        /// <summary>
        /// Phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Current status.
        /// </summary>
        public int CurrentStatus { get; set; }
    }
}
