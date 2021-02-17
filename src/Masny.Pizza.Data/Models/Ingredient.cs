using System.Collections.Generic;

namespace Masny.Pizza.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
