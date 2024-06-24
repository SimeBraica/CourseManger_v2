using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class StudentActivityPoint
{
    public int Id { get; set; }

    public decimal Points { get; set; }

    public int CourseInAcademicYearId { get; set; }

    public int StudentId { get; set; }

    public int ActivityId { get; set; }

    public int TeacherId { get; set; }

    public DateTime? DateAwarded { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual CourseInAcademicYear CourseInAcademicYear { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
