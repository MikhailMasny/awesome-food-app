using System.Collections.Generic;

namespace Masny.Food.App.ViewModels
{
    /// <summary>
    /// Product list view model.
    /// </summary>
    public class ProductListViewModel
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Products.
        /// </summary>
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
