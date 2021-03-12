using Masny.Food.App.Extensions;
using Masny.Food.App.ViewModels;
using Masny.Food.Data.Models;
using Masny.Food.Logic.Interfaces;
using Masny.Food.Logic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Masny.Food.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IProfileManager _profileManager;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IProfileManager profileManager)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _profileManager = profileManager ?? throw new ArgumentNullException(nameof(profileManager));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

                    var createdUser = await _userManager.FindByNameAsync(user.UserName);
                    await _profileManager.CreateProfileAsync(createdUser.Id, model.Name);

                    // UNDONE: add email service

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
                var result =
                    await _signInManager.PasswordSignInAsync(
                        model.Username,
                        model.Password,
                        model.RememberMe,
                        false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                // UNDONE: use resources files

                ModelState.AddModelError(string.Empty, "Incorrect data");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profileDto = await _profileManager.GetProfileByUserIdAsync(User.GetUserIdByClaimsPrincipal());

            var profileViewModel = new ProfileViewModel
            {
                Name = profileDto.Name,
                BirthDate = profileDto.BirthDate,
                Gender = profileDto.Gender,
                Address = profileDto.Address,
                Avatar = profileDto.Avatar,
            };

            return View(profileViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var profileDto = new ProfileDto
                {
                    UserId = User.GetUserIdByClaimsPrincipal(),
                    Name = model.Name,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    Address = model.Address,
                };

                if (model.AvatarFile is not null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(model.AvatarFile.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.AvatarFile.Length);
                    }
                    profileDto.Avatar = imageData;
                }

                await _profileManager.UpdateProfileAsync(profileDto);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
