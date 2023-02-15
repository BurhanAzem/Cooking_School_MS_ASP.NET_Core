using Cooking_School_ASP.NET.Dtos.CookClassDto;

namespace Cooking_School_ASP.NET.Services
{
    public interface ICookClassService
    {
        public Task<ResponsCookClass> CreateCookClass(CreateCookClassDto createCookClassDto);
        public Task<ResponsCookClass> UpdateCookClass(int cookClassId , UpdateCookClassDto updateCookClassDto);
        public Task<ResponsCookClass> DeleteCookClass(int cookClassId);
    }
}
