using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class StudyProgram
{
    public int Id { get; set; }

    public string ShortName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
