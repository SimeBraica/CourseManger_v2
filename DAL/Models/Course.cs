using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string IsvuId { get; set; } = null!;

    public int Ects { get; set; }

    public int Semester { get; set; }

    public int? StudyProgramId { get; set; }

    public virtual ICollection<CourseInAcademicYear> CourseInAcademicYears { get; set; } = new List<CourseInAcademicYear>();

    public virtual StudyProgram? StudyProgram { get; set; } = null!;
}
