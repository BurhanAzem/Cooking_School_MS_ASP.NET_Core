using AutoMapper;
using Cooking_School.Core.IRepository.IUnitOfWork;
using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Dtos.CourseDto;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School.Services.CourseService
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

        public async Task<ResponsDto<CourseDTO>> CreateCourse(CreateCourseDto createCourseDto)
        {
            if (await _unitOfWork.Courses.Get(x => x.CourseName == createCourseDto.CourseName) is not null)
            {
                return new ResponsDto<CourseDTO>()
                {
                    Exception = new Exception("Failed, Course Entered alraedy Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var course = _mapper.Map<Course>(createCourseDto);
            Levels level;
            try
            {
                level = (Levels)Enum.Parse(typeof(Levels), createCourseDto.CourseLevel);
            }
            catch (Exception ex)
            {
                return new ResponsDto<CourseDTO>
                {
                    Exception = new Exception($"Failed, Invaild Input Level"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            course.CourseLevel = level;
            await _unitOfWork.Courses.Insert(course);
            await _unitOfWork.Save();
            var courseDto = _mapper.Map<CourseDTO>(course);
            return new ResponsDto<CourseDTO>()
            {
                Dto = courseDto,
            };
        }

        public async Task<ResponsDto<CourseDTO>> DeleteCourse(int courseId)
        {
            if (await _unitOfWork.Courses.Get(x => x.Id == courseId) is null)
            {
                return new ResponsDto<CourseDTO>()
                {
                    Exception = new Exception("Failed, This Course Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.Courses.Delete(courseId);
            await _unitOfWork.Save();
            return new ResponsDto<CourseDTO>();
        }

        public async Task<ResponsDto<CourseDTO>> FavoriteCourse(int courseId, int traineeId)
        {
            if (await _unitOfWork.Courses.Get(x => x.Id == courseId) is not null)
            {
                return new ResponsDto<CourseDTO>()
                {
                    Exception = new Exception($"Failed, Course Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == traineeId) is not null)
            {
                return new ResponsDto<CourseDTO>()
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
            return new ResponsDto<CourseDTO>()
            {
            };
        }

        public async Task<ResponsDto<CourseDTO>> GetAllCourses([FromQuery] RequestParam requestParams)
        {
            if (requestParams == null)
            {
                var courses = await _unitOfWork.Courses.GetAll();
                var coursesDto = _mapper.Map<IList<CourseDTO>>(courses);
                return new ResponsDto<CourseDTO>()
                {
                    ListDto = coursesDto
                };
            }
            var coursesPag = await _unitOfWork.Courses.GetPagedList(requestParams);
            var coursesDtoPag = _mapper.Map<IList<CourseDTO>>(coursesPag);
            return new ResponsDto<CourseDTO>()
            {
                ListDto = coursesDtoPag
            };
        }

        public async Task<ResponsDto<CourseDTO>> GetAllFavoriteCourses()
        {
            var favorite_Courses = await _unitOfWork.Favorite_Courses.GetAll();
            var courses = await _unitOfWork.Courses.GetAll();
            IList<Course> favoriteCourses = new List<Course>();
            foreach (var _course in favorite_Courses)
            {
                favoriteCourses.Add(courses.FirstOrDefault(x => x.Id == _course.Id));
            }
            return new ResponsDto<CourseDTO>()
            {
                ListDto = _mapper.Map<IList<CourseDTO>>(favoriteCourses)
            };
        }

        public async Task<ResponsDto<CourseDTO>> GetCourseById(int courseId)
        {
            var course = await _unitOfWork.Courses.Get(x => x.Id == courseId);
            if (course == null)
            {
                return new ResponsDto<CourseDTO>
                {
                    Exception = new Exception($"Failed, this Course with {courseId} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            else
            {
                return new ResponsDto<CourseDTO>()
                {
                    Dto = _mapper.Map<CourseDTO>(course)
                };
            }
        }

        public async Task<ResponsDto<CourseDTO>> UnFavoriteCourse(int courseId, int traineeId)
        {
            var favoriteCourse = await _unitOfWork.Favorite_Courses.Get(x => x.Id == courseId && x.Id == traineeId);
            if (favoriteCourse is not null)
            {
                return new ResponsDto<CourseDTO>()
                {
                    Exception = new Exception($"Failed, Course Favorite Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.Favorite_Courses.Delete(favoriteCourse.Id);
            await _unitOfWork.Save();
            return new ResponsDto<CourseDTO>()
            {
            };
        }

        public async Task<ResponsDto<CourseDTO>> UpdateCourse(UpdateCourseDto updateCourseDto, int courseId)
        {
            var course = await _unitOfWork.Courses.Get(x => x.Id == courseId);
            if (course is null)
            {
                return new ResponsDto<CourseDTO>()
                {
                    Exception = new Exception($"Failed, This Course Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (updateCourseDto.CourseName is not null) { course.CourseName = updateCourseDto.CourseName; }
            if (updateCourseDto.Description is not null) { course.Description = updateCourseDto.Description; }
            if (updateCourseDto.Price != 0) { course.Price = (decimal)updateCourseDto.Price; }
            //var updateCourse = _mapper.Map<Course>(updateCourseDto);
            course.Updated = DateTime.Now;
            _unitOfWork.Courses.Update(course);
            await _unitOfWork.Save();
            return new ResponsDto<CourseDTO>() { Dto = _mapper.Map<CourseDTO>(course) };
        }
    }
}
