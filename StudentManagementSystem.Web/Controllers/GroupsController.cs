using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.BLL.Services;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.UI.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IStudentService studentService;

        public GroupsController(IGroupService _groupService, IStudentService _studentService)
        {
            groupService = _groupService;
            studentService = _studentService;
        }
        public IActionResult Index(int pageNo=1,int pageSize=10)
        { 
            return View(groupService.GetAll(pageNo,pageSize));
        }
        public IActionResult AddGroup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddGroup(GroupViewModel vm)
        {
           var group= groupService.AddGroup(vm);  
            return RedirectToAction("Index");
        }
        public IActionResult GroupDetails(int id)
        {
            GroupStudentViewModel vm=new GroupStudentViewModel();
            var group = groupService.GetGroup(id);
            var students = studentService.GetAll();
            vm.GroupId= group.Id;
            foreach (var student in students)
            {
                vm.StudentList.Add(new CheckBoxTable
                {
                    Id = student.Id,
                    Name = student.Name,
                    isChecked = false
                });
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult GroupDetails(GroupStudentViewModel vm)
        {
            bool result = studentService.SetGroupIdToStudent(vm);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}
