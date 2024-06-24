using DAL.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories {
    public class AcademicYearRepository : Repository<AcademicYear> {
        public AcademicYearRepository() : base(new CourseManagerTestContext()) {

        }

        public async Task<List<AcademicYear>> GetAcademicYearsAsync() {
            var query = from e in Entities
                        select e;
            return await query.ToListAsync();
        }

        public async Task<AcademicYear> CreateAcademicYearsAsync(AcademicYearCreateDTO academicYear, bool saveChanges = true) {
            AcademicYear _academicYear = null;
                _academicYear = new AcademicYear {
                    Name = academicYear.Name,
                    Active = academicYear.Active,
                };
                Context.AcademicYears.Add(_academicYear);
                await Context.SaveChangesAsync();
            return _academicYear;
        }

        public async Task<int> RemoveAcademicYearAsync(int id, bool saveChanges = true) {
            var coursesInAcademicYearToRemove = await Context.CourseInAcademicYears
                                                .Where(c => c.AcademicYearId == id)
                                                .ToListAsync();

            if (coursesInAcademicYearToRemove != null && coursesInAcademicYearToRemove.Any()) {
                foreach (var courseInAcademicYear in coursesInAcademicYearToRemove) {
                    // You need to remove the courses associated with each CourseInAcademicYear
                    Context.Courses.RemoveRange(courseInAcademicYear.Course);
                }

                // Remove all CourseInAcademicYear entities associated with the academic year
                Context.CourseInAcademicYears.RemoveRange(coursesInAcademicYearToRemove);

                // Remove academic year
                var academicYearToRemove = await Context.AcademicYears.FindAsync(id);
                if (academicYearToRemove != null) {
                    Context.AcademicYears.Remove(academicYearToRemove);
                }

                if (saveChanges) {
                    return await Context.SaveChangesAsync();
                } else {
                    return 0;
                }
            }
            return 0;
        }






        public override int Update(AcademicYear entity, bool saveChanges = true) {
            throw new NotImplementedException();
        }
    }
}
