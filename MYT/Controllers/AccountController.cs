using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MYT.Data.AppContext;
using MYT.Data.Entity;
using MYT.ViewModels;

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
                    return RedirectToAction("Index", "Home");
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

        //[HttpPost]
        //public async Task<IActionResult> Login()
        //{
        //    return
        //}
    }
}