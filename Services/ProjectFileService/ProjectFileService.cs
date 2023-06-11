using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School.Core.IRepository.IUnitOfWork;
using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Services.FilesService;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Microsoft.EntityFrameworkCore;
using Cooking_School.Dtos.SubmitedFileDto;

namespace Cooking_School.Services.SubmitedFileService
{
    public class ProjectFileService : IProjectFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        public ProjectFileService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _configuration = configuration;
        }
        public async Task<ResponsDto<SubmitedFileDTO>> DeleteProjectFile(int submitedFileId)
        {
            var submitedFile = await _unitOfWork.ProjectFiles.Get(x => x.Id == submitedFileId);
            if (submitedFile is null)
            {
                return new ResponsDto<SubmitedFileDTO>()
                {
                    Exception = new Exception("Failed, This submitedFile Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var fileName = submitedFile.FilePath.Split('/')[0];

            var res = await _fileService.DeleteBlob(fileName);
            if (res.error == true)
            {
                return new ResponsDto<SubmitedFileDTO>()
                {
                    Exception = new Exception(res.Status),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            // Save new file
            //string fileName = Guid.NewGuid() + Path.GetFileName(UploadImage.FileName);
            await _unitOfWork.ProjectFiles.Delete(submitedFileId);
            await _unitOfWork.Save();
            return new ResponsDto<SubmitedFileDTO>();
        }

        public async Task<ResponsDto<BlobFile>> DownloadProjectFile(string FileName)
        {
            BlobFile blobFile = await _fileService.DownloadAsync(FileName);
            if (blobFile is null)
            {
                return new ResponsDto<BlobFile>
                {
                    Exception = new Exception($"there is problem with download {FileName} "),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
            return new ResponsDto<BlobFile>()
            {
                Dto = blobFile
            };
        }



        public async Task<ResponsDto<SubmitedFileDTO>> GetAllProjectFile(int prjectId, RequestParam requestParams = null)
        {
            if (requestParams == null)
            {
                var submitedFils = new List<ProjectTraineeFile>();
                var projectFiles = await _unitOfWork.ProjectFiles.GetAll(x => x.ProjectId == prjectId);
                
                var projectFileDto = _mapper.Map<IList<SubmitedFileDTO>>(projectFiles);
                return new ResponsDto<SubmitedFileDTO>()
                {
                    ListDto = projectFileDto
                };
            }
            else
            {
                var submitedFilsPag = new List<ProjectTraineeFile>();
                var projectFilesPag = await _unitOfWork.ProjectFiles.GetPagedList(requestParams, x => x.ProjectId == prjectId);

                var projectFileDtoPag = _mapper.Map<IList<SubmitedFileDTO>>(projectFilesPag);
                return new ResponsDto<SubmitedFileDTO>()
                {
                    ListDto = projectFileDtoPag
                };
            }
        }


        public async Task<ResponsDto<SubmitedFileDTO>> UpdateProjectFile(UpdateSubmitedFileDto submitedFilesDto)
        {
            var project = await _unitOfWork.ProjectFiles.Get(x => x.ProjectId == submitedFilesDto.ProjectId);
            if (project == null)
            {
                return new ResponsDto<SubmitedFileDTO>()
                {
                    Exception = new Exception("this ProjectId not exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var submitedFiles = await _unitOfWork.ProjectFiles.GetAll(x => x.ProjectId == project.Id);
            if (submitedFiles == null)
            {
                return new ResponsDto<SubmitedFileDTO>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Exception = new Exception("Failed, This submitedFile Is Not Exist")

                };
            }

            foreach (var file in submitedFiles)
            {
                var splitedFile = file.FilePath.Split('/');
                var resDelete = await _fileService.DeleteBlob(splitedFile[splitedFile.Length - 1]);
                if (resDelete.error == true)
                {
                    return new ResponsDto<SubmitedFileDTO>()
                    {
                        Exception = new Exception(resDelete.Status),
                    };
                }
            }

            _unitOfWork.ProjectFiles.DeleteRange(submitedFiles);
            foreach (var file in submitedFilesDto.Files)
            {
                var res = await _fileService.UploadAsync(file);

                if (res.error == true)
                {
                    return new ResponsDto<SubmitedFileDTO>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    };
                }
                ProjectFile submitedFile = new ProjectFile();
                submitedFile.FilePath = res.Blob.Uri;
                submitedFile.ProjectId = project.Id;
                submitedFile.Created = DateTime.Now;

                await _unitOfWork.ProjectFiles.Insert(submitedFile);
                await _unitOfWork.Save();
            }

            return new ResponsDto<SubmitedFileDTO>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }

        public async Task<ResponsDto<SubmitedFileDTO>> UploadProjectFile(CreateSubmitedFileDto submitedFilesDto)
        {
            foreach (var file in submitedFilesDto.Files)
            {
                string fileName = file.FileName;

                fileName = Path.GetFileName(fileName);

                var res = await _fileService.UploadAsync(file);

                if (res.error == true)
                {
                    return new ResponsDto<SubmitedFileDTO>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    };
                }
                ProjectFile projectFile = new ProjectFile();
                projectFile.FilePath = res.Blob.Uri;
                projectFile.ProjectId = submitedFilesDto.ProjectId;
                projectFile.Created = DateTime.Now;

                
                await _unitOfWork.ProjectFiles.Insert(projectFile);
                await _unitOfWork.Save();
            }

            return new ResponsDto<SubmitedFileDTO>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
