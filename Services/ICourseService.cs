using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.CourseDto;
using Cooking_School_ASP.NET.ModelUsed;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Services
{
    public interface ICourseService
    {
        Task<ResponsDto<CourseDTO>> CreateCourse(CreateCourseDto createCourseDto);
        Task<ResponsDto<CourseDTO>> UpdateCourse(UpdateCourseDto updateCourseDto, int courseId);
        Task<ResponsDto<CourseDTO>> DeleteCourse(int courseId);
        Task<ResponsDto<CourseDTO>> GetAllCourses([FromQuery] RequestParam requestParams = null);
        Task<ResponsDto<CourseDTO>> GetCourseById(int courseId);
        Task<ResponsDto<CourseDTO>> FavoriteCourse(int courseId, int traineeId);
        Task<ResponsDto<CourseDTO>> UnFavoriteCourse(int courseId, int traineeId);
        Task<ResponsDto<CourseDTO>> GetAllFavoriteCourses();
    }
}
