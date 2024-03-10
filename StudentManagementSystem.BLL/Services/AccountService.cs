using StudentManagementSystem.Data.Entities;
using StudentManagementSystem.Data.UnitOfWorks;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddTeacher(UserViewModel vm)
        {
            try
            {
                Users model = new Users
                {
                    Name = vm.Name,
                    UserName = vm.UserName,
                    Password = vm.Password,
                    Role = (int)EnumRoles.Teacher
                };
                unitOfWork.GenericRepository<Users>().Add(model);
                unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            };
            return true;
        }

        public PagedResult<TeacherViewModel> GetAllTeacher(int pageNo, int pageSize)
        {
            try
            {

                int excludeRecords = (pageSize * pageNo) - pageSize;
                List<TeacherViewModel> teacherViewModel = new List<TeacherViewModel>();
                var usersList = unitOfWork.GenericRepository<Users>()
                    .GetAll().Where(x => x.Role == (int)EnumRoles.Teacher)
                    .Skip(excludeRecords).Take(pageSize).ToList();

                teacherViewModel = ListInfo(usersList);
                var pagedResult = new PagedResult<TeacherViewModel>
                {
                    Data = teacherViewModel,
                    TotalItems = unitOfWork.GenericRepository<Users>()
                    .GetAll().Where(x => x.Role == (int)EnumRoles.Teacher).Count(),
                    PageSize = pageSize
                };
                return pagedResult;
            }
            catch (Exception)
            {

                throw;
            } 
        }

        private List<TeacherViewModel> ListInfo(List<Users> usersList)
        {
            return usersList.Select(x=>new TeacherViewModel(x)).ToList();
        }

        public LoginViewModel Login(LoginViewModel loginViewModel)
        {
            if (loginViewModel.Role==(int)EnumRoles.Teacher || loginViewModel.Role==(int)EnumRoles.Admin)
            {
                var user = unitOfWork.GenericRepository<Users>().GetAll()
                            .FirstOrDefault(a => a.UserName == loginViewModel.UserName.Trim()
                            && a.Password == loginViewModel.Password
                            && a.Role == loginViewModel.Role);
                if (user != null)
                {
                    loginViewModel.Id = user.Id;
                    return loginViewModel;
                }
            }
            else
            {
                var user = unitOfWork.GenericRepository<Student>().GetAll()
                                           .FirstOrDefault(a => a.UserName == loginViewModel.UserName.Trim()
                                           && a.Password == loginViewModel.Password);
                if (user != null)
                {
                    loginViewModel.Id = user.Id;
                    return loginViewModel;
                }
            }
             return null;
        }
    }
}
