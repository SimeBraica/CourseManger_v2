using DAL;
using DAL.Models;
using DAL.Repositories;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BAL {
    public class TeacherService {
        public async Task<bool> AddTeacher(TeacherRegistrationDTO newTeacher) {
            using (var repo = new TeacherRepository()) {
                var addedTeacher = await repo.AddTeacherAsync(newTeacher, true);
                return addedTeacher != null;
            }
        }


        public async Task<TeacherLoginDTO> GetAccount(TeacherLoginDTO teacher) {
            using (var repo = new TeacherRepository()) {
                var _teacher = repo.GetTeachersAsync(teacher);
                if (_teacher != null) return await _teacher;
                else return null;
            }
        }

        public async Task<Teacher> GetTeacherAccountByUsername(string username) {
            using (var repo = new TeacherRepository()) {
                var teacher = await repo.GetTeacherByUsername(username);
                return teacher; 
            }
        }

        public async Task<bool> UpdateTeacher(TeacherProfileDTO teacher) {
            using (var repo = new TeacherRepository()) {
                int affectedRows = await repo.UpdateTeacherAsync(teacher, true);
                return affectedRows > 0;
            }
        }
    }
}
