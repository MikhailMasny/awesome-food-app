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

        Task Clear(string userId);
        CartDto Get(string userId);

        void AddOrUpdate(int operationType, string userId, Product product);
    }
}
