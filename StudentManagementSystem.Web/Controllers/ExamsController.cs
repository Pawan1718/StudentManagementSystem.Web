using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.BLL.Services;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.UI.Controllers
{
    public class ExamsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IExamService _examService;

        public ExamsController(IGroupService groupService, IExamService examService)
        {
            _groupService = groupService;
            _examService = examService;
        }

        public IActionResult Index(int pageNo = 1, int pageSize = 10)
        {
            if (_examService != null)
            {
                var exams = _examService.GetAll(pageNo, pageSize);
                if (exams != null)
                {
                    return View(exams);
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }



        public IActionResult CreateExam()
        {
            var groups = _groupService.GetAllGroups();
            ViewBag.AllGroups = new SelectList(groups, "Id", "Name");

            return View();
        }



        [HttpPost]
        public IActionResult CreateExam(CreateExamsViewModel vm)
        {
           
            if (ModelState.IsValid)
            {
                _examService.AddExam(vm);
                TempData["success"] = "Exam Created successfully";
                return View(vm);
            }
           TempData["error"] = "Failed";
           return View();
        }
    }

}
