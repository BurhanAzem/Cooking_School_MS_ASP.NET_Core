using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Dtos.SubmitedFileDto;
using Cooking_School_ASP.NET.Dtos;


namespace Cooking_School.Services.ProjectTraineeFileService
{
    public interface IProjectTraineeFileService
    {
        Task<ResponsDto<BlobFile>> DownloadProjectTraineeFile(string FileName);
        Task<ResponsDto<SubmitedFileDTO>> UploadProjectTraineeFile(CreateSubmitedFileDto projectFileId);
        Task<ResponsDto<SubmitedFileDTO>> GetAllProjectTraineeFile(int prjectId, RequestParam requestParams = null);
        Task<ResponsDto<SubmitedFileDTO>> DeleteProjectTraineeFile(int submitedFileId);
        Task<ResponsDto<SubmitedFileDTO>> UpdateProjectTraineeFile(UpdateSubmitedFileDto projectFileDto);
    }
}
