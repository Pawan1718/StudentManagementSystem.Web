using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Data.Entities
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int QueAnsId { get; set; }
        public QueAns QueAns { get; set; }
        public int ExamId { get; set; }
        public virtual Exams Exam { get; set; }
        public int Answer { get; set; }
    }
}
