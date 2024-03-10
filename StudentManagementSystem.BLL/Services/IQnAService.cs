using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Services
{
    public interface IQnAService
    {
        void AddQnA(CreateQueAnsViewModel vm);
        PagedResult<QnAViewModel> GetAll(int pageNo,int pageSize);
        bool isAttendExam(int ExamId,int StudentId);
        IEnumerable<QnAViewModel> GetAllByExamId(int examId);

    }
}
