using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.BLL.Services;
using StudentManagementSystem.Models;
using StudentManagementSystem.UI.Filters;
using System.Security.Claims;
using System.Text.Json;

namespace StudentManagementSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
           LoginViewModel vm= accountService.Login(model);
            if (vm!=null)
            {
                string sessionObj = JsonSerializer.Serialize(vm);
                HttpContext.Session.SetString("loginDetails", sessionObj);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                };
                var claimsIdentity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                    ,new ClaimsPrincipal(claimsIdentity));
                return RedirectToUser(vm);

            }
            return View(model);
        }

        private IActionResult RedirectToUser(LoginViewModel vm)
        {
            if (vm.Role==(int)EnumRoles.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (vm.Role == (int)EnumRoles.Teacher)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("StudentProfile", "Students");

            }
        }
    }
}
