using BAL;
using DAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : ControllerBase {

        AcademicYearService _academicYearService = new AcademicYearService();
        private readonly Jwt _jwt;
        private readonly CourseManagerTestContext _context;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AcademicYearController(AcademicYearService academicYearService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, CourseManagerTestContext context) {
            _academicYearService = academicYearService;
            _config = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _jwt = new Jwt(_config, _httpContextAccessor, _context);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<AcademicYearDTO>>> GetAllAcademicYears([FromQuery] string sortBy ="asc", [FromQuery] string is_active="" , [FromQuery] int page = 1, [FromQuery] int pageSize = 10 ) {
            var claims = _jwt.DecodeToken();
            string isAdmin = claims[1];
            if (isAdmin == "admin") {
                var courses = await _academicYearService.GetAcademicYears();
                var totalCount = courses.Count;
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                courses = courses.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (!string.IsNullOrEmpty(sortBy)) {
                    if(sortBy == "desc") {
                        courses = courses.OrderBy(c => c.Name).ToList();
                    } else {
                        courses = courses.OrderByDescending(c => c.Name).ToList();
                    }
                    if (!string.IsNullOrEmpty(is_active)) {
                        if(is_active == "true") {
                            bool isActive = is_active.ToLower() == "true";
                            courses = courses.Where(c => c.Active == isActive).ToList();
                        }
                        if (is_active == "false") {
                            bool isActive = is_active.ToLower() == "true";
                            courses = courses.Where(c => c.Active == isActive).ToList();
                        }
                    }
                   

                    var result = new {
                        TotalCount = totalCount,
                        TotalPages = totalPages,
                        CurrentPage = page,
                        PageSize = pageSize,
                        Data = courses
                    };

                    return Ok(result);
                }
                return Forbid();
            } else {
                return Forbid();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAcademicYear(AcademicYearCreateDTO academicYear) {
            var claims = _jwt.DecodeToken();
            string isAdmin = claims[1];
            if (isAdmin == "admin") {
                var _academicYear = await _academicYearService.AddAcademicYear(academicYear);
                return Ok(_academicYear);
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcademicYear(int id) {
            IActionResult response = BadRequest()  ;
            var _academicYear = await _academicYearService.RemoveAcadmicYear(id);
            if (_academicYear != null) {
                response = Ok();
            } 
            return response;
        }
    }
}
