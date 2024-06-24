using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class CourseInAcademicYear
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int AcademicYearId { get; set; }

    public virtual AcademicYear AcademicYear { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();

    public virtual ICollection<ExamPeriod> ExamPeriods { get; set; } = new List<ExamPeriod>();

    public virtual ICollection<GradeScale> GradeScales { get; set; } = new List<GradeScale>();

    public virtual ICollection<StudentActivityPoint> StudentActivityPoints { get; set; } = new List<StudentActivityPoint>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
