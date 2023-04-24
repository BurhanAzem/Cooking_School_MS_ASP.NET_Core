using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services.FilesService;
using Cooking_School_ASP.NET.Services.ProjectFileService;
using Cooking_School_ASP.NET_.Models;
using CookingSchoolASP.NET.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.Services.SubmitedFileService
{
    public class SubmitedFileService : ISubmitedFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        public SubmitedFileService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _configuration = configuration;
        }
        public async Task<ResponsDto<SubmitedFileDto>> DeleteSubmitedFile(int submitedFileId)
        {
            var submitedFile = await _unitOfWork.SubmitedFiles.Get(x => x.Id == submitedFileId);
            if (submitedFile is null)
            {
                return new ResponsDto<SubmitedFileDto>()
                {
                    Exception = new Exception("Failed, This submitedFile Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var fileName = submitedFile.ContentPath.Split('/')[0];

            var res = await _fileService.DeleteBlob(fileName);
            if (res.error == true)
            {
                return new ResponsDto<SubmitedFileDto>()
                {
                    Exception = new Exception(res.Status),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            } 
        
            // Save new file
            //string fileName = Guid.NewGuid() + Path.GetFileName(UploadImage.FileName);
            await _unitOfWork.SubmitedFiles.Delete(submitedFileId);
            await _unitOfWork.Save();
            return new ResponsDto<SubmitedFileDto>();
        }

        public async Task<ResponsDto<BlobFile>> DownloadSubmitedFile(string FileName)
        {
            BlobFile blobFile = await _fileService.DownloadAsync(FileName);
            if(blobFile is null)
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

        public async Task<ResponsDto<SubmitedFileDto>> EvaluateTraineeProject(decimal mark, int submitedFileId)
        {
            var submitedFile = await _unitOfWork.SubmitedFiles.Get(x => x.Id == submitedFileId);
            if (submitedFile is null)
            {
                return new ResponsDto<SubmitedFileDto>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Exception = new Exception("Failed, This submitedFile Is Not Exist")

                };
            }
            else
            {
                submitedFile.Evalution = mark;
            }

            _unitOfWork.SubmitedFiles.Update(submitedFile);
            await _unitOfWork.Save();
            return new ResponsDto<SubmitedFileDto>();
        }

        public async Task<ResponsDto<SubmitedFileDto>> GetAllSubmitedFile(RequestParam requestParams = null)
        {

            if (requestParams == null)
            {
                var submitedFils = await _unitOfWork.SubmitedFiles.GetAll();
                var submiteFilsDto = _mapper.Map<IList<SubmitedFileDto>>(submitedFils);
                return new ResponsDto<SubmitedFileDto>()
                {
                    ListDto = submiteFilsDto,
                };
            }
            var projectFilsPag = await _unitOfWork.SubmitedFiles.GetPagedList(requestParams);
            var projectFilsDtoPag = _mapper.Map<IList<SubmitedFileDto>>(projectFilsPag);
            return new ResponsDto<SubmitedFileDto>()
            {
                ListDto = projectFilsDtoPag,
            };
        }

        public async Task<ResponsDto<SubmitedFileDto>> UpdateSubmitedFile(UpdateSubmitedFileDto submitedFilesDto)
        {
            var submitedFiles = await _unitOfWork.SubmitedFiles.GetAll(x => x.ProjectId == submitedFilesDto.ProjectId);
            if (submitedFiles.Count == 0)
            {
                return new ResponsDto<SubmitedFileDto>()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Exception = new Exception("Failed, This submitedFile Is Not Exist")

                };
            }
            _unitOfWork.SubmitedFiles.DeleteRange(submitedFiles);
            foreach(var file in submitedFilesDto.Files)
            {
                var resDelete = await _fileService.DeleteBlob(file.FileName);
                if (resDelete.error == false)
                {
                    return new ResponsDto<SubmitedFileDto>()
                    {
                        Exception = new Exception(resDelete.Status),
                    };
                }
                var res = await _fileService.UploadAsync(file);

                if (res.error == false)
                {
                    return new ResponsDto<SubmitedFileDto>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    };
                }
                SubmitedFile submitedFile = new SubmitedFile();
                submitedFile.ContentPath = res.Blob.Uri;
                submitedFile.ProjectId = submitedFile.ProjectId;
                submitedFile.TraineeId = submitedFile.TraineeId;
                submitedFile.Created = DateTime.Now;

                await _unitOfWork.SubmitedFiles.Insert(submitedFile);
                await _unitOfWork.Save();
            }

            return new ResponsDto<SubmitedFileDto>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }

        public async Task<ResponsDto<SubmitedFileDto>> UploadSubmitedFile(CreateSubmitedFileDto submitedFilesDto)
        {

            foreach(var file in submitedFilesDto.Files)
            {
                string fileName = file.FileName;

                fileName = Path.GetFileName(fileName);

                var res = await _fileService.UploadAsync(file);

                if (res.error == false)
                {
                    return new ResponsDto<SubmitedFileDto>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    };
                }
                SubmitedFile submitedFile = new SubmitedFile();
                submitedFile.ContentPath = res.Blob.Uri;
                submitedFile.ProjectId = submitedFile.ProjectId;
                submitedFile.TraineeId = submitedFile.TraineeId;
                submitedFile.Created = DateTime.Now;

                var project = await _unitOfWork.Projects.Get(x => x.Id == submitedFile.ProjectId);
                if (submitedFile.Created > project.ExpirDate)
                {
                    submitedFile.status = status_project.submitedLate;
                }
                else
                {
                    submitedFile.status = status_project.submited;
                }
                await _unitOfWork.SubmitedFiles.Insert(submitedFile);
                await _unitOfWork.Save();
            }
            
            return new ResponsDto<SubmitedFileDto>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
