using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services.ProjectService
{
    public interface IProjectService
    {
        Task<ResponsDto<ProjectDTO>> CreateProject(CreateProjectDto createProjectDto, int cookClassId);
        Task<ResponsDto<ProjectDTO>> DeleteProject(int projectId);
        Task<ResponsDto<ProjectDTO>> UpdateProject(int projectId, UpdateProjectDto updateProjectDto);
        Task<ResponsDto<ProjectDTO>> GetProjectById(int projectId);
        Task<ResponsDto<ProjectDTO>> GetAllProjectOfCookClass(RequestParam requestParams, int cookClassId);
    }
}
