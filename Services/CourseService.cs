using AutoMapper;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.CourseDto;
using Cooking_School_ASP.NET.Dtos.ProjectDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        public async Task<ResponsCourseDto> CreateCourse(CreateCourseDto createCourseDto, int adminId)
        {
            if (await _unitOfWork.Courses.Get(x => x.CourseName == createCourseDto.CourseName) is not null)
            {
                return new ResponsCourseDto()
                {
                    Exception = new Exception("Failed, Course Entered alraedy Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == adminId) is null)
            {
                return new ResponsCourseDto()
                {
                    Exception = new Exception("Failed, Admin Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var course = _mapper.Map<Course>(createCourseDto);
            await _unitOfWork.Courses.Insert(course);
            await _unitOfWork.Save();
            var courseDto = _mapper.Map<CourseDTO>(course);
            return new ResponsCourseDto()
            {
                CourseDTO = courseDto,
            };
        }

        public async Task<ResponsCourseDto> DeleteCourse(int courseId)
        {
            if (await _unitOfWork.Courses.Get(x => x.Id == courseId) is null)
            {
                return new ResponsCourseDto()
                {
                    Exception = new Exception("Failed, This Course Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.Courses.Delete(courseId);
            await _unitOfWork.Save();
            return new ResponsCourseDto();
        }

        public async Task<ResponsCourseDto> FavoriteCourse(int courseId, int traineeId)
        {
            if (await _unitOfWork.Courses.Get(x => x.Id == courseId) is not null) 
            {
                return new ResponsCourseDto()
                {
                    Exception = new Exception($"Failed, Course Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == traineeId) is not null)
            {
                return new ResponsCourseDto()
                {
                    Exception = new Exception($"Failed, trainee Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            Favorite_Course favoriteCourse = new Favorite_Course();
            favoriteCourse.CourseId = courseId;
            favoriteCourse.TraineeId = traineeId;
            favoriteCourse.Created = DateTime.Now;
            await _unitOfWork.Favorite_Courses.Insert(favoriteCourse);
            await _unitOfWork.Save();
            return new ResponsCourseDto()
            {
            };
        }

        public async Task<ResponsCourseDto> GetAllCourses([FromQuery] RequestParam requestParams)
        {
            if (requestParams == null)
            {
                var courses = await _unitOfWork.Courses.GetAll();
                var coursesDto = _mapper.Map<IList<CourseDTO>>(courses);
                return new ResponsCourseDto()
                {
                    Courses = coursesDto
                };
            }
            var coursesPag = await _unitOfWork.Courses.GetPagedList(requestParams);
            var coursesDtoPag = _mapper.Map<IList<CourseDTO>>(coursesPag);
            return new ResponsCourseDto()
            {
                Courses = coursesDtoPag
            };
        }

        public async Task<ResponsCourseDto> GetAllFavoriteCourses()
        {
            var favorite_Courses = await _unitOfWork.Favorite_Courses.GetAll();
            var courses = await _unitOfWork.Courses.GetAll();
            IList<Course> favoriteCourses = new List<Course>(); 
            foreach (var _course in favorite_Courses)
            {
                favoriteCourses.Add(courses.FirstOrDefault(x => x.Id == _course.Id));
            }
            return new ResponsCourseDto()
            {
                Courses = _mapper.Map<IList<CourseDTO>>(favoriteCourses)
            };
        }

        public async Task<ResponsCourseDto> GetCourseById(int courseId)
        {
            var course = await _unitOfWork.Courses.Get(x => x.Id == courseId);
            if (course == null)
            {
                return new ResponsCourseDto
                {
                    Exception = new Exception($"Failed, this Course with {courseId} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            else
            {
                return new ResponsCourseDto()
                {
                    CourseDTO = _mapper.Map<CourseDTO>(course)
                };
            }
        }

        public async Task<ResponsCourseDto> UnFavoriteCourse(int courseId, int traineeId)
        {
            var favoriteCourse = await _unitOfWork.Favorite_Courses.Get(x => x.Id == courseId && x.Id == traineeId);
            if (favoriteCourse is not null)
            {
                return new ResponsCourseDto()
                {
                    Exception = new Exception($"Failed, Course Favorite Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.Favorite_Courses.Delete(favoriteCourse.Id);
            await _unitOfWork.Save();
            return new ResponsCourseDto()
            {
            };
        }

        public async Task<ResponsCourseDto> UpdateCourse(UpdateCourseDto updateCourseDto, int courseId)
        {
            if (await _unitOfWork.Projects.Get(x => x.Id == courseId) is null)
            {
                return new ResponsCourseDto()
                {
                    Exception = new Exception($"Failed, This Course Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var updateCourse = _mapper.Map<Course>(updateCourseDto);
            updateCourse.Updated = DateTime.Now;
            _unitOfWork.Courses.Update(updateCourse);
            await _unitOfWork.Save();
            return new ResponsCourseDto() { CourseDTO = _mapper.Map<CourseDTO>(updateCourse)};
        }
    }
}
