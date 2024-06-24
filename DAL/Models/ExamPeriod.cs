using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ExamPeriod
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public int ExamPeriodTypeId { get; set; }

    public int CourseInAcademicYearId { get; set; }

    public virtual CourseInAcademicYear CourseInAcademicYear { get; set; } = null!;

    public virtual ExamPeriodType ExamPeriodType { get; set; } = null!;
}
