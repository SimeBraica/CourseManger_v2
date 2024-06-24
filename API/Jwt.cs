using DAL;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace API {
    public class Jwt {

        private IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CourseManagerTestContext _context;

        public Jwt(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, CourseManagerTestContext context) {
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public string GenerateJWT(TeacherLoginDTO teacher) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var audience = _config["Jwt:Audience"];
            var issuer = _config["Jwt:Issuer"];
            TimeZoneInfo croatiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
            DateTime expiresLocalTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, croatiaTimeZone).AddMinutes(30);

            string username = teacher.Username;
            bool admin = GetUserAdmin(username);
            string adminValue = "nonAdmin";
            if(admin) {
                adminValue = "admin";
            }


            var jwt_description = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {new Claim("username", username),
                                                    new Claim("admin", adminValue)
                                                   }),
                Expires = expiresLocalTime,
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = credentials
            };

            var token = new JwtSecurityTokenHandler().CreateToken(jwt_description);
            var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("token", encryptedToken,
                new CookieOptions {
                    Expires = expiresLocalTime,
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });

            var response = new { token = encryptedToken, username = teacher.Username };
            return JsonSerializer.Serialize(response);
        }


        public List<string> DecodeToken() {
            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadToken(cookie);
            var jwtS = jwt as JwtSecurityToken;


            var jti1 = jwtS.Claims.First(claim => claim.Type == "username").Value;
            var jti2 = jwtS.Claims.First(claim => claim.Type == "admin").Value;
            var listOfClaims = new List<string> {
                jti1,
                jti2
            };
            return listOfClaims;
        }

        private bool GetUserAdmin(string username) {
            var user = _context.Teachers.FirstOrDefault(u => u.Username == username);
            return (bool)(user?.Admin);
        }

    }


}
