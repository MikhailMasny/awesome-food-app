using Masny.Food.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.App.ViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }

        public SelectList Statuses { get; set; }

        public int CurrentStatus { get; set; }
    }
}
