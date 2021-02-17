using Masny.Pizza.Data.Enums;
using System.Collections.Generic;

namespace Masny.Pizza.Data.Models
{
    public class Product
    {
        // TODO: Type (pizza or any type)
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }

        //public DiameterType Diameter { get; set; } // TODO: to another table

        public ICollection<ProductIngredient> ProductIngredients { get; set; }

        
    }
}
