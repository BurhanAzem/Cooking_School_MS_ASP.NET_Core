using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET_.Models;
using CookingSchoolASP.NET.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.Services
{
    public class ProjectFileService : IProjectFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectFileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponsPrpjectFileDto> DeleteProjectFile(int projectFileId)
        {
            if (await _unitOfWork.ProjectFiles.Get(x => x.Id == projectFileId) is null)
            {
                return new ResponsPrpjectFileDto()
                {
                    Exception = new Exception("Failed, This projectFile Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.ProjectFiles.Delete(projectFileId);
            await _unitOfWork.Save();
            return new ResponsPrpjectFileDto();
        }

        public async Task<ResponsPrpjectFileDto> EvaluateTraineeProject(decimal mark, int projectFileId)
        {
            var projectFile = await _unitOfWork.ProjectFiles.Get(x => x.Id == projectFileId);
            if (projectFile is null)
            {
                return new ResponsPrpjectFileDto()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Exception = new Exception("Failed, This projectFile Is Not Exist")
                    
                };
            }
            if (projectFile.status == Models.status_project.notSubmited)
            {
                return new ResponsPrpjectFileDto()
                {
                    Exception = new Exception("Failed, This projectFile Is Not Submited"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            else if (projectFile.status == Models.status_project.submitedLate)
            {
                projectFile.Evalution = mark - (mark * (decimal)0.01);
            }
            else
            {
                projectFile.Evalution = mark;
            }

            _unitOfWork.ProjectFiles.Update(projectFile);
            await _unitOfWork.Save();
            return new ResponsPrpjectFileDto();
        }

        public async Task<IList<ProjectFileDTO>> GetAllProjectFile(RequestParam requestParams = null)
        {

            if (requestParams == null)
            {
                var projectFils = await _unitOfWork.Projects.GetAll();
                var projectFilsDto = _mapper.Map<IList<ProjectFileDTO>>(projectFils);
                return projectFilsDto;
            }
            var projectFilsPag = await _unitOfWork.Projects.GetPagedList(requestParams);
            var projectFilsDtoPag = _mapper.Map<IList<ProjectFileDTO>>(projectFilsPag);
            return projectFilsDtoPag;
        }

        public async Task<ResponsPrpjectFileDto> UploadeProjectFile(CreateProjectFileDto projectFilesDto)
        {
            var projectFile = _mapper.Map<ProjectFile>(projectFilesDto);

            projectFile.Created = DateTime.Now;

            string fileName = projectFilesDto.content.FileName;

            fileName = Path.GetFileName(fileName);

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\projectFile", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            await projectFilesDto.content.CopyToAsync(stream);

            projectFile.ContentPath = uploadpath;
            var project = await _unitOfWork.Projects.Get(x => x.Id == projectFile.ProjectId); 
            if (projectFile.Created > project.ExpirDate)
            {
                projectFile.status = status_project.submitedLate;
            }
            else
            {
                projectFile.status = status_project.submited;
            }
            _unitOfWork.ProjectFiles.Update(projectFile);
            await _unitOfWork.Save();

            return new ResponsPrpjectFileDto();
        }
    }
}
