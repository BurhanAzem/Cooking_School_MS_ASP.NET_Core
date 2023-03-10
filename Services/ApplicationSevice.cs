﻿using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.ProjectDto;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Cooking_School_ASP.NET.Services
{
    public class ApplicationSevice : IApplicationSevice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ApplicationSevice(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponsDto<ApplicationDTO>> AcceptApplication(int applicationId)
        {
            var application = await _unitOfWork.Applications.Get(x => x.Id == applicationId);
            if (application is null)
            {
                return new ResponsDto<ApplicationDTO>()
                {
                    Exception = new Exception("Failed, This applicationId Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            application.status = status_apply.accepted;
            _unitOfWork.Applications.Update(application);
            Trainee_Course trainee_Course = new Trainee_Course();
            trainee_Course.TraineeId = application.TraineeId;
            trainee_Course.CourseId = application.CookClassId;
            trainee_Course.Created = DateTime.Now;
            await _unitOfWork.Trainee_Courses.Insert(trainee_Course);
            await _unitOfWork.Save();
            return new ResponsDto<ApplicationDTO>();
        }
            public async Task<ResponsDto<ApplicationDTO>> CreateApplication(CreateApplicationDto applicationDto)
        {
            if (await _unitOfWork.CookClasses.Get(x => x.Id == applicationDto.CookClassId) is not null)
            {
                return new ResponsDto<ApplicationDTO>()
                {
                    Exception = new Exception($"Failed, CookClass {applicationDto.CookClassId} Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == applicationDto.TraineeId) is not null)
            {
                return new ResponsDto<ApplicationDTO>()
                {
                    Exception = new Exception($"Failed, Trainee {applicationDto.TraineeId} Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var application = _mapper.Map<ApplicationT>(applicationDto);
            await _unitOfWork.Applications.Insert(application);
            await _unitOfWork.Save();
            var projectDto = _mapper.Map<ApplicationDTO>(application);
            return new ResponsDto<ApplicationDTO>()
            {
                Dto = projectDto,
            };
        }

        public async Task<ResponsDto<ApplicationDTO>> GetAllApplicationsToChef(int cheefId)
        {
            Chef chef = (Chef)await _unitOfWork.Users.Get(x => x.Id == cheefId, include: x => x.Include(s => ((Chef)s).CookClasses));
            var cookClasses = chef.CookClasses;
            if(cookClasses is null)
            {
                return new ResponsDto<ApplicationDTO>()
                {
                    Exception = new Exception("Chef dose not have CookClasses"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            IList<ApplicationT> applications = new List<ApplicationT>();    
            foreach(var cookClass in cookClasses)
            {
                foreach(var application in cookClass.Applications)
                {
                    applications.Add(application);
                }
            }
            var applicationsDto = _mapper.Map<IList<ApplicationDTO>>(applications);
            return new ResponsDto<ApplicationDTO>()
            {
                ListDto = applicationsDto
            };
        }

        public async Task<ResponsDto<ApplicationDTO>> GetAllApplicationToClass(int classId)
        {
            CookClass cookClass = await _unitOfWork.CookClasses.Get(x => x.Id == classId, include: x => x.Include(s => s.Applications));
            IList<ApplicationT> applications = new List<ApplicationT>();
            foreach (var application in cookClass.Applications)
            {
                applications.Add(application);

            }
            var applicationsDto = _mapper.Map<IList<ApplicationDTO>>(applications);
            return new ResponsDto<ApplicationDTO>()
            {
                ListDto = applicationsDto
            };
        }

        public async Task<ResponsDto<ApplicationDTO>> RejectApplication(int applicationId)
        {
            var application = await _unitOfWork.Applications.Get(x => x.Id == applicationId);
            if (application is null)
            {
                return new ResponsDto<ApplicationDTO>()
                {
                    Exception = new Exception("Failed, This applicationId Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            application.status = status_apply.rejected;
            _unitOfWork.Applications.Update(application);
            await _unitOfWork.Save();
            return new ResponsDto<ApplicationDTO>();
        }
    }
}
