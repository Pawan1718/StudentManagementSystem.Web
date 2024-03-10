﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Data.Entities
{
    public class QueAns
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }

        public int ExamId { get; set; }
        public virtual Exams Exam { get; set; }

        public int Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    }
}
