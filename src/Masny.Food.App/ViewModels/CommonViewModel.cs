using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.App.ViewModels
{
    public class CommonViewModel
    {
        public IEnumerable<Masny.Food.Data.Models.ProductDetail> Products { get; set; }

        public int Count { get; set; }
    }
}
