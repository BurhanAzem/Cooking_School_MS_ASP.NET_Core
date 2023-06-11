using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos;
using Cooking_School.Dtos.ApplicationDto;


namespace Cooking_School.Services.ApplicationService
{
    public interface IApplicationSevice
    {
        Task<ResponsDto<ApplicationDTO>> GetAllApplications();
        Task<ResponsDto<ApplicationDTO>> GetAllApplicationsToChef(int chefId);
        Task<ResponsDto<ApplicationDTO>> AcceptApplication(int applicationId);
        Task<ResponsDto<ApplicationDTO>> GetAllApplicationToClass(int classId);
        Task<ResponsDto<ApplicationDTO>> RejectApplication(int applicationId);
        Task<ResponsDto<ApplicationDTO>> CreateApplication(int traineeId, int classId);
    }
}
