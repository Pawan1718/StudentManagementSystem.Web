using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Exams> Exams { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<QueAns> QueAns { get; set; }
       // public DbSet<Attendance> Attendances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamResult>(entity =>
            {
                entity.HasOne(d => d.Exam)
                .WithMany(p => p.ExamResults)
                .HasForeignKey(x => x.ExamId)
                .HasConstraintName("FK_ExamResults_Exams");

                entity.HasOne(d => d.QueAns)
                .WithMany(m => m.ExamResults)
                .HasForeignKey(x => x.QueAnsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamResults_QueAns");

                entity.HasOne(d => d.Student)
                .WithMany(x => x.ExamResults)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamResults_Users");

            });

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, Name = "Admin", Password = "Admin@123", Role = 1, UserName = "Admin" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
