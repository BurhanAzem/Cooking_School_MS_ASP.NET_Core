using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Dtos.SubmitedFileDto;
using Cooking_School_ASP.NET.Dtos;


namespace Cooking_School.Services.SubmitedFileService
{
    public interface IProjectFileService
    {
        Task<ResponsDto<BlobFile>> DownloadProjectFile(string FileName);
        Task<ResponsDto<SubmitedFileDTO>> UploadProjectFile(CreateSubmitedFileDto projectFileId);
        Task<ResponsDto<SubmitedFileDTO>> GetAllProjectFile(int prjectId, RequestParam requestParams = null);
        Task<ResponsDto<SubmitedFileDTO>> DeleteProjectFile(int submitedFileId);
        Task<ResponsDto<SubmitedFileDTO>> UpdateProjectFile(UpdateSubmitedFileDto projectFileDto);
    }
}
