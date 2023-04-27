using AutoMapper;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services.FilesService;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<ResponsDto<ProjectDTO>> CreateProject(CreateProjectDto createProjectDto, int cookClassId)
        {
            if (await _unitOfWork.CookClasses.Get(x => x.Id == cookClassId) is null)
            {
                return new ResponsDto<ProjectDTO>()
                {
                    Exception = new Exception("Failed, CookClass Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == cookClassId) is null)
            {
                return new ResponsDto<ProjectDTO>()
                {
                    Exception = new Exception("Failed, Chef Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var project = _mapper.Map<Project>(createProjectDto);
            project.CookClassId = cookClassId;
            await _unitOfWork.Projects.Insert(project);
            await _unitOfWork.Save();
            foreach (var file in createProjectDto.Files)
            {
                BlobResponse res = await _fileService.UploadAsync(file);
                if (res.error == true)
                {
                    return new ResponsDto<ProjectDTO>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    };
                }
                ProjectFile projectFile = new ProjectFile();
                projectFile.ProjectId = project.Id;
                projectFile.ContentPath = res.Blob.Uri; ;
                await _unitOfWork.ProjectFiles.Insert(projectFile);
                await _unitOfWork.Save();
            }
            var projectDto = _mapper.Map<ProjectDTO>(project);
            return new ResponsDto<ProjectDTO>()
            {
                Dto = projectDto,
            };
        }

        public async Task<ResponsDto<ProjectDTO>> DeleteProject(int projectId)
        {
            if (await _unitOfWork.Projects.Get(x => x.Id == projectId) is null)
            {
                return new ResponsDto<ProjectDTO>()
                {
                    Exception = new Exception("Failed, This Project Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var projectFiles = await _unitOfWork.ProjectFiles.GetAll(x => x.Id == projectId);
            _unitOfWork.ProjectFiles.DeleteRange(projectFiles);
            foreach (var file in projectFiles)
            {
                var fileName = file.ContentPath.Split('/')[0];

                var res = await _fileService.DeleteBlob(fileName);
                if (res.error == true)
                {
                    return new ResponsDto<ProjectDTO>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };
                }
            }

            await _unitOfWork.CookClasses.Delete(projectId);
            await _unitOfWork.Save();
            return new ResponsDto<ProjectDTO>()
            {
            };
        }

        public async Task<ResponsDto<ProjectDTO>> GetAllProjectOfCookClass(RequestParam requestParams, int cookClassId)
        {
            if (requestParams == null)
            {
                var projects = await _unitOfWork.Projects.GetAll(x => x.CookClassId == cookClassId);
                var projectDto = _mapper.Map<IList<ProjectDTO>>(projects);
                return new ResponsDto<ProjectDTO>
                {
                    ListDto = projectDto
                };
            }
            var projectsPag = await _unitOfWork.Projects.GetPagedList(requestParams, include: x => x.Include(s => s.ProjectFiles));
            var projectDtoPag = _mapper.Map<IList<ProjectDTO>>(projectsPag);
            return new ResponsDto<ProjectDTO>
            {
                ListDto = projectDtoPag
            };
        }

        public async Task<ResponsDto<ProjectDTO>> GetProjectById(int projectId)
        {
            var project = await _unitOfWork.Projects.Get(x => x.Id == projectId, include: x => x.Include(s => s.ProjectFiles));
            if (project == null)
            {
                return new ResponsDto<ProjectDTO>
                {
                    Exception = new Exception($"Failed, this Project with {projectId} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var projectDto = _mapper.Map<ProjectDTO>(project);
            return new ResponsDto<ProjectDTO>
            {
                Dto = projectDto,
            };
        }

        public async Task<ResponsDto<ProjectDTO>> UpdateProject(int projectId, UpdateProjectDto updateProjectDto)
        {
            var project = await _unitOfWork.Projects.Get(x => x.Id == projectId);
            if (project is null)
            {
                return new ResponsDto<ProjectDTO>()
                {
                    Exception = new Exception($"Failed, This Project Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            if (updateProjectDto.CookClassId != 0) { project.CookClassId = (int)updateProjectDto.CookClassId; }
            if (updateProjectDto.ProjectName is not null) { project.ProjectName = updateProjectDto.ProjectName; }
            if (updateProjectDto.ExpirDate != null) { project.ExpirDate = (DateTime)updateProjectDto.ExpirDate; }
            if (updateProjectDto.Description is not null) { project.Description = updateProjectDto.Description; }

            project.Updated = DateTime.Now;


            var projectFiles = await _unitOfWork.ProjectFiles.GetAll(x => x.ProjectId == projectId);
            if (projectFiles.Count == 0)
            {
                return new ResponsDto<ProjectDTO>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Exception = new Exception("Failed, This submitedFile Is Not Exist")

                };
            }
            _unitOfWork.ProjectFiles.DeleteRange(projectFiles);
            foreach (var file in updateProjectDto.Files)
            {
                var resDelete = await _fileService.DeleteBlob(file.FileName);
                if (resDelete.error == true)
                {
                    return new ResponsDto<ProjectDTO>()
                    {
                        Exception = new Exception(resDelete.Status),
                    };
                }
                var res = await _fileService.UploadAsync(file);

                if (res.error == true)
                {
                    return new ResponsDto<ProjectDTO>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    };
                }
                ProjectFile projectFile = new ProjectFile();
                projectFile.ContentPath = res.Blob.Uri;
                projectFile.ProjectId = projectFile.ProjectId;
                projectFile.Created = DateTime.Now;

                await _unitOfWork.ProjectFiles.Insert(projectFile);
                await _unitOfWork.Save();
            }

            return new ResponsDto<ProjectDTO>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
