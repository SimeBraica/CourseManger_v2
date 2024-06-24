using System;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class CourseManagerTestContext : DbContext
{
    public CourseManagerTestContext()
    {
    }

    public CourseManagerTestContext(DbContextOptions<CourseManagerTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicYear> AcademicYears { get; set; }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<ActivityType> ActivityTypes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseInAcademicYear> CourseInAcademicYears { get; set; }

    public virtual DbSet<CourseStudent> CourseStudents { get; set; }

    public virtual DbSet<ExamPeriod> ExamPeriods { get; set; }

    public virtual DbSet<ExamPeriodType> ExamPeriodTypes { get; set; }

    public virtual DbSet<GradeScale> GradeScales { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentActivityPoint> StudentActivityPoints { get; set; }

    public virtual DbSet<StudyProgram> StudyPrograms { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=31.147.206.65;Initial Catalog=CourseManagerTest;User ID=sbraica;Password=uJYptMsf;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Croatian_CI_AI");

        modelBuilder.Entity<AcademicYear>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.MaxPoints).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MinPointsForGrade).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MinPointsForSignature).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ActivityType).WithMany(p => p.Activities)
                .HasForeignKey(d => d.ActivityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activities_ActivityTypes");

            entity.HasOne(d => d.CourseInAcademicYear).WithMany(p => p.Activities)
                .HasForeignKey(d => d.CourseInAcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Activities_CourseInAcademicYears");
        });

        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.Ects).HasColumnName("ECTS");
            entity.Property(e => e.IsvuId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ISVU_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.StudyProgram).WithMany(p => p.Courses)
                .HasForeignKey(d => d.StudyProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_StudyPrograms");
        });

        modelBuilder.Entity<CourseInAcademicYear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CoursesInAcademicYear");

            entity.HasOne(d => d.AcademicYear).WithMany(p => p.CourseInAcademicYears)
                .HasForeignKey(d => d.AcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursesInAcademicYear_AcademicYears");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseInAcademicYears)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursesInAcademicYear_Courses");
        });

        modelBuilder.Entity<CourseStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CourseStudents_1");

            entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");
            entity.Property(e => e.GradeDate).HasColumnType("datetime");

            entity.HasOne(d => d.CourseInAcademicYear).WithMany(p => p.CourseStudents)
                .HasForeignKey(d => d.CourseInAcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseStudents_CourseInAcademicYears");

            entity.HasOne(d => d.EnrolledByNavigation).WithMany(p => p.CourseStudents)
                .HasForeignKey(d => d.EnrolledBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseStudents_Teachers");

            entity.HasOne(d => d.Student).WithMany(p => p.CourseStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseStudents_Students");
        });

        modelBuilder.Entity<ExamPeriod>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.CourseInAcademicYear).WithMany(p => p.ExamPeriods)
                .HasForeignKey(d => d.CourseInAcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamPeriods_CourseInAcademicYears");

            entity.HasOne(d => d.ExamPeriodType).WithMany(p => p.ExamPeriods)
                .HasForeignKey(d => d.ExamPeriodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExamPeriods_ExamPeriodType");
        });

        modelBuilder.Entity<ExamPeriodType>(entity =>
        {
            entity.ToTable("ExamPeriodType");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GradeScale>(entity =>
        {
            entity.Property(e => e.LowerBound).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpperBound).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.CourseInAcademicYear).WithMany(p => p.GradeScales)
                .HasForeignKey(d => d.CourseInAcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GradeScales_CourseInAcademicYears");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Jmbag)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("JMBAG");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentActivityPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentA__3214EC07C28534C9");

            entity.Property(e => e.DateAwarded).HasColumnType("datetime");
            entity.Property(e => e.Points).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Activity).WithMany(p => p.StudentActivityPoints)
                .HasForeignKey(d => d.ActivityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAc__Activ__36470DEF");

            entity.HasOne(d => d.CourseInAcademicYear).WithMany(p => p.StudentActivityPoints)
                .HasForeignKey(d => d.CourseInAcademicYearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAc__Cours__3552E9B6");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentActivityPoints)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAc__Stude__345EC57D");

            entity.HasOne(d => d.Teacher).WithMany(p => p.StudentActivityPoints)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentAc__Teach__373B3228");
        });

        modelBuilder.Entity<StudyProgram>(entity =>
        {
            entity.Property(e => e.Name).HasColumnType("ntext");
            entity.Property(e => e.ShortName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.CourseInAcademicYears).WithMany(p => p.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseTeacher",
                    r => r.HasOne<CourseInAcademicYear>().WithMany()
                        .HasForeignKey("CourseInAcademicYearId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseTeacher_CourseInAcademicYear"),
                    l => l.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseTeacher_Teacher"),
                    j =>
                    {
                        j.HasKey("TeacherId", "CourseInAcademicYearId").HasName("PK_CourseTeacher");
                        j.ToTable("CourseTeachers");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
