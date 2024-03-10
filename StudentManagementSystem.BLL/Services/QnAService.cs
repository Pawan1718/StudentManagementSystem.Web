using Microsoft.AspNetCore.Http.HttpResults;
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
    public class QnAService : IQnAService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QnAService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddQnA(CreateQueAnsViewModel vm)
        {
            try
            {
                var model = vm.ConvertToModel(vm);
                _unitOfWork.GenericRepository<QueAns>().Add(model);
                _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public PagedResult<QnAViewModel> GetAll(int pageNo, int pageSize)
        {
            try
            {
                int excludeRecords = (pageSize * pageNo) - pageSize;
                List<QnAViewModel> qnAViewModel = new List<QnAViewModel>();

                var QnAList = _unitOfWork.GenericRepository<QueAns>()
                    .GetAll().Skip(excludeRecords).Take(pageSize).ToList();

                qnAViewModel = ListInfo(QnAList);
                var pagedResult = new PagedResult<QnAViewModel>
                {
                    Data = qnAViewModel,
                    TotalItems = _unitOfWork.GenericRepository<QueAns>()
                    .GetAll().Count(),
                    PageNo= pageNo,
                    PageSize = pageSize
                };
                return pagedResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

            public IEnumerable<QnAViewModel> GetAllByExamId(int examId)
            {
                var qNA=_unitOfWork.GenericRepository<QueAns>().GetAll()
                    .Where(x=>x.ExamId == examId).ToList();
            if (qNA.Count == 0)  
            {
                return new List<QnAViewModel>();
            }

            return ListInfo(qNA);
            }

        public bool isAttendExam(int examId, int studentId)
        {
            var result = _unitOfWork.GenericRepository<ExamResult>()
                .GetAll().Any(x => x.ExamId == examId && x.StudentId == studentId);
            return result==false ? false: true;
        }

        private List<QnAViewModel> ListInfo(List<QueAns> QnAList)
        {
            return QnAList.Select(x => new QnAViewModel(x)).ToList();
        }
    }
}
