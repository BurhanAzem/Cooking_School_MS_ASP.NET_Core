using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services
{
    public interface ICookClassService
    {
        public Task<ResponsDto<CookClassDTO>> CreateCookClass(CreateCookClassDto createCookClassDto);
        public Task<ResponsDto<CookClassDTO>> UpdateCookClass(int cookClassId , UpdateCookClassDto updateCookClassDto);
        public Task<ResponsDto<CookClassDTO>> DeleteCookClass(int cookClassId);
    }
}
