using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class CourseStudent
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int CourseInAcademicYearId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public int EnrolledBy { get; set; }

    public int? FinalGrade { get; set; }

    public DateTime? GradeDate { get; set; }

    public virtual CourseInAcademicYear CourseInAcademicYear { get; set; } = null!;

    public virtual Teacher EnrolledByNavigation { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
