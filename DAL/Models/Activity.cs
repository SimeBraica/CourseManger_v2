using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Activity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal MaxPoints { get; set; }

    public decimal MinPointsForGrade { get; set; }

    public decimal MinPointsForSignature { get; set; }

    public int CourseInAcademicYearId { get; set; }

    public int ActivityTypeId { get; set; }

    public virtual ActivityType ActivityType { get; set; } = null!;

    public virtual CourseInAcademicYear CourseInAcademicYear { get; set; } = null!;

    public virtual ICollection<StudentActivityPoint> StudentActivityPoints { get; set; } = new List<StudentActivityPoint>();
}
