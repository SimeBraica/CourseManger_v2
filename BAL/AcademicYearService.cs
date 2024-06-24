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
    public class AcademicYearService {
        public async Task<List<AcademicYearDTO>> GetAcademicYears() {

            using (var repo = new AcademicYearRepository()) {

                var academicYears = await repo.GetAcademicYearsAsync();

                return academicYears.Select(academicYears => new AcademicYearDTO {
                    Id = academicYears.Id,
                    Name = academicYears.Name,
                    Active = academicYears.Active
                }).ToList();
            }
        }


        public async Task<bool> AddAcademicYear(AcademicYearCreateDTO newAcademicYear) {
            using (var repo = new AcademicYearRepository()) {
                var addedAcademicYear = await repo.CreateAcademicYearsAsync(newAcademicYear, true);
                return addedAcademicYear != null;
            }
        }
        public async Task<bool> RemoveAcadmicYear(int id) {
            using (var repo = new AcademicYearRepository()) {
                int affectedRows = await repo.RemoveAcademicYearAsync(id, true);
                return affectedRows > 0;
            }
        }
    }
    }
 
