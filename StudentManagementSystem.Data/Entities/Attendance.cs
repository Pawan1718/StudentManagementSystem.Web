//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace StudentManagementSystem.Data.Entities
//{
//    public class Attendance
//    {
//        public int Id { get; set; }
//        public DateTime Year { get; set; }
//        public DateTime Month { get; set; }
//        public DateTime Date { get; set; }
//        public bool IsPresent { get; set; }
//        public int TotalPresent { get; set; }
//        public int TotalAbsent { get; set; }

//        [Required]
//        [ForeignKey("Student")]
//        public int StudentId { get; set; }

//        public virtual Student Student { get; set; }

//        [Required]
//        [ForeignKey("Group")]
//        public int GroupId { get; set; }

//        public virtual Group Group { get; set; }
//    }
//}
