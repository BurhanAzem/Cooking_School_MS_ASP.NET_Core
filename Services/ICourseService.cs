using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.CourseDto;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Services
{
    public interface ICourseService
    {
        Task<ResponsCourseDto> CreateCourse(CreateCourseDto createCourseDto, int adminId);
        Task<ResponsCourseDto> UpdateCourse(UpdateCourseDto updateCourseDto, int courseId);
        Task<ResponsCourseDto> DeleteCourse(int courseId);
        Task<ResponsCourseDto> GetAllCourses([FromQuery] RequestParam requestParams = null);
        Task<ResponsCourseDto> GetCourseById(int courseId);
        Task<ResponsCourseDto> FavoriteCourse(int courseId, int traineeId);
        Task<ResponsCourseDto> UnFavoriteCourse(int courseId, int traineeId);
        Task<ResponsCourseDto> GetAllFavoriteCourses();
    }
}
