namespace Masny.Pizza.Data.Models
{
    public class ProductIngredient
    {
        public int Id { get; set; }

        public int ProductDetailId { get; set; }

        public ProductDetail ProductDetail { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
