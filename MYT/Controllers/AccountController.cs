using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MYT.Data.AppContext;
using MYT.Data.Entity;
using MYT.Data.Models;
using MYT.ViewModels;
using Newtonsoft.Json;

namespace MYT.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly AppDbContext _appDbContext;

        public AccountController(UserManager<DbUser> userManager, SignInManager<DbUser> signInManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = new UserProfile
                {
                    Name = model.Name,
                    RegistrationDate = DateTime.Now
                };

                DbUser user = new DbUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    UserProfile = userProfile
                };

                var rolename = "User";
                var result = await _userManager.CreateAsync(user, model.Password);
                result = _userManager.AddToRoleAsync(user, rolename).Result;


                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _appDbContext.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                ModelState.AddModelError("","This e-mail isn't register");
                return View();
            }

            var result = _signInManager
                .PasswordSignInAsync(user, model.Password, false, false).Result;

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Wrong password!");
                return View();
            }

            await _signInManager.SignInAsync(user, isPersistent: true);

            await Authenticate(model.Email);

            var userInfo = new UserInfo()
            {
                Id = user.Id,
                Email = user.Email
            };

            HttpContext.Session.SetString("SessionUserData", JsonConvert.SerializeObject(userInfo));

            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}