using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School.Core.IRepository.IUnitOfWork;
using Cooking_School.Core.Models;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.Dtos;
using Cooking_School.Services.Dtos.SubmitedFileDto;
using Cooking_School.Services.FilesService;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cooking_School.Services.ProjectTraineeFileService
{
    public class ProjectTraineeFileService : IProjectTraineeFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        public ProjectTraineeFileService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _configuration = configuration;
        }
        public async Task<ResponsDto<SubmitedFileDTO>> DeleteProjectTraineeFile(int submitedFileId)
        {
            var submitedFile = await _unitOfWork.ProjectTraineeFiles.Get(x => x.Id == submitedFileId);
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
            await _unitOfWork.ProjectTraineeFiles.Delete(submitedFileId);
            await _unitOfWork.Save();
            return new ResponsDto<SubmitedFileDTO>();
        }

        public async Task<ResponsDto<BlobFile>> DownloadProjectTraineeFile(string FileName)
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



        public async Task<ResponsDto<SubmitedFileDTO>> GetAllProjectTraineeFile(int prjectId, RequestParam requestParams = null)
        {
            if (requestParams == null)
            {
                var submitedFils = new List<ProjectTraineeFile>();
                var projectTrainees = await _unitOfWork.ProjectTrainees.GetAll(x => x.ProjectId == prjectId);
                foreach (var trine in projectTrainees)
                {
                    var files = await _unitOfWork.ProjectTraineeFiles.GetAll(x => x.ProjectTraineeId == trine.Id);
                    submitedFils.AddRange(files);
                }
                var submiteFilsDto = _mapper.Map<IList<SubmitedFileDTO>>(submitedFils);
                return new ResponsDto<SubmitedFileDTO>()
                {
                    ListDto = submiteFilsDto,
                };
            }
            else
            {
                var submitedFilsPag = new List<ProjectTraineeFile>();
                var projectTraineesPag = await _unitOfWork.ProjectTrainees.GetPagedList(requestParams, x => x.ProjectId == prjectId);
                foreach (var trine in projectTraineesPag)
                {
                    var files = await _unitOfWork.ProjectTraineeFiles.GetPagedList(requestParams, x => x.ProjectTraineeId == trine.Id);
                    submitedFilsPag.AddRange(files);
                }
                var submiteFilsDtoPag = _mapper.Map<IList<SubmitedFileDTO>>(submitedFilsPag);
                return new ResponsDto<SubmitedFileDTO>()
                {
                    ListDto = submiteFilsDtoPag,
                };
            }
        }


        public async Task<ResponsDto<SubmitedFileDTO>> UpdateProjectTraineeFile(UpdateSubmitedFileDto submitedFilesDto)
        {
            var projectTrainee = await _unitOfWork.ProjectTrainees.Get(x => x.TraineeId == submitedFilesDto.TraineeId && x.ProjectId == submitedFilesDto.ProjectId);
            if (projectTrainee == null)
            {
                return new ResponsDto<SubmitedFileDTO>()
                {
                    Exception = new Exception("this ProjectTrinee not exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var submitedFiles = await _unitOfWork.ProjectTraineeFiles.GetAll(x => x.ProjectTraineeId == projectTrainee.Id);
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

            _unitOfWork.ProjectTraineeFiles.DeleteRange(submitedFiles);
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
                ProjectTraineeFile submitedFile = new ProjectTraineeFile();
                submitedFile.FilePath = res.Blob.Uri;
                submitedFile.ProjectTraineeId = projectTrainee.Id;
                submitedFile.Created = DateTime.Now;

                await _unitOfWork.ProjectTraineeFiles.Insert(submitedFile);
                await _unitOfWork.Save();
            }

            return new ResponsDto<SubmitedFileDTO>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }

        public async Task<ResponsDto<SubmitedFileDTO>> UploadProjectTraineeFile(CreateSubmitedFileDto submitedFilesDto)
        {
            var projectTrainee = await _unitOfWork.ProjectTrainees.Get(x => x.TraineeId == submitedFilesDto.TraineeId && x.ProjectId == submitedFilesDto.ProjectId);
            if(projectTrainee == null)
            {
                return new ResponsDto<SubmitedFileDTO>()
                {
                    Exception= new Exception("this ProjectTrinee not exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest 
                };
            }

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
                ProjectTraineeFile submitedFile = new ProjectTraineeFile();
                submitedFile.FilePath = res.Blob.Uri;
                submitedFile.ProjectTraineeId = projectTrainee.Id;
                submitedFile.Created = DateTime.Now;

                var project = await _unitOfWork.Projects.Get(x => x.Id == projectTrainee.ProjectId);
                if (projectTrainee.Created > project.ExpirDate)
                {
                    projectTrainee.status = status_project.submitedLate;
                }
                else
                {
                    projectTrainee.status = status_project.submited;
                }
                await _unitOfWork.ProjectTraineeFiles.Insert(submitedFile);
                await _unitOfWork.Save();
            }

            return new ResponsDto<SubmitedFileDTO>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
