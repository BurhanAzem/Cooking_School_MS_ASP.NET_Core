using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.AdminDto;
using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Services
{
    public interface IApplicationSevice
    {
        Task<ResponsDto<ApplicationDTO>> GetAllApplicationsToChef(int chefId);
        Task<ResponsDto<ApplicationDTO>> AcceptApplication(int applicationId);
        Task<ResponsDto<ApplicationDTO>> GetAllApplicationToClass(int classId);
        Task<ResponsDto<ApplicationDTO>> RejectApplication(int applicationId);
        Task<ResponsDto<ApplicationDTO>> CreateApplication(CreateApplicationDto applicationDto);
    }
}
