using Cooking_School_ASP.NET.Dtos.AdminDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services.AdminService
{
    public interface IAdminService
    {
        Task<ResponsDto<AdminDTO>> RegisterUser(CreateAdminDto createAdminDto);
        Task<ResponsDto<AdminDTO>> UpdateUser(int adminId, UpdateAdminDto updateAdminDto);
        Task<ResponsDto<AdminDTO>> LogOut(string token);
        Task<ResponsDto<AdminDTO>> GetUserById(int adminId);
        Task<IList<Meal>> GetAllFavoriteMeals(IList<Meal> meals);
    }
}
