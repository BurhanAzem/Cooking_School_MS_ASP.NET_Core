using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;

namespace Cooking_School_ASP.NET.Services
{
    public interface IProjectFileService
    {
        Task<ResponsPrpjectFileDto> EvaluateTraineeProject(decimal mark, int projectFileId);
        Task<ResponsPrpjectFileDto> UploadeProjectFile(CreateProjectFileDto projectFileId);
        Task<IList<ProjectFileDTO>> GetAllProjectFile(RequestParam requestParams = null);
        Task<ResponsPrpjectFileDto> DeleteProjectFile(int projectFileDto);

    }
}
