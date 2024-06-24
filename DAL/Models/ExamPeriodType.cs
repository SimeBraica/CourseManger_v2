using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ExamPeriodType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ExamPeriod> ExamPeriods { get; set; } = new List<ExamPeriod>();
}
