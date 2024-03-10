using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagementSystem.BLL.Interfaces;
using StudentManagementSystem.BLL.Services;
using StudentManagementSystem.Data.Entities;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.UI.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly IQnAService _qnAService;
        private readonly IUtilityService _utilityService;

        private string containerName = "StudentImage";
        private string CVcontainerName = "StudentCV";

        public StudentsController(IStudentService studentService, IExamService examService, IQnAService qnAService, IUtilityService utilityService)
        {
            _studentService = studentService;
            _examService = examService;
            _qnAService = qnAService;
            _utilityService = utilityService;
        }
        public IActionResult Index(int pageNo = 1, int pageSize = 10)
        {
            var stdDetails = _studentService.GetAllStudentDetails(pageNo, pageSize);
            return View(stdDetails);
        }
        public IActionResult StudentProfile()
        {
            var sessionObj = HttpContext.Session.GetString("loginDetails");
            if (sessionObj != null)
            {
                var loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(sessionObj);
                var studentDetails = _studentService.GetById(loginViewModel.Id);
                return View(studentDetails);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> StudentProfile(StudentProfileViewModel vm)
        {
            try
            {
                if (vm.ProfilePictureUrl != null)
                {
                    vm.ProfilePicture = await _utilityService.SaveImage(containerName, vm.ProfilePictureUrl);
                }
                if (vm.CVFileUrl != null)
                {
                    vm.CVFileName = await _utilityService.SaveImage(CVcontainerName, vm.CVFileUrl);
                }


                _studentService.UpdateProfile(vm);
                return RedirectToAction("StudentProfile");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DownloadCV(StudentProfileViewModel vm)
        {
            try
            {
                if (vm.CVFileName == null)
                {

                    return RedirectToAction("Error", "Home", new { errorMessage = "CV file name is null." });
                }

                string dbPath = Path.Combine(CVcontainerName, vm.CVFileName);
                byte[] fileContent = await _utilityService.DownloadCV(dbPath);
                return File(fileContent, "application/octet-stream", "CVFileName.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentViewModel vm)
        {
            var success = await _studentService.AddStudentAsync(vm);
            if (success > 0)
            {
                return RedirectToAction("Index");
            }
            return View(vm);
        }





        public IActionResult AttendExam()
        {
            var model = new AttendExamViewModel();
            string loginObj = HttpContext.Session.GetString("loginDetails");
            LoginViewModel sessionObj = JsonConvert.DeserializeObject<LoginViewModel>(loginObj);

            if (sessionObj != null)
            {
                model.StudentId = sessionObj.Id;
                var todayExam = _examService.GetAllExams()
                .Where(x => x.StartDate.Date == DateTime.Today.Date).FirstOrDefault();
                if (todayExam == null)
                {
                    model.Message = "No exam scheduled today";
                    return View(model);
                }
                else
                {
                    if (!_qnAService.isAttendExam(todayExam.Id, model.StudentId))
                    {
                        model.qnAList = _qnAService.GetAllByExamId(todayExam.Id).ToList();
                        model.ExamName = todayExam.Title;
                        model.Message = "";
                        return View(model);
                    }
                    else
                    {
                        model.Message = "You have already attend this exam";
                        return View(model);
                    }
                }
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public IActionResult AttendExam(AttendExamViewModel vm)
        {
            bool result = _studentService.SetExamResult(vm);

            return RedirectToAction();
        }




        public IActionResult Result()
        {
            var sessionObj = HttpContext.Session.GetString("loginDetails");
            if (sessionObj != null)
            {
                var loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(sessionObj);
                var model = _studentService.GetExamResults(Convert.ToInt32(loginViewModel.Id));
                return View(model);
            }
            return RedirectToAction("Login","Account");
        }

    }
}
