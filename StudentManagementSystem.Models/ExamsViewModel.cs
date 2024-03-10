using StudentManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class ExamsViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public double Time { get; set; }
        public int GroupsId { get; set; }

        public ExamsViewModel(Exams exams)
        {
               Id= exams.Id;
            Title= exams.Title;
            Description= exams.Description;
            StartDate= exams.StartDate;
            Time= exams.Time;
            GroupsId = exams.GroupsId;
        }
    }
}
