using DAL.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TeacherRepository : Repository<Teacher>
    {
        public TeacherRepository() : base(new CourseManagerTestContext())
        {

        }

        public async Task<TeacherLoginDTO> GetTeachersAsync(TeacherLoginDTO teacher)
        {
            TeacherLoginDTO _teacher = null;
            var existingTeacher = await Context.Teachers.FirstOrDefaultAsync(u => u.Username == teacher.Username);
            if (existingTeacher != null && BCrypt.Net.BCrypt.Verify(teacher.Password, existingTeacher.Password))
            {
                _teacher = new TeacherLoginDTO { Username = existingTeacher.Username };
            }
            return _teacher;
        }



        public async Task<Teacher> AddTeacherAsync(TeacherRegistrationDTO teacher, bool saveChanges = true)
        {
            Teacher _teacher = null;
            var existingTeacher = await Context.Teachers.FirstOrDefaultAsync(u => u.Username == teacher.Username);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(teacher.Password);
            if (existingTeacher == null)
            {
                _teacher = new Teacher
                {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Username = teacher.Username,
                    Password = passwordHash,
                    Admin = false,
                    Attempts = 0
                };
                Context.Teachers.Add(_teacher);
                await Context.SaveChangesAsync();
            }

            return _teacher;
        }

        public async Task<int> UpdateTeacherAsync(TeacherProfileDTO teacher, bool saveChanges = true) {
            Teacher _teacher = await Context.Teachers.FirstOrDefaultAsync(u => u.Username == teacher.Username);

            if (_teacher != null) {
                _teacher.Username = teacher.Username;
                _teacher.Password = teacher.Password;
                _teacher.FirstName = teacher.FirstName;
                _teacher.LastName = teacher.LastName;
                _teacher.Admin = teacher.Admin;
                if (saveChanges) {
                    return await Context.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<Teacher> GetTeacherByUsername(string username) {
            var existingTeacher = await Context.Teachers.FirstOrDefaultAsync(u => u.Username == username);
            return existingTeacher;
        }

        public override int Update(Teacher entity, bool saveChanges = true) {
            throw new NotImplementedException();
        }
    }
}
