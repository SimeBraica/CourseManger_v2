using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Grade { get; set; }

    public string? Email { get; set; }

    public string? Jmbag { get; set; }

    public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();

    public virtual ICollection<StudentActivityPoint> StudentActivityPoints { get; set; } = new List<StudentActivityPoint>();
}
