using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.Dtos;
using Cooking_School.Services.Dtos.CookClassDto;
using Cooking_School.Services.Dtos.CourseDto;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School.Services.CourseService
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
