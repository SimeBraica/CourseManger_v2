using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BAL;
using DAL;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var connectionString = builder.Configuration.GetConnectionString("CourseManagerDb") ?? throw new InvalidOperationException("Connection string 'TasksUsersContextConnection' not found.");

// Configure services
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options => {
    options.AddPolicy(MyAllowSpecificOrigins,
        policy => {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(options => options.Cookie.Name = "token")
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents {
        OnMessageReceived = context => {
            context.Token = context.Request.Cookies["token"];
            return Task.CompletedTask;
        }
    };
})
.AddGitHub(o =>
 {
     o.ClientId = "Ov23likPYqh3uOf9C0HX";
     o.ClientSecret = "f0e9a3b5837c953afc90d6bfe17f72734054e3b1";
     o.CallbackPath = "/signin-github";

     o.Scope.Add("read:user");

     o.Events.OnCreatingTicket += context =>
     {
         if (context.AccessToken is { }) {
             context.Identity?.AddClaim(new Claim("access_token", context.AccessToken));
         }

         return Task.CompletedTask;
     };
 });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<AcademicYearService>();
builder.Services.AddScoped<CourseInAcademicYearService>();
builder.Services.AddScoped<StudyProgramService>();
builder.Services.AddDbContext<CourseManagerTestContext>();


var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
