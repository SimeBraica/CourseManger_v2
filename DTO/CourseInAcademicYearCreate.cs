using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO {
    public class CourseInAcademicYearCreate {
        public string Name { get; set; } = null!;

        public string IsvuId { get; set; } = null!;

        public int Ects { get; set; } = 0!;

        public int Semester { get; set; } = 0!;

        public int StudyProgramId { get; set; } = 0!;
    }
}
