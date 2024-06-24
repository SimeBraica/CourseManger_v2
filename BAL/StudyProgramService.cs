using DAL.Models;
using DAL.Repositories;
using DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL {
    public class StudyProgramService {

        public async Task<List<StudyProgramDTO>> GetStudyPrograms() {

            using (var repo = new StudyProgramRepository()) {

                var studyPrograms = await repo.GetStudyProgramsAsync();

                return studyPrograms.Select(studyPrograms => new StudyProgramDTO {
                    Id = studyPrograms.Id,
                    ShortName = studyPrograms.ShortName,
                    Name = studyPrograms.Name,
                }).ToList();
            }
        }


        public async Task<List<StudyProgram>> GetAllStudyProgramsWithId() {

            using (var repo = new StudyProgramRepository()) {

                var studyPrograms = await repo.GetStudyProgramsAsync();

                return studyPrograms.ToList();
            }
        }

        public async Task<List<StudyProgramDTO>> GetStudyProgramById(int id) {

            using (var repo = new StudyProgramRepository()) {

                var studyPrograms = await repo.GetStudyProgramByIdAsync(id);

                return studyPrograms.Select(studyPrograms => new StudyProgramDTO {
                    ShortName = studyPrograms.ShortName,
                    Name = studyPrograms.Name,
                }).ToList();
            }
        }

        public async Task<bool> AddStudyProgram(StudyProgramDTO newStudyProgram) {
            using (var repo = new StudyProgramRepository()) {
                var addedStudyProgram = await repo.CreateStudyProgramAsync(newStudyProgram, true);
                return addedStudyProgram != null;
            }
        }

        public async Task<bool> UpdateStudyProgram(StudyProgramDTO studyProgram, int id) {
            bool isSuccessful = false;
            using (var repo = new StudyProgramRepository()) {
                int affectedRows = await repo.UpdateStudyProgramAsync(studyProgram, id);
                isSuccessful = affectedRows > 0;
            }
            return isSuccessful;
        }

        public async Task<bool> RemoveStudyProgram(int idStudyProgram) {
            using (var repo = new StudyProgramRepository()) {
                int affectedRows = await repo.RemoveStudyProgramAsync(idStudyProgram, true);
                return affectedRows > 0;
            }
        }

    }
}
