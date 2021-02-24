using Masny.Pizza.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masny.Pizza.Logic.Models
{
    public class CartDto
    {
        public string UserId { get; set; }

        // TODO: change it to Dto object
        public IList<Product> Products { get; set; }

        //public IList<int> ProductIds { get; set; }
    }
}
