using Microsoft.AspNetCore.Http;
using StudentManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class AddStudentViewModel
    {
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
  

        public Student ConvertToModel(AddStudentViewModel vm)
        {
            return new Student
            {
                Name = vm.Name,
                UserName = vm.UserName,
                Password = vm.Password,
            };
        }
    }
}
