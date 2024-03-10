using StudentManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StudentViewModel(Student student)
        {
            Id = student.Id;
            Name = student.Name;
        }
    }
    public  class CheckBoxTable
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public  bool isChecked { get; set; }
    }
  
}
