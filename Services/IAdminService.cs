using Cooking_School_ASP.NET.Dtos.AdminDto;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services
{
    public interface IAdminService
    {
        Task<ResponsAdminDto> RegisterUser(CreateAdminDto createAdminDto);
        Task<ResponsAdminDto> UpdateUser(int adminId, UpdateAdminDto updateAdminDto);
        Task<ResponsAdminDto> GetUserById(int adminId);
        Task<IList<Meal>> GetAllFavoriteMeals(IList<Meal> meals);
    }
}
