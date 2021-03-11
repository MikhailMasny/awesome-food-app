using Masny.Food.App.Extensions;
using Masny.Food.App.ViewModels;
using Masny.Food.Data.Contexts;
using Masny.Food.Data.Enums;
using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// TODO: ordercontroller, deliveryaddresses, js to delete from cart

namespace Masny.Food.App.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderManager _orderManager;
        private readonly IProductManager _productManager;
        private readonly FoodAppContext foodAppContext;

        public OrderController(
            ICartService cartService,
            FoodAppContext foodAppContext,
            IOrderManager orderManager,
            IProductManager productManager)
        {
            this.foodAppContext = foodAppContext;

            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _orderManager = orderManager ?? throw new ArgumentNullException(nameof(orderManager));
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
        }

        [Authorize]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.GetUserIdByClaimsPrincipal();

                // UNDONE: need method get order number
                var orderNumber = 1;
                var dateTimeNow = DateTime.Now;
                var lastOrder = await foodAppContext.Orders
                    .AsNoTracking()
                    .OrderBy(o => o.Id)
                    .LastOrDefaultAsync();

                if (lastOrder is not null && lastOrder.Creation.Date == dateTimeNow.Date)
                {
                    orderNumber = ++lastOrder.Number;
                }

                // UNDONE: need method get totalPrice
                var cartDto = await _cartService.GetAsync(userId);

                var orderDto = new OrderDto
                {
                    Number = orderNumber,
                    Creation = dateTimeNow,
                    UserId = userId,
                    Name = model.Name,
                    Phone = model.Phone,
                    InPlace = model.InPlace,
                    Address = model.Address,
                    PromoCode = model.PromoCode,
                    TotalPrice = await _productManager.GetTotalPriceByProductIds(cartDto.ProductIds), // UNDONE: to calc service
                    Comment = model.Comment,
                    Status = StatusType.Todo,
                };

                var orderId = await _orderManager.CreateOrderAsync(orderDto);

                await _orderManager.CreateOrderProductsAsync(orderId, cartDto.ProductIds);

                await _cartService.ClearAsync(userId);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Cart");
        }

        [Authorize]
        public async Task<IActionResult> History()
        {
            var orderDtos = await _orderManager.GetOrdersByUserId(User.GetUserIdByClaimsPrincipal());

            var orderViewModels = new List<OrderViewModel>();
            foreach (var orderDto in orderDtos)
            {
                orderViewModels.Add(new OrderViewModel
                {
                    Number = orderDto.Number,
                    Creation = orderDto.Creation,
                    Name = orderDto.Name,
                    Phone = orderDto.Phone,
                    InPlace = orderDto.InPlace,
                    Address = orderDto.Address,
                    PromoCode = orderDto.PromoCode,
                    TotalPrice = orderDto.TotalPrice,
                    Comment = orderDto.Comment,
                    Status = orderDto.Status,
                });
            }

            return View(orderViewModels);
        }






        [Authorize]
        public async Task<IActionResult> List(int? status)
        {
            IQueryable<Order> orders = foodAppContext.Orders.AsNoTracking();
            if (status != null && status != -1)
            {
                orders = orders.Where(p => p.Status == (StatusType)status);
            }

            var statusList = new List<StatusModel>
            {
                new StatusModel
                {
                    Id = 0,
                    Name = "Nazvanie 0"
                },
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



        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = foodAppContext.Orders.AsNoTracking().FirstOrDefault(o => o.Id == id);



            return View(new OrderEditViewModel
            {
                Id = order.Id,
                Status = (int)order.Status,
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(OrderEditViewModel model)
        {
            //model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var order = foodAppContext.Orders.FirstOrDefault(o => o.Id == model.Id);

                order.Status = (StatusType)model.Status;

                foodAppContext.SaveChanges();


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
