using Masny.Food.App.Extensions;
using Masny.Food.App.Models;
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
            var orderDtos = await _orderManager.GetOrdersByUserIdAsync(User.GetUserIdByClaimsPrincipal());

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
        public async Task<IActionResult> List(int? status, string phone)
        {
            var orderDtos = await _orderManager.GetAllOrdersAsync();
            if (status != null && status != -1)
            {
                orderDtos = orderDtos.Where(p => p.Status == (StatusType)status);
            }

            if (!string.IsNullOrEmpty(phone))
            {
                orderDtos = orderDtos.Where(p => p.Phone == phone);
            }

            var orderViewModels = new List<OrderViewModel>();
            foreach (var orderDto in orderDtos)
            {
                orderViewModels.Add(new OrderViewModel
                {
                    Id = orderDto.Id,
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

            var orderListViewModel = new OrderListViewModel
            {
                Orders = orderViewModels.OrderByDescending(o => o.Creation),
                Statuses = GetStatuses(true),
                Phone = phone,
                CurrentStatus = status.HasValue 
                    ? status.Value 
                    : (int)StatusType.Unknown
            };

            return View(orderListViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            var orderDto = await _orderManager.GetOrderByIdAsync(id);

            return View(new OrderEditViewModel
            {
                Id = orderDto.Id,
                Number = orderDto.Number,
                Status = (int)orderDto.Status,
                Statuses = GetStatuses(false),
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(OrderEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderManager.UpdateOrderStatusByIdAsync(model.Id, (StatusType)model.Status);

                return RedirectToAction("List", "Order");
            }

            return View(model);
        }

        private SelectList GetStatuses(bool withDefaultStatusModel)
        {
            var statusList = new List<StatusModel>
            {
                new StatusModel
                {
                    Id = 0,
                    Name = StatusType.Todo.ToString(),
                },
                new StatusModel
                {
                    Id = 1,
                    Name = StatusType.InProgress.ToString(),
                },
                new StatusModel
                {
                    Id = 2,
                    Name = StatusType.Done.ToString(),
                }
            };

            if (withDefaultStatusModel)
            {
                statusList.Insert(0, new StatusModel { Name = "All", Id = -1 });
            }

            return new SelectList(statusList, "Id", "Name");
        }
    }
}
