using Masny.Food.App.ViewModels;
using Masny.Food.Data.Contexts;
using Masny.Food.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Masny.Food.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly FoodAppContext foodAppContext;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            FoodAppContext foodAppContext)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.foodAppContext = foodAppContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Username,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    //_emailService.Send(model.Email, EmailResource.Subject, EmailResource.Message);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var signInViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(signInViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                //ModelState.AddModelError(string.Empty, AccountResource.IncorrectData);
                ModelState.AddModelError(string.Empty, "Неверные данные");
            }
            return View(model);
        }

        public IActionResult OrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderHistory = foodAppContext.Orders.AsNoTracking().Where(o => o.UserId == userId).ToList();

            return View(orderHistory);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfileAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await foodAppContext.Profiles.AsNoTracking().FirstAsync(p => p.UserId == userId);

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var profile = await foodAppContext.Profiles.FirstAsync(p => p.UserId == userId);

                profile.Name = model.FullName;
                profile.BirthDate = model.BirthDate;

                foodAppContext.SaveChanges();

                return RedirectToAction("Index", "Home");

                //var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                //if (result.Succeeded)
                //{
                //    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                //    {
                //        return Redirect(model.ReturnUrl);
                //    }

                //    return RedirectToAction("Account", "Profile");
                //}

                //ModelState.AddModelError(string.Empty, AccountResource.IncorrectData);
                ModelState.AddModelError(string.Empty, "Неверные данные");
            }
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

