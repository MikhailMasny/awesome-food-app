using Masny.Food.App.Extensions;
using Masny.Food.App.Models;
using Masny.Food.App.ViewModels;
using Masny.Food.Common.Enums;
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
        private readonly IProductManager _productManager;
        private readonly IPromoCodeManager _promoCodeManager;
        private readonly ICartService _cartService;
        private readonly ICalcService _calcService;

        public OrderController(
            IOrderManager orderManager,
            IProductManager productManager,
            IPromoCodeManager promoCodeManager,
            ICartService cartService,
            ICalcService calcService)
        {
            _orderManager = orderManager ?? throw new ArgumentNullException(nameof(orderManager));
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
            _promoCodeManager = promoCodeManager ?? throw new ArgumentNullException(nameof(promoCodeManager));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _calcService = calcService ?? throw new ArgumentNullException(nameof(calcService));
        }

        [Authorize]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            var productIds = (await _cartService
                    .GetAsync(User.GetUserIdByClaimsPrincipal()))
                    .ProductIds;

            if (!productIds.Any())
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var dateTimeNow = DateTime.Now;

                var newOrderNumber = await _calcService
                    .GetNewOrderNumberAsync(
                        await _orderManager.GetLastAsync(),
                        dateTimeNow);

                var totalPrice = await _calcService
                    .GetTotalPriceAsync(
                        await _productManager
                            .GetAllProductsByIdsAsync(productIds),
                        await _promoCodeManager
                            .GetPromoCodeByCodeAsync(model.PromoCode));

                var orderDto = new OrderDto
                {
                    Number = newOrderNumber,
                    Creation = dateTimeNow,
                    UserId = User.GetUserIdByClaimsPrincipal(),
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

                await _orderManager.CreateOrderProductsAsync(
                    await _orderManager.CreateOrderAsync(orderDto),
                    productIds);

                await _cartService.ClearAsync(User.GetUserIdByClaimsPrincipal());

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Cart");
        }

        [Authorize]
        public async Task<IActionResult> History()
        {
            var orderDtos = await _orderManager
                .GetAllByUserIdAsync(User.GetUserIdByClaimsPrincipal());

            if (!orderDtos.Any())
            {
                return View(new List<OrderViewModel>());
            }

            IEnumerable<OrderViewModel> GetOrderViewModels()
            {
                foreach (var orderDto in orderDtos)
                {
                    yield return new OrderViewModel
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
                    };
                }
            }

            return View(GetOrderViewModels()
                .OrderByDescending(orderViewModel => orderViewModel.Creation));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List(int? status, string phone)
        {
            ViewBag.Statuses = GetStatusTypes();
            ViewBag.Phone = phone;
            ViewBag.Status = status ?? (int)StatusType.Unknown;

            var orderDtos = await _orderManager.GetAllAsync();
            if (!orderDtos.Any())
            {
                return View(new OrderListViewModel
                {
                    Orders = new List<OrderViewModel>(),
                });
            }

            if (status.HasValue && status != -1)
            {
                orderDtos = orderDtos
                    .Where(orderDto => orderDto.Status == (StatusType)status.Value);
            }

            if (!string.IsNullOrEmpty(phone))
            {
                orderDtos = orderDtos
                    .Where(orderDto => orderDto.Phone.Contains(phone));
            }

            IEnumerable<OrderViewModel> GetOrderViewModels()
            {
                foreach (var orderDto in orderDtos)
                {
                    yield return new OrderViewModel
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
                    };
                }
            }

            return View(new OrderListViewModel
            {
                CurrentStatus = status ?? (int)StatusType.Unknown,
                Orders = GetOrderViewModels()
                    .OrderByDescending(orderViewModel => orderViewModel.Creation),
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Statuses = GetStatusTypes(false);

            var orderDto = await _orderManager.GetByIdAsync(id);

            return View(new OrderEditViewModel
            {
                Id = orderDto.Id,
                Number = orderDto.Number,
                Status = (int)orderDto.Status,
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
        public async Task<IActionResult> CheckPromoCode([FromBody] PromoCodeViewModel model)
        {
            var promoCode = await _promoCodeManager.GetPromoCodeByCodeAsync(model.Value);
            return await _calcService.IsValidPromoCodeAsync(promoCode.Value)
                ? Ok()
                : NotFound();
        }

        private static SelectList GetStatusTypes(bool withDefaultStatusModel = true)
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
                statusList.Insert(0, new SelectListModel
                {
                    Name = "All",
                    Id = -1
                });
            }

            return new SelectList(statusList, "Id", "Name");
        }
    }
}
