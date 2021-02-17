using Masny.Pizza.Data.Enums;
using System.Collections.Generic;

namespace Masny.Pizza.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public decimal Price { get; set; }

        public double Energy { get; set; }

        public double Protein { get; set; }

        public double Fat { get; set; }

        public double Carbohydrate { get; set; }

        public double Weight { get; set; }

        public DiameterType Diameter { get; set; }

        public ICollection<ProductIngredient> ProductIngredients { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
