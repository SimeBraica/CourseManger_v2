using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO {
    public class CourseInAcademicYearDTO {
        public int CourseId { get; set; } = 0!;

        public string Name { get; set; } = null!;

        public string IsvuId { get; set; } = null!;

        public int Ects { get; set; } = 0!;

        public int Semester { get; set; } = 0!;

        public int AcademicYearId { get; set; } = 0!;
        public string ShortName { get; set; } = null!;
    }
}
