namespace Masny.Pizza.Data.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductDetailId { get; set; }

        public ProductDetail ProductDetail { get; set; }
    }
}
