using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Attempts { get; set; }

    public bool Admin { get; set; }

    public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();

    public virtual ICollection<StudentActivityPoint> StudentActivityPoints { get; set; } = new List<StudentActivityPoint>();

    public virtual ICollection<CourseInAcademicYear> CourseInAcademicYears { get; set; } = new List<CourseInAcademicYear>();
}
