using DAL.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories {
    public class StudyProgramRepository : Repository<StudyProgram> {
        public StudyProgramRepository() : base(new CourseManagerTestContext()) {

        }
        public async Task<List<StudyProgram>> GetStudyProgramsAsync() {
            var query = from e in Entities
                        select e;
            return await query.ToListAsync();
        }

        public async Task<List<StudyProgram>> GetStudyProgramByIdAsync(int id) {
            var query = from e in Entities
                        where e.Id == id
                        select e;
            return await query.ToListAsync();
        }

        public async Task<StudyProgram> CreateStudyProgramAsync(StudyProgramDTO studyProgram, bool saveChanges = true) {
            StudyProgram _studyProgram = null;
            _studyProgram = new StudyProgram {
                Id = studyProgram.Id,
                ShortName = studyProgram.ShortName,
                Name = studyProgram.Name,
            };
            Context.StudyPrograms.Add(_studyProgram);
            await Context.SaveChangesAsync();
            return _studyProgram;
        }


        public async Task<int> RemoveStudyProgramAsync(int idStudyProgram,bool saveChanges = true) {
            var _studyProgram = await Entities.SingleOrDefaultAsync(s => s.Id == idStudyProgram);
            var foreignKeyCourses = await Context.Courses.Where(c => c.StudyProgramId == idStudyProgram).ToListAsync();
            foreach(var course in foreignKeyCourses) {
                course.StudyProgramId = null;
            }
            Entities.Attach(_studyProgram);
            Entities.Remove(_studyProgram);
            if (saveChanges) {
                return SaveChanges();
            } else {
                return 0;
            }
        }

        public async  Task<int> UpdateStudyProgramAsync(StudyProgramDTO studyProgram, int id, bool saveChanges = true) {

            var _studyProgram = await Entities.SingleOrDefaultAsync(s => s.Id == id);
            _studyProgram.Id = id;
            _studyProgram.ShortName = studyProgram.ShortName;
            _studyProgram.Name = studyProgram.Name;

            if (saveChanges) {
                return await Context.SaveChangesAsync();
            } else {
                return 0;
            }
        }

        public override int Update(StudyProgram entity, bool saveChanges = true) {
            throw new NotImplementedException();
        }
    }
}
