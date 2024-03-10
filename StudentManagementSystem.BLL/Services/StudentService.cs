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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddStudentAsync(AddStudentViewModel vm)
        {
            try
            {
                Student obj = vm.ConvertToModel(vm);
                await _unitOfWork.GenericRepository<Student>().AddAsync(obj);
                _unitOfWork.Save();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
          
        }

        public IEnumerable<StudentViewModel> GetAll()
        {
            try
            {
                var students = _unitOfWork.GenericRepository<Student>().GetAll().ToList();
                List<StudentViewModel> list = new List<StudentViewModel>();
                list = ListInfo(students);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public PagedResult<StudentsDetailsViewModel> GetAllStudentDetails(int pageNo,int pageSize)
        {
            try
            {
                int excludeRecords = (pageSize * pageNo) - pageSize;
                List<StudentsDetailsViewModel> studentDetailsViewModel = new List<StudentsDetailsViewModel>();
                var studentList = _unitOfWork.GenericRepository<Student>()
                    .GetAll().Skip(excludeRecords).Take(pageSize).ToList();

                studentDetailsViewModel = ConvertToStudentDetailsVM(studentList);
                var pagedResult = new PagedResult<StudentsDetailsViewModel>
                {
                    Data = studentDetailsViewModel,
                    TotalItems = _unitOfWork.GenericRepository<Student>()
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

        public StudentProfileViewModel GetById(int studentId)
        {
            var student= _unitOfWork.GenericRepository<Student>().GetById(studentId);
            var studentProfile = new StudentProfileViewModel(student);
            return studentProfile;
        }

        public IEnumerable<ResultViewModel> GetExamResults(int studentId)
        {
            try
            {
                var examResults = _unitOfWork.GenericRepository<ExamResult>().GetAll().Where(x => x.StudentId == studentId);
                var students = _unitOfWork.GenericRepository<Student>().GetAll();
                var exams = _unitOfWork.GenericRepository<Exams>().GetAll();
                var QueAns = _unitOfWork.GenericRepository<QueAns>().GetAll();

                var requiredData = examResults.Join(students, er => er.StudentId, s => s.Id, (er, st) => new {er,st})
                    .Join(exams,erj=>erj.er.ExamId, ex => ex.Id, (erj, ex) => new {erj, ex })
                    .Join(QueAns,exj=>exj.erj.er.QueAnsId,q=>q.Id,(exj,q)=>new ResultViewModel()
                    {
                        StudentId= studentId,
                        ExamName=exj.ex.Title,
                        TotalQuestion=examResults.Count(a=>a.StudentId==studentId && a.ExamId==exj.ex.Id),
                        CorrectAnswer=examResults.Count(a=>a.StudentId==studentId&&a.ExamId==exj.ex.Id && a.Answer==q.Answer),
                        WrongAnswer=examResults.Count(a=>a.StudentId==studentId&&a.ExamId==exj.ex.Id && a.Answer!=q.Answer)

                    });
                return requiredData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<StudentViewModel> GetStudentsByGroupId(int groupId)
        {
            return _unitOfWork.GenericRepository<Student>()
                  .GetAll(s => s.GroupsId == groupId)
                  .Select(s => new StudentViewModel(s))
                  .ToList();
        }

        public bool SetExamResult(AttendExamViewModel vm)
        {
            try
            {
                foreach (var item in vm.qnAList)
                {
                    ExamResult result = new ExamResult();
                    result.StudentId = vm.StudentId;
                    result.ExamId = item.ExamId;
                    result.QueAnsId = item.Id;
                    result.Answer = item.Answer;
                    _unitOfWork.GenericRepository<ExamResult>().Add(result);
                    _unitOfWork.Save();
                    return true;

                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
           
        }

        public bool SetGroupIdToStudent(GroupStudentViewModel vm)
        {
            try
            {
                foreach (var item in vm.StudentList)
                {
                    var student = _unitOfWork.GenericRepository<Student>().GetById(item.Id);
                    if (item.isChecked)
                    {
                        student.GroupsId= vm.GroupId;
                        _unitOfWork.GenericRepository<Student>().Update(student);
                    }
                    else
                    {
                        student.GroupsId =null;
                    }
                }
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateProfile(StudentProfileViewModel vm)
        {
            try
            {
                var student = _unitOfWork.GenericRepository<Student>().GetById(vm.StudentId);
                if (student != null)
                {
                    student.Name = vm.Name;
                    student.Contact = vm.Contact;
                    student.ProfilePicture = vm.ProfilePicture != null ? vm.ProfilePicture : student.ProfilePicture;
                    student.CVFileName = vm.CVFileName != null ? vm.CVFileName : student.CVFileName;

                    _unitOfWork.GenericRepository<Student>().Update(student);
                    _unitOfWork.Save();
                }
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        private List<StudentsDetailsViewModel> ConvertToStudentDetailsVM(List<Student> studentList)
        {
            return studentList.Select(x => new StudentsDetailsViewModel(x)).ToList();
        }
        private List<StudentViewModel> ListInfo(List<Student> studentList)
        {
            return studentList.Select(x => new StudentViewModel(x)).ToList();
        }

      
    }
}
