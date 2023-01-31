using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.CourseDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET_.Models;
using System.Runtime;

namespace Cooking_School_ASP.NET.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<ChefDto, Chef>().ReverseMap();
            CreateMap<CreateChefDto, Chef>()
                .ForMember(c => c.PasswordHashed, option => option.Ignore())
                .ForMember(c => c.PasswordSlot, option => option.Ignore())
                .ForMember(t => t.Created, option => option.Ignore());

            CreateMap<TraineeDto, Trainee>().ReverseMap();
            CreateMap<CreateTraineeDto, Trainee>()
                .ForMember(t => t.Created, option => option.Ignore());

            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<CreateCourseDto, Course>()
                .ForMember(t => t.Created, option => option.Ignore());

            CreateMap<CookClassDto, CookClass>().ReverseMap();
            CreateMap<CreateCookClassDto, CookClass>()
                .ForMember(t => t.Created, option => option.Ignore());

            CreateMap<ApplicationDto, ApplicationT>().ReverseMap();
            CreateMap<CreateApplicationDto, ApplicationT>()
                .ForMember(t => t.Created, option => option.Ignore())
                .ForMember(a => a.DateOfApplay, option => option.Ignore());

            CreateMap<ProjectDto, Project>().ReverseMap();
            CreateMap<CreateProjectDto, Project>()
                .ForMember(t => t.Created, option => option.Ignore());

            CreateMap<ProjectFileDto, ProjectFile>().ReverseMap();
            CreateMap<CreateProjectFileDto, ProjectFile>()
                .ForMember(t => t.Created, option => option.Ignore());

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();



        }
    }
}
