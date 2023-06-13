using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.Dtos;
using Cooking_School.Services.Dtos.AdminDto;

namespace Cooking_School.Services.AdminService
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
