using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services.ProjectFileService
{
    public interface ISubmitedFileService
    {
        Task<ResponsDto<BlobFile>> DownloadSubmitedFile(string FileName);
        Task<ResponsDto<SubmitedFileDto>> EvaluateTraineeProject(decimal mark, int projectFileId);
        Task<ResponsDto<SubmitedFileDto>> UploadSubmitedFile(CreateSubmitedFileDto projectFileId);
        Task<ResponsDto<SubmitedFileDto>> GetAllSubmitedFile(RequestParam requestParams = null);
        Task<ResponsDto<SubmitedFileDto>> DeleteSubmitedFile(int projectFileDto);
        Task<ResponsDto<SubmitedFileDto>> UpdateSubmitedFile(UpdateSubmitedFileDto projectFileDto);
    }
}
