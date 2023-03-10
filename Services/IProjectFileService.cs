using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services
{
    public interface IProjectFileService
    {
        Task<ResponsDto<ProjectFileDTO>> EvaluateTraineeProject(decimal mark, int projectFileId);
        Task<ResponsDto<ProjectFileDTO>> UploadeProjectFile(CreateProjectFileDto projectFileId);
        Task<ResponsDto<ProjectFileDTO>> GetAllProjectFile(RequestParam requestParams = null);
        Task<ResponsDto<ProjectFileDTO>> DeleteProjectFile(int projectFileDto);

    }
}
