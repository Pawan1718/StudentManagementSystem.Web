using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.BLL.Services;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.UI.Controllers
{
    public class QnAController : Controller
    {
        private readonly IExamService _examService;
        private readonly IQnAService _qnAService;

        public QnAController(IExamService examService, IQnAService qnAService)
        {
            _examService = examService;
            _qnAService = qnAService;
        }
        public IActionResult Index(int pageNo = 1, int pageSize = 10)
        {
            var TotalQnAs = _qnAService.GetAll(pageNo,pageSize);

            return View(TotalQnAs);
        }
        public IActionResult CreateQnA()
        {
            var exams=_examService.GetAllExams();
            ViewBag.Exams = new SelectList(exams, "Id", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult  CreateQnA(CreateQueAnsViewModel vm)
        {
            if (ModelState.IsValid)
            {
               _qnAService.AddQnA(vm);
                TempData["success"] = "Question and Answer Created successfully";
                return View(vm);
            }
            TempData["error"] = "Failed";
            return View();
        }
    }
}
