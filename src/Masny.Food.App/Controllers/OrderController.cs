using Masny.Food.App.Extensions;
using Masny.Food.App.Models;
using Masny.Food.App.ViewModels;
using Masny.Food.Data.Enums;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masny.Food.App.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;
        private readonly ICartService _cartService;
        private readonly ICalcService _calcService;

        public OrderController(
            IOrderManager orderManager,
            ICartService cartService,
            ICalcService calcService)
        {
            _orderManager = orderManager ?? throw new ArgumentNullException(nameof(orderManager));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _calcService = calcService ?? throw new ArgumentNullException(nameof(calcService));
        }

        [Authorize]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.GetUserIdByClaimsPrincipal();
                var dateTimeNow = DateTime.Now;
                var orderNumber = await _calcService.GetNewOrderNumberAsync(dateTimeNow);
                var cartDto = await _cartService.GetAsync(userId);

                var totalPrice = await _calcService.GetTotalPriceByProductIdsAsync(cartDto.ProductIds);

                if (!string.IsNullOrEmpty(model.PromoCode))
                {
                    totalPrice = await _calcService.ApplyPromoCodeAsync(model.PromoCode, totalPrice);
                }

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
                    Payment = model.Payment,
                    TotalPrice = totalPrice,
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

            return View(orderViewModels.OrderByDescending(o => o.Creation));
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
                    Payment = orderDto.Payment,
                    TotalPrice = orderDto.TotalPrice,
                    Comment = orderDto.Comment,
                    Status = orderDto.Status,
                });
            }

            var orderListViewModel = new OrderListViewModel
            {
                Orders = orderViewModels.OrderByDescending(o => o.Creation),
                Statuses = GetStatusTypes(true),
                Phone = phone,
                CurrentStatus = status ?? (int)StatusType.Unknown
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
                Statuses = GetStatusTypes(false),
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CheckPromoCodeAsync([FromBody] PromoCodeViewModel model)
        {
            var isExist = await _calcService.IsExistPromoCodeAsync(model.Value.ToUpper());
            if (isExist)
            {
                return Ok();
            }

            return NotFound();
        }

        private SelectList GetStatusTypes(bool withDefaultStatusModel)
        {
            var statusList = new List<SelectListModel>
            {
                new SelectListModel
                {
                    Id = 0,
                    Name = StatusType.Todo.ToString(),
                },
                new SelectListModel
                {
                    Id = 1,
                    Name = StatusType.InProgress.ToString(),
                },
                new SelectListModel
                {
                    Id = 2,
                    Name = StatusType.Done.ToString(),
                },
                new SelectListModel
                {
                    Id = 3,
                    Name = StatusType.Canceled.ToString(),
                }
            };

            if (withDefaultStatusModel)
            {
                statusList.Insert(0, new SelectListModel { Name = "All", Id = -1 });
            }

            return new SelectList(statusList, "Id", "Name");
        }
    }
}
