using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO {
    public class AcademicYearCreateDTO {
        public string Name { get; set; } = null!;
        public bool Active { get; set; } = false!;
    }
}
