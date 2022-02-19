using EmployeeCrudOperation.Models;
using EmployeeCrudOperation.Models.Entities;
using EmployeeCrudOperation.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeCrudOperation.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext context;
        private readonly UserManager<IdentityCustomUser> userManager;
        private readonly SignInManager<IdentityCustomUser> signInManager;

        public AccountController(ApplicationContext context,UserManager<IdentityCustomUser> userManager, SignInManager<IdentityCustomUser>signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUP()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUP(SignUpUserViewModel model)
        {
            var Name = model.FirstName;
            var check = context.Users.Where(x => x.FirstName == Name).ToList();
            if (ModelState.IsValid)
            {

                if (check.Count > 0)
                {
                    ViewBag.Duplicate = "Enter Name" + " " + Name + " " + "is already exists in Database!";
                    return View(model);

                }
                else
                {
                    var user = new IdentityCustomUser()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Gender = model.Gender,
                        Email = model.Gender,
                        UserName = model.Email,
                        PasswordHash = model.Password,
                        PhoneNumber = model.Phone
                    };

                    IdentityResult result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        ModelState.Clear();
                        ViewBag.message = "User Created";
                        return View();
                    }
                    else
                    {
                        foreach (var er in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, er.Description);
                        }
                        return View();
                    }
                }
            }
            else
            {
                return View(model);
            }
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
             return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult>  Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //success
                var user = context.Users.SingleOrDefault(e => e.UserName == model.Username);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false,false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "These credentials don't match our records!");
                        return View();
                    }
                }
               
            }
            else
            {
                return View(model);
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
