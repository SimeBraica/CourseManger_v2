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
    public class StudyProgramController : ControllerBase {

        StudyProgramService _studyProgramService = new StudyProgramService();
        private readonly Jwt _jwt;
        private readonly CourseManagerTestContext _context;
        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public StudyProgramController(StudyProgramService studyProgramService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, CourseManagerTestContext context) {
            _studyProgramService = studyProgramService;
            _config = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _jwt = new Jwt(_config, _httpContextAccessor, _context);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<StudyProgramDTO>>> GetAllAcademicYears() {
            var claims = _jwt.DecodeToken();
            string isAdmin = claims[1];
            if (isAdmin == "admin") {
                var courses = await _studyProgramService.GetStudyPrograms();
                if (courses != null) {
                    return Ok(courses);
                } else {
                    return BadRequest();
                }
            }
            return Forbid();
        }


        [Authorize]
        [HttpGet]
        [Route("StudyProgramWithId")]
        public async Task<ActionResult<List<StudyProgram>>> GetAllStudyProgramsWithId() {
            var claims = _jwt.DecodeToken();
            string isAdmin = claims[1];
            if (isAdmin == "admin") {
                var studyPrograms = await _studyProgramService.GetAllStudyProgramsWithId();
                if (studyPrograms != null) {
                    return Ok(studyPrograms);
                } else {
                    return BadRequest();
                }
            }
            return Forbid();
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<StudyProgramDTO>>> GetStudyProgramId(int id) {
           // var claims = _jwt.DecodeToken();
            //string isAdmin = claims[1];
           // if (isAdmin == "admin") {
                var courses = await _studyProgramService.GetStudyProgramById(id);
                if (courses != null) {
                    return Ok(courses);
              //  } else {
               //     return BadRequest();
             //   }
            }
            return Forbid();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateStudyProgram(StudyProgramDTO studyProgram) {
            var claims = _jwt.DecodeToken();
            string isAdmin = claims[1];
            if (isAdmin == "admin") {
                var _studyProgram = await _studyProgramService.AddStudyProgram(studyProgram);
                if (_studyProgram != null) {
                    return Ok();
                } else {
                    return BadRequest();
                }
            }
            return Forbid();
        }


         [Authorize]
         [HttpDelete("{id}")]
         public async Task<IActionResult> RemoveStudyProgram(int id) {
             var claims = _jwt.DecodeToken();
             string isAdmin = claims[1];
             if (isAdmin == "admin") {
                 var _studyProgram = await _studyProgramService.RemoveStudyProgram(id);
                 if (_studyProgram != false) {
                     return Ok();
                 } else {
                     return BadRequest();
                 }
             }
             return Forbid();
         }

     

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudyProgram(StudyProgramDTO studyProgram, int id) {
            var claims = _jwt.DecodeToken();
            string isAdmin = claims[1];
            if (isAdmin == "admin") {
                var _studyProgram = await _studyProgramService.UpdateStudyProgram(studyProgram, 4);
                if (_studyProgram != null) {
                    return Ok();
                } else {
                    return BadRequest();
                }
            }
            return Forbid();
        }
    }
}
