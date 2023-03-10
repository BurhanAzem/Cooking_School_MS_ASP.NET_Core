using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services
{
    public interface IProjectService
    {
        Task<ResponsDto<ProjectDTO>> CreateProject(CreateProjectDto createProjectDto, int chefId);
        Task<ResponsDto<ProjectDTO>> DeleteProject(int projectId, int classId);
        Task<ResponsDto<ProjectDTO>> UpdateProject(int projectId, UpdateProjectDto updateProjectDto);
        Task<ResponsDto<ProjectDTO>> GetProjectById(int projectId);
        Task<ResponsDto<ProjectDTO>> GetAllProject(RequestParam requestParams);
    }
}
