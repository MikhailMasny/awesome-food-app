using Masny.Pizza.Data.Models;
using Masny.Pizza.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masny.Pizza.Logic.Interfaces
{
    public interface ICartService
    {
        CartDto Get(string userId);

        void AddOrUpdate(int operationType, string userId, ProductDetail product);
    }
}
