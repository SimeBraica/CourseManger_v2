using BAL;
using DAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase {
        private readonly TeacherService _teacherService;
        private readonly Jwt _jwt;
        private readonly CourseManagerTestContext _context;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TeacherController(TeacherService teacherService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, CourseManagerTestContext context) {
            _teacherService = teacherService;
            _config = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _jwt = new Jwt(_config, _httpContextAccessor, _context);

        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(TeacherRegistrationDTO teacher) {
            IActionResult response;
            var _teacher = await _teacherService.AddTeacher(teacher);
            if (_teacher != null) {
                response = Ok();
            } else {
                response = BadRequest();
            }
            return response;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(TeacherLoginDTO teacher) {
            IActionResult response = Unauthorized();
            var _teacher = await _teacherService.GetAccount(teacher);
            if (_teacher != null) {
                var token = _jwt.GenerateJWT(_teacher);
                response = Ok();
            }
            return response;
        }

        [AllowAnonymous]
        [HttpPost("githubLogin")]
        public async Task<IActionResult> GithubLogin(string code) {
            return Ok(code);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout() {
            foreach (var cookie in Request.Cookies) {
                if (Request.Cookies[cookie.Key] != null) {
                    var cookieOptions = new CookieOptions {
                        Expires = DateTimeOffset.UtcNow.AddDays(-1),
                        HttpOnly = true, 
                        Secure = true, 
                        SameSite = SameSiteMode.None 
                    };
                    Response.Cookies.Delete(cookie.Key, cookieOptions);
                }
            }
            return Ok();
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(TeacherProfileDTO teacher) {
            var  claims = _jwt.DecodeToken();
            string username = claims[0];
            Teacher teacherToUpdate = await _teacherService.GetTeacherAccountByUsername(username);

            if (teacherToUpdate != null) {
                var updateTeacher = new TeacherProfileDTO {
                    Username = teacherToUpdate.Username,
                    Password = teacherToUpdate.Password,
                    FirstName = teacherToUpdate.FirstName,
                    LastName = teacherToUpdate.LastName,
                    Admin = teacherToUpdate.Admin
                };


                bool success = await _teacherService.UpdateTeacher(updateTeacher);

                if (success) {
                    return Ok();
                } else {
                    return BadRequest();
                }

            }
            return BadRequest();
        }
    }
}
