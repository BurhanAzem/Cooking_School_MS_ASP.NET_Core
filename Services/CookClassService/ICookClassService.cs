using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos;


namespace Cooking_School.Services.CookClassService
{
    public interface ICookClassService
    {
        public Task<ResponsDto<CookClassDTO>> CreateCookClass(CreateCookClassDto createCookClassDto, int chefId);
        public Task<ResponsDto<CookClassDTO>> UpdateCookClass(int cookClassId, UpdateCookClassDto updateCookClassDto);
        public Task<ResponsDto<CookClassDTO>> DeleteCookClass(int cookClassId);
        public Task<ResponsDto<CookClassDTO>> GetAllCookClasses(RequestParam requestParam);
        public Task<ResponsDto<CookClassDTO>> GetAllCookClassesForChef(int chefId, RequestParam requestParam);
    }
}
