using Masny.Pizza.Data.Enums;
using System;

namespace Masny.Pizza.Data.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string FullName { get; set; }

        public GenderType Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public byte[] Avatar { get; set; }
    }
}
