using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.BLL.Services;
using StudentManagementSystem.Models;
using StudentManagementSystem.UI.Filters;

namespace StudentManagementSystem.UI.Controllers
{
    [RoleAuthorize(1)]
    public class UsersController : Controller
    {
        private IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index(int pageNo=1,int pageSize=10)
        {
            return View(_accountService.GetAllTeacher(pageNo,pageSize));
        }
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(UserViewModel vm)
        {
            bool success = _accountService.AddTeacher(vm);
            if (success)
            {
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}
