//using StudentManagementSystem.Data.Entities;
//using StudentManagementSystem.Data.UnitOfWorks;
//using StudentManagementSystem.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace StudentManagementSystem.BLL.Services
//{
//    public class AttendanceService : IAttendanceService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        public AttendanceService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }
//        public IEnumerable<StudentAttendanceViewModel> GetAllAttendances()
//        {
//            var attendances = _unitOfWork.GenericRepository<Attendance>().GetAll();
//            return MapToViewModels(attendances);
//        }

//        public IEnumerable<StudentAttendanceViewModel> GetAttendancesByMonthAndYear(DateTime month, DateTime year)
//        {
//            var attendances = _unitOfWork.GenericRepository<Attendance>()
//                .GetAll(a => a.Date.Month == month.Month && a.Date.Year == year.Year);
//            return MapToViewModels(attendances);
//        }

//        public StudentAttendanceViewModel GetAttendanceById(int id)
//        {
//            var attendance = _unitOfWork.GenericRepository<Attendance>().GetById(id);
//            return MapToViewModel(attendance);
//        }

//        public void AddAttendance(StudentAttendanceViewModel attendance)
//        {
//            var attendanceEntity = MapToEntity(attendance);
//            _unitOfWork.GenericRepository<Attendance>().Add(attendanceEntity);
//            _unitOfWork.Save();
//        }

//        public void UpdateAttendance(StudentAttendanceViewModel attendance)
//        {
//            var attendanceEntity = MapToEntity(attendance);
//            _unitOfWork.GenericRepository<Attendance>().Update(attendanceEntity);
//            _unitOfWork.Save();
//        }

//        public void DeleteAttendance(int id)
//        {
//            _unitOfWork.GenericRepository<Attendance>().DeleteById(id);
//            _unitOfWork.Save();
//        }

//        private StudentAttendanceViewModel MapToViewModel(Attendance attendance)
//        {
//            if (attendance == null)
//                return null;

//            return new StudentAttendanceViewModel
//            {
//                IsPresent = attendance.IsPresent,
//                StudentId = attendance.StudentId
//            };
//        }

//        private IEnumerable<StudentAttendanceViewModel> MapToViewModels(IEnumerable<Attendance> attendances)
//        {
//            return attendances.Select(a => MapToViewModel(a));
//        }

//        private Attendance MapToEntity(StudentAttendanceViewModel attendanceViewModel)
//        {
//            return new Attendance
//            {
//                IsPresent = attendanceViewModel.IsPresent,
//                StudentId = attendanceViewModel.StudentId
//            };
//        }
//    }
//}
