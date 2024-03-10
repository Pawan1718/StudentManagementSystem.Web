using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Data.Entities
{
    public class Exams
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int GroupsId { get; set; }
        public virtual Groups Groups { get; set; }

        public virtual ICollection<ExamResult> ExamResults { get; set; }
        public virtual ICollection<QueAns> QueAns { get; set; }
    }
}
