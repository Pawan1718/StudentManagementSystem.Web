using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Services
{
    public interface IAccountService
    {
        bool AddTeacher(UserViewModel vm);
        LoginViewModel Login(LoginViewModel loginViewModel);
        PagedResult<TeacherViewModel> GetAllTeacher(int pageNo,int pageSize);
    }
}
