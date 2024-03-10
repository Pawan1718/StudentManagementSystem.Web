//using Microsoft.AspNetCore.Mvc;
//using NuGet.Protocol;
//using StudentManagementSystem.BLL.Services;
//using StudentManagementSystem.Data.Entities;
//using StudentManagementSystem.Models;

//namespace StudentManagementSystem.UI.Controllers
//{
//    public class AttendanceController : Controller
//    {
//        private readonly IAttendanceService _attendanceService;
//        private readonly IGroupService _groupService;
//        private readonly IStudentService _studentService;
//        public AttendanceController(IAttendanceService attendanceService, IGroupService groupService, IStudentService studentService)
//        {
//            _attendanceService = attendanceService;
//            _groupService = groupService;
//            _studentService = studentService;
//        }
//        public IActionResult Index()
//        {
//            var groups = _groupService.GetAllGroups();

//            return View(groups);
//        }

//        public IActionResult TrackAttendance(int groupId)
//        {
//            var group = _groupService.GetGroup(groupId);

//            if (group == null)
//            {
//                return NotFound();
//            }

//            var students = _studentService.GetStudentsByGroupId(groupId);

//            var attendanceViewModels = students.Select(s => new StudentAttendanceViewModel
//            {
//                StudentId = s.Id,
//                StudentName = s.Name,
//                IsPresent = false 
//            }).ToList();

//            ViewBag.GroupName = group.Name;
//            ViewBag.GroupId = group.Id;

//            return View(attendanceViewModels);
//        }

//        [HttpPost]
//        public IActionResult TrackAttendance(int groupId, List<StudentAttendanceViewModel> attendanceViewModels)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    foreach (var viewModel in attendanceViewModels)
//                    {
//                        var attendance = new Attendance
//                        {
//                            Date = DateTime.Today,
//                            IsPresent = viewModel.IsPresent,
//                            StudentId = viewModel.StudentId,
//                            GroupId = groupId
//                        };

//                        _attendanceService.AddAttendance(attendance);
//                    }

//                    return RedirectToAction(nameof(Index));
//                }
//                catch (Exception ex)
//                {
//                    ModelState.AddModelError("", $"Error tracking attendance: {ex.Message}");
//                }
//            }

//            return View(attendanceViewModels);
//        }
//    }
//}
