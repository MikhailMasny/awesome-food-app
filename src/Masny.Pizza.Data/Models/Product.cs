using Masny.Pizza.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masny.Pizza.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int ProductDetailId { get; set; }

        public ProductDetail ProductDetail { get; set; }

        public byte[] Photo { get; set; }

        public decimal Price { get; set; }

        public double Energy { get; set; }

        public double Protein { get; set; }

        public double Fat { get; set; }

        public double Carbohydrate { get; set; }

        public double Weight { get; set; }

        public string Comment { get; set; }

        public DiameterType Diameter { get; set; }

        public int Size { get; set; }

        public int Kind { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
