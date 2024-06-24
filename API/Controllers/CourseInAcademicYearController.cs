using BAL;
using DAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInAcademicYearController : ControllerBase {

        CourseInAcademicYearService _courseInAcademicYearService = new CourseInAcademicYearService();
        private readonly Jwt _jwt;
        private readonly CourseManagerTestContext _context;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseInAcademicYearController(CourseInAcademicYearService courseInAcademicYearService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, CourseManagerTestContext context)
        {
            _courseInAcademicYearService = courseInAcademicYearService;
            _config = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _jwt = new Jwt(_config, _httpContextAccessor, _context);
        }

        [Authorize]
        [HttpGet("CourseYears/{id}")]
        public async Task<ActionResult<List<CourseInAcademicYearDTO>>> GetCoursesYears(int id) {
            var claims = _jwt.DecodeToken();
            string isAdmin = claims[1];
            if (isAdmin == "admin") {
                var courses = await _courseInAcademicYearService.GetAllCoursesInAcademicYears(id);
                return Ok(courses);
            } else {
                return Forbid();
            }
        }
        [Authorize]
        [HttpPost("CourseYears/{id}")]
        public async Task<IActionResult> CreateCourse(CourseInAcademicYearCreate courseInAcademicYear, int id) {
            var claims = _jwt.DecodeToken();
            string isAdmin = claims[1];
            IActionResult response;
            if (isAdmin == "admin") {
                var _course = await _courseInAcademicYearService.AddCourse(courseInAcademicYear, id);
                if (_course != null) {
                    return Ok();
                } else {
                    return BadRequest();
                }
            }
            return Forbid();
        }
    }
}
