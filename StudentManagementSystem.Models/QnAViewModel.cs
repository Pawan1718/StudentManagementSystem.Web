using StudentManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Models
{
    public class QnAViewModel
    {
        public  int Id { get; set; }
        public string QuestionTitle { get; set; }
        public int ExamId { get; set; }
        public int Answer { get; set; }
        public int SelectedAnswer { get; set; }

        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public QnAViewModel()
        {
        }
        public QnAViewModel(QueAns model)
        {
            Id = model.Id;
            QuestionTitle = model.QuestionTitle;
            ExamId = model.ExamId;
            Answer = model.Answer;
            Option1 = model.Option1;
            Option2 = model.Option2;
            Option3 = model.Option3;
            Option4 = model.Option4;
        }
    }
}
