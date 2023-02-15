using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.CourseDto;
using Cooking_School_ASP.NET.Dtos.ProjectDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponsProjectDto> CreateProject(CreateProjectDto createProjectDto, int chefId)
        {
            if (await _unitOfWork.CookClasses.Get(x => x.Id == createProjectDto.CookClassId) is not null)
            {
                return new ResponsProjectDto()
                {
                    Exception = new Exception("Failed, CookClass Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == chefId) is not null)
            {
                return new ResponsProjectDto()
                {
                    Exception = new Exception("Failed, Chef Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var project = _mapper.Map<Project>(createProjectDto);
            await _unitOfWork.Projects.Insert(project);
            await _unitOfWork.Save();
            var projectDto = _mapper.Map<ProjectDTO>(project);
            return new ResponsProjectDto()
            {
                projectDto = projectDto,
            };
        }

        public async Task<ResponsProjectDto> DeleteProject(int projectId, int classId)
        {
            if (await _unitOfWork.Projects.Get(x => x.Id == projectId) is null)
            {
                return new ResponsProjectDto()
                {
                    Exception = new Exception("Failed, This Project Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.CookClasses.Delete(projectId);
            await _unitOfWork.Save();
            return new ResponsProjectDto()
            {
            };
        }

        public async Task<IList<ProjectDTO>> GetAllProject(RequestParam requestParams)
        {
            if (requestParams == null)
            {
                var projects = await _unitOfWork.Projects.GetAll();
                var projectDto = _mapper.Map<IList<ProjectDTO>>(projects);
                return projectDto;
            }
            var projectsPag = await _unitOfWork.Projects.GetPagedList(requestParams, include: x => x.Include(s => s.projectFiles));
            var projectDtoPag = _mapper.Map<IList<ProjectDTO>>(projectsPag);
            return projectDtoPag;
        }

        public async Task<ResponsProjectDto> GetProjectById(int projectId)
        {
            var project = await _unitOfWork.Projects.Get(x => x.Id == projectId);
            if (project == null)
            {
                return new ResponsProjectDto
                {
                    Exception = new Exception($"Failed, this Project with {projectId} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var projectDto = _mapper.Map<ProjectDTO>(project);
            return new ResponsProjectDto
            {
                projectDto = projectDto,
            };
        }

        public async Task<ResponsProjectDto> UpdateProject(int projectId, UpdateProjectDto updateProjectDto)
        {
            if (await _unitOfWork.Projects.Get(x => x.Id == projectId) is null)
            {
                return new ResponsProjectDto()
                {
                    Exception = new Exception($"Failed, This Project Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var updatedCookClass = _mapper.Map<Project>(updateProjectDto);
            updatedCookClass.Updated = DateTime.Now;
            _unitOfWork.Projects.Update(updatedCookClass);
            await _unitOfWork.Save();
            return new ResponsProjectDto()
            {
            };
        }
    }
}
