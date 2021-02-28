using System;
using System.Collections.Generic;

namespace Masny.Pizza.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime Creation { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        // TODO: release it
        //public string PromoCode { get; set; }

        public decimal TotalPrice { get; set; }

        public string Comment { get; set; }

        public int Status { get; set; }

        // TODO: comment status time

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
