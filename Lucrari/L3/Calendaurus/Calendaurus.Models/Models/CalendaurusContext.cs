using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Calendaurus.Models.Models;

public partial class CalendaurusContext : DbContext
{
    public CalendaurusContext()
    {
    }

    public CalendaurusContext(DbContextOptions<CalendaurusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<PracticalLesson> PracticalLessons { get; set; }

    public virtual DbSet<PracticalLessonEvent> PracticalLessonEvents { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<SysAdmin> SysAdmins { get; set; }

    public virtual DbSet<TheoreticalLesson> TheoreticalLessons { get; set; }

    public virtual DbSet<TheoreticalLessonEvent> TheoreticalLessonEvents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL2016;Database=Calendaurus;Trusted_Connection=True;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<PracticalLesson>(entity =>
        {
            entity.Property(e => e.Description).HasComment("A short description on what the students will be doing at this practical lesson");

            entity.HasOne(d => d.Discipline).WithMany(p => p.PracticalLessons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PracticalLesson_Discipline");
        });

        modelBuilder.Entity<PracticalLessonEvent>(entity =>
        {
            entity.Property(e => e.Occurance).HasComment("An enum which describes the occurance. ex:1-weekly/2-bi-weekly");

            entity.HasOne(d => d.PracticalLesson).WithMany(p => p.PracticalLessonEvents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PracticalLessonEvent_PracticalLesson");

            entity.HasMany(d => d.Students).WithMany(p => p.PracticalLessonEvens)
                .UsingEntity<Dictionary<string, object>>(
                    "PracticalLessonEventStudentAttendance",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PracticalLessonEvent_Student_Attendance_Student"),
                    l => l.HasOne<PracticalLessonEvent>().WithMany()
                        .HasForeignKey("PracticalLessonEvenId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PracticalLessonEvent_Student_Attendance_PracticalLessonEvent"),
                    j =>
                    {
                        j.HasKey("PracticalLessonEvenId", "StudentId");
                        j.ToTable("PracticalLessonEvent_Student_Attendance");
                    });
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasOne(d => d.User).WithOne(p => p.Professor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Professor_User");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasOne(d => d.User).WithOne(p => p.Student)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_User");
        });

        modelBuilder.Entity<SysAdmin>(entity =>
        {
            entity.HasOne(d => d.User).WithOne(p => p.SysAdmin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SysAdmin_User");
        });

        modelBuilder.Entity<TheoreticalLesson>(entity =>
        {
            entity.Property(e => e.Description).HasComment("A short description on what the students will be learning at this theoretical lesson");

            entity.HasOne(d => d.Discipline).WithOne(p => p.TheoreticalLesson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TheoreticalLesson_Discipline");
        });

        modelBuilder.Entity<TheoreticalLessonEvent>(entity =>
        {
            entity.Property(e => e.Occurance).HasComment("An enum which describes the occurance. ex:1-weekly/2-bi-weekly");

            entity.HasOne(d => d.TheoreticalLesson).WithMany(p => p.TheoreticalLessonEvents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TheoreticalLessonEvent_PracticalLesson");

            entity.HasMany(d => d.Students).WithMany(p => p.TheoreticalLessonEvens)
                .UsingEntity<Dictionary<string, object>>(
                    "TheoreticalLessonEventStudentAttendance",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TheoreticalLessonEvent_Student_Attendance_Student"),
                    l => l.HasOne<TheoreticalLessonEvent>().WithMany()
                        .HasForeignKey("TheoreticalLessonEvenId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TheoreticalLessonEvent_Student_Attendance_TheoreticalLessonEvent"),
                    j =>
                    {
                        j.HasKey("TheoreticalLessonEvenId", "StudentId");
                        j.ToTable("TheoreticalLessonEvent_Student_Attendance");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
