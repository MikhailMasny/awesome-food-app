using Masny.Pizza.App.ViewModels;
using Masny.Pizza.Data.Contexts;
using Masny.Pizza.Data.Models;
using Masny.Pizza.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Masny.Pizza.App.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService cartService;
        private readonly PizzaAppContext pizzaAppContext;

        public OrderController(ICartService cartService, PizzaAppContext pizzaAppContext)
        {
            this.cartService = cartService;
            this.pizzaAppContext = pizzaAppContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartProducts = cartService.Get(userId);

            //var tp = 
            //var order = new Order
            //{
            //    Name = "Random",
            //    TotalPrice = 
            //}

            //pizzaAppContext.Orders.Add()

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(SimpleTestClass simpleTestClass)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartProducts = cartService.Get(userId);

            var orderNumber = 1;
            var dateTimeNow = DateTime.Now;
            //try
            //{
            //    var lastOrder1 = await pizzaAppContext.Orders.AsNoTracking().LastOrDefaultAsync();
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

            var lastOrder = await pizzaAppContext.Orders
                .AsNoTracking()
                .OrderBy(o => o.Id)
                .LastOrDefaultAsync();

            if (lastOrder is not null && lastOrder.Creation.Date == dateTimeNow.Date)
            {
                orderNumber = ++lastOrder.Number;
            }

            var order = new Order
            {
                Number = orderNumber,
                Creation = dateTimeNow,
                Comment = simpleTestClass.Comment,
                UserId = userId,
                Name = "test",
                Phone = "take from db",
                Address = "take from db",
                TotalPrice = cartProducts.Products.Sum(p => p.Price)
            };

            pizzaAppContext.Orders.Add(order);
            pizzaAppContext.SaveChanges();

            foreach (var item in cartProducts.Products)
            {
                pizzaAppContext.OrderProducts.Add(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = item.Id
                });
            }

            pizzaAppContext.SaveChanges();

            //var tp = 
            //var order = new Order
            //{
            //    Name = "Random",
            //    TotalPrice = 
            //}

            //pizzaAppContext.Orders.Add()

            await cartService.Clear(userId);

            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> DetailAsync(int id)
        {



            var pdm = 
                await pizzaAppContext.OrderProducts
                    .Include(op => op.Product)
                        .ThenInclude(p => p.ProductDetail)
                    .AsNoTracking()
                    .Where(pd => pd.OrderId == id)
                    .ToListAsync();
            //_cartService.AddOrUpdate(1, HttpContext.User.Identity.Name, pdm);

            return View(pdm);
        }

        public async Task<IActionResult> List(int? status)
        {
            IQueryable<Order> orders = pizzaAppContext.Orders.AsNoTracking();
            if (status != null && status != -1)
            {
                orders = orders.Where(p => p.Status == status);
            }

            var statusList = new List<StatusModel>
            {
                new StatusModel
                {
                    Id = 1,
                    Name = "Nazvanie 1"
                },
                new StatusModel
                {
                    Id = 2,
                    Name = "Nazvanie 2"
                }
            };

            statusList.Insert(0, new StatusModel { Name = "Все", Id = -1 });

            OrderListViewModel viewModel = new OrderListViewModel
            {
                Orders = await orders.ToListAsync(),
                Statuses = new SelectList(statusList, "Id", "Name"),
                CurrentStatus = status.HasValue ? status.Value : -1
            };

            return View(viewModel);






            //var orders = await pizzaAppContext.Orders.AsNoTracking().ToListAsync();

            ////var pdm =
            ////    await pizzaAppContext.OrderProducts
            ////        .Include(op => op.Product)
            ////            .ThenInclude(p => p.ProductDetail)
            ////        .AsNoTracking()
            ////        .Where(pd => pd.OrderId == id)
            ////        .ToListAsync();

            ////_cartService.AddOrUpdate(1, HttpContext.User.Identity.Name, pdm);



            //return View(orders);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = pizzaAppContext.Orders.AsNoTracking().FirstOrDefault(o => o.Id == id);



            return View(new OrderEditViewModel
            {
                Id = order.Id,
                Status = order.Status,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderEditViewModel model)
        {
            //model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var order = pizzaAppContext.Orders.FirstOrDefault(o => o.Id == model.Id);

                order.Status = model.Status;

                pizzaAppContext.SaveChanges();


                return RedirectToAction("List", "Order");


                //var user = new User
                //{
                //    Email = model.Email,
                //    UserName = model.Username,
                //};

                //var result = await _userManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    await _signInManager.SignInAsync(user, false);

                //    //_emailService.Send(model.Email, EmailResource.Subject, EmailResource.Message);

                //    return RedirectToAction("Index", "Home");
                //}

                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }

            return View(model);
        }

    }

    public class StatusModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class SimpleTestClass
    {
        public int OrderType { get; set; }

        public string Address { get; set; }

        //public string Time { get; set; }

        public int PaymentType { get; set; }

        public string Comment { get; set; }
    }
}
