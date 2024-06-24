using DAL.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories {
    public class CourseInAcademicYearRepository : Repository<CourseInAcademicYear> {

        public CourseInAcademicYearRepository() : base(new CourseManagerTestContext()) {

        }

        public async Task<List<CourseInAcademicYear>> GetCourseInAcademicYearsAsync(int id) {
            var query =  Entities
                         .Include(e => e.Course) 
                         .Include(e => e.Course.StudyProgram)
                         .Where(e => e.AcademicYearId == id);
            return await query.ToListAsync();
        }

        public async Task<CourseInAcademicYear> CreateCourseAsync(CourseInAcademicYearCreate courseInAcademicYear,int id ,bool saveChanges = true) {
            Course _course = null;
            _course = new Course {
                Name = courseInAcademicYear.Name,
                IsvuId = courseInAcademicYear.IsvuId,
                Ects = courseInAcademicYear.Ects,
                Semester = courseInAcademicYear.Semester,
                StudyProgramId = courseInAcademicYear.StudyProgramId
            };
            Context.Courses.Add(_course);
            await Context.SaveChangesAsync();

            CourseInAcademicYear _courseInAcademicYear = new CourseInAcademicYear {
                AcademicYearId = id,
                CourseId = _course.Id
            };

            Context.CourseInAcademicYears.Add(_courseInAcademicYear);
            await Context.SaveChangesAsync();
            return _courseInAcademicYear;
        }

        public override int Update(CourseInAcademicYear entity, bool saveChanges = true) {
            throw new NotImplementedException();
        }
    }
}
