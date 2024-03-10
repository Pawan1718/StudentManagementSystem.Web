using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Services
{
    public interface IExamService
    {
        PagedResult<ExamsViewModel> GetAll(int PageNumber,int pageSize);
        void AddExam(CreateExamsViewModel vm);
        IEnumerable<ExamsViewModel> GetAllExams();
    }
}
