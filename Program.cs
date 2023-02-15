using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Repository;
using Cooking_School_ASP.NET.Services;
using HotelListing.Core;
using Microsoft.AspNetCore.Hosting;
using Cooking_School_ASP.NET.Hash;
//using Swashbuckle.AspNetCore.Filters;
using Cooking_School_ASP.NET;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
builder.Services.AddControllers()
    .AddNewtonsoftJson(op =>
            op.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureAutoMapper();
builder.Services.AddAuthentication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();    
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<IChefService, ChefService>();
builder.Services.AddScoped<ITraineeService, TraineeService>();
builder.Services.AddScoped<IHashPassword, HashPassword>();
builder.Services.AddScoped<ICookClassService, CookClassService>();
builder.Services.AddScoped<IProjectFileService, ProjectFileService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IProjectFileService, ProjectFileService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IApplicationSevice, ApplicationSevice>();


builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

});
var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddlewareExtensions>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization(); ;
app.MapControllers();
app.Run();