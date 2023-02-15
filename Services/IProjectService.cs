using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;

namespace Cooking_School_ASP.NET.Services
{
    public interface IProjectService
    {
        Task<ResponsProjectDto> CreateProject(CreateProjectDto createProjectDto, int chefId);
        Task<ResponsProjectDto> DeleteProject(int projectId, int classId);
        Task<ResponsProjectDto> UpdateProject(int projectId, UpdateProjectDto updateProjectDto);
        Task<ResponsProjectDto> GetProjectById(int projectId);
        Task<IList<ProjectDTO>> GetAllProject(RequestParam requestParams);
    }
}
