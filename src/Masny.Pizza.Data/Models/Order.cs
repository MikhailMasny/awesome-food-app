using System.Collections.Generic;

namespace Masny.Pizza.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        // TODO: release it
        //public string PromoCode { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
