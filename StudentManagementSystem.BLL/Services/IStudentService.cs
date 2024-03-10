using StudentManagementSystem.Data.Entities;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Services
{
    public interface IStudentService
    {
        PagedResult<StudentsDetailsViewModel> GetAllStudentDetails(int pageNo,int pageSize);
        Task<int> AddStudentAsync(AddStudentViewModel vm);
        IEnumerable<StudentViewModel> GetAll();
        IEnumerable<ResultViewModel> GetExamResults(int studentId);
        bool SetExamResult(AttendExamViewModel vm);
        bool SetGroupIdToStudent(GroupStudentViewModel vm);
        StudentProfileViewModel GetById(int studentId);
        void UpdateProfile(StudentProfileViewModel vm);
        IEnumerable<StudentViewModel> GetStudentsByGroupId(int groupId);

    }
}
