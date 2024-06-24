using DAL.Models;
using DAL.Repositories;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL {
    public class CourseInAcademicYearService {
        public async Task<List<CourseInAcademicYearDTO>> GetAllCoursesInAcademicYears(int id) {
            using (var repo = new CourseInAcademicYearRepository()) {

                var courseYears = await repo.GetCourseInAcademicYearsAsync(id);

                return courseYears.Select(courseYears => new CourseInAcademicYearDTO {
                    CourseId = courseYears.CourseId,
                    AcademicYearId = courseYears.AcademicYearId,
                    Name = courseYears.Course.Name,
                    IsvuId = courseYears.Course.IsvuId,
                    Ects = courseYears.Course.Ects,
                    Semester = courseYears.Course.Semester,
                    ShortName = courseYears.Course.StudyProgram?.ShortName
                }).ToList();
            }
        }

        public async Task<bool> AddCourse(CourseInAcademicYearCreate newCourse, int id) {
            using (var repo = new CourseInAcademicYearRepository()) {
                var addedCourse = await repo.CreateCourseAsync(newCourse,id, true);
                return addedCourse != null;
            }
        }
    }
}
