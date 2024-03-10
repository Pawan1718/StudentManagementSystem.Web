using StudentManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class StudentsDetailsViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Student_UserName { get; set; }
        public string? Contact { get; set; }

        public StudentsDetailsViewModel(Student std)
        {
            StudentId = std.Id;
            StudentName=std.Name;
            Student_UserName=std.UserName;
            Contact = std.Contact;
        }
    }
}
