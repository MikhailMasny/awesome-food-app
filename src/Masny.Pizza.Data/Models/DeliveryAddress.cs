namespace Masny.Pizza.Data.Models
{
    public class DeliveryAddress
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Address { get; set; }

        public int Apartment { get; set; }

        public int Floor { get; set; }

        public int Intercom { get; set; }

        public int Entrance { get; set; }
    }
}
