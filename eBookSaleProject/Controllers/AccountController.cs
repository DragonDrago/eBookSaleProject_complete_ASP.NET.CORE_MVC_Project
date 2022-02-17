 using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using eBookSaleProject.Models;
using eBookSaleProject.Data;
using eBookSaleProject.Data.ViewModels;
using eBookSaleProject.Data.Static;
using Microsoft.EntityFrameworkCore;

namespace eBookSaleProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext appDbContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appDbContext = appDbContext;
        }

        public async Task<IActionResult> Users()
        {
            var users = await appDbContext.Users.ToListAsync();
            return View(users);
        }
         
        public IActionResult Login()=> View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if(user != null)
            {
                var passwordCheck = await userManager.CheckPasswordAsync(user,loginViewModel.Password); 
                if (passwordCheck)
                {
                    var result = await signInManager.PasswordSignInAsync(user,loginViewModel.Password,false,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Book");
                    }
                }
                TempData["Error"] = "Wrong Credentials. Please, try again! ";
                return View(loginViewModel);
            }
            TempData["Error"] = "Wrong Credentials. Please, try again! ";
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var user = await userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address already exist";
                return View(registerViewModel);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerViewModel.FullName,
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await userManager.CreateAsync(newUser,registerViewModel.Password);
            if (newUserResponse.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser,UserRoles.User);
            }
            
            //When the response sends an error
            if (newUserResponse.Errors.Count() > 0 )
            {
                List<string> errorList = newUserResponse.Errors.Select(x => x.Description).ToList();
                TempData["Error"] = string.Join("<br/>", errorList);
                return View(registerViewModel);
            }
            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Book");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
