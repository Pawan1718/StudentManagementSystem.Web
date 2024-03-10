using StudentManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
     public class StudentAttendanceViewModel
     {
            public int StudentId { get; set; }

            [Display(Name = "Student Name")]
            public string StudentName { get; set; }

            public bool IsPresent { get; set; }
     }


   
}
