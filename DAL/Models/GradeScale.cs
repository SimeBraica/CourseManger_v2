using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class GradeScale
{
    public int Id { get; set; }

    public decimal LowerBound { get; set; }

    public decimal UpperBound { get; set; }

    public int Grade { get; set; }

    public int CourseInAcademicYearId { get; set; }

    public virtual CourseInAcademicYear CourseInAcademicYear { get; set; } = null!;
}
