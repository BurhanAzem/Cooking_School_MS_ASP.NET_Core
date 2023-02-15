using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Services
{
    public interface IApplicationSevice
    {
        Task<ResponsApplicationDto> GetAllApplicationsToChef(int chefId);
        Task<ResponsApplicationDto> AcceptApplication(int applicationId);
        Task<ResponsApplicationDto> GetAllApplicationToClass(int classId);
        Task<ResponsApplicationDto> RejectApplication(int applicationId);
        Task<ResponsApplicationDto> CreateApplication(CreateApplicationDto applicationDto);
    }
}
