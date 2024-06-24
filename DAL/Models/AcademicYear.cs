using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class AcademicYear
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<CourseInAcademicYear> CourseInAcademicYears { get; set; } = new List<CourseInAcademicYear>();
}
