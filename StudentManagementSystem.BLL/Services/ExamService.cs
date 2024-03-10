using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddExam(CreateExamsViewModel vm)
        {
            try
            {
                var model = vm.ConvertToModel(vm);
                if (model==null)
                {
                    return ;
                }
                _unitOfWork.GenericRepository<Exams>().Add(model);
                _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public PagedResult<ExamsViewModel> GetAll(int pageNo, int pageSize)
        {
            try
            {

                int excludeRecords = (pageSize * pageNo) - pageSize;
                List<ExamsViewModel> examViewModel = new List<ExamsViewModel>();
                var examList = _unitOfWork.GenericRepository<Exams>()
                    .GetAll().Skip(excludeRecords).Take(pageSize).ToList();

                examViewModel = ListInfo(examList);
                var pagedResult = new PagedResult<ExamsViewModel>
                {
                    Data = examViewModel,
                    TotalItems = _unitOfWork.GenericRepository<Exams>()
                    .GetAll().Count(),
                    PageSize = pageSize
                };
                return pagedResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ExamsViewModel> GetAllExams()
        {
            List<ExamsViewModel> examList=new List<ExamsViewModel>();
           var exams= _unitOfWork.GenericRepository<Exams>().GetAll().ToList();
            examList = ListInfo(exams);
            return examList;
        }

        private List<ExamsViewModel> ListInfo(List<Exams> examList)
        {
            return examList.Select(x => new ExamsViewModel(x)).ToList();
        }
    }
}
