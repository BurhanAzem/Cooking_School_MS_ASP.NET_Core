using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using E_Commerce_System.Hash;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Cooking_School_ASP.NET.Services
{
    public class ChefService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashPassword _hash;
        public ChefService(IUnitOfWork unitOfWork, IMapper mapper, IHashPassword hash)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hash = hash;
        }

        public async Task<ResponsChefDto> AddMealToFovarite(int idMeal, User currentUser)
        {
            FavoriteMeal_chef favoritMeal = new FavoriteMeal_chef();
            favoritMeal.ChefId = currentUser.Id;
            favoritMeal.MealId = idMeal;
            await _unitOfWork.FavoriteMeal_Chefs.Insert(favoritMeal);
            return new ResponsChefDto
            {
                Message = "Done"
            };
        }

        public async Task<ResponsChefDto> DeleteMealFromFovarite(int idMeal)
        {
            var meal = await _unitOfWork.FavoriteMeal_Chefs.Get(x => x.Id == idMeal);
            if (meal == null)
            {
                return new ResponsChefDto
                {
                    Message = "Failed, Meal Not Exist"
                };
            }
            await _unitOfWork.FavoriteMeal_Chefs.Delete(idMeal);
            return new ResponsChefDto
            {
                Message = "Done"
            };
        }

        //[FromQuery] RequestParams requestParams
        public async Task<IList<ChefDto>> GetAllUser(RequestParam requestParams = null)
        {

            if (requestParams == null)
            {
                var chefs = await _unitOfWork.Users.GetAll(x => x.Discriminator == "Chef");
                var chefDto = _mapper.Map<IList<ChefDto>>(chefs);
                return chefDto;
            }
            var chefsPag = await _unitOfWork.Users.GetPagedList(requestParams, x => x.Discriminator == "Chef");
            var chefDtoPag = _mapper.Map<IList<ChefDto>>(chefsPag);
            return chefDtoPag;
        }



        public async Task<ResponsChefDto> GetUserById(int id)
        {
            var chef = await _unitOfWork.Users.Get(x => x.Id == id);
            if (chef == null)
            {
                return new ResponsChefDto
                {
                    Message = $"Failed, this User with {id} Is Not Exist"
                };
            }

            var chefDto = _mapper.Map<ChefDto>(chef);
            return new ResponsChefDto
            {
                ChefDto = chefDto,
                Message = "Done"
            };
        }

        public async Task<ResponsChefDto> LogOut(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            await httpContext.SignOutAsync();
            while (identity.Claims != null)
            {
                identity.RemoveClaim(identity.Claims.FirstOrDefault());
            }
            return new ResponsChefDto
            {
                Message = "Done"
            };
        }

        public async Task<ResponsChefDto> RegisterUser(CreateChefDto createChefDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == createChefDto.Email) is not null)
                return new ResponsChefDto
                {
                    Message = "Failed, User Is Already Exists",
                };
            var chef = _mapper.Map<Chef>(createChefDto);
            chef.Created = DateTime.Now;
            _hash.createHashPassword(createChefDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
            chef.PasswordHashed = hashedPass;
            chef.PasswordSlot = hashedSlot;
            await _unitOfWork.Users.Insert(chef);
            await _unitOfWork.Save();
            var chefDto = _mapper.Map<ChefDto>(chef);

            return new ResponsChefDto
            {
                ChefDto = chefDto,
                Message = "Done",
            };
        }


        public async Task<ResponsChefDto> UpdateUser(int id, UpdateChefDto UpdatechefDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == UpdatechefDto.Email) is not null)
                return new ResponsChefDto
                {
                    Message = "Failed, User Is Not Exists",
                };

            var trainee = await _unitOfWork.Users.Get(x => x.Id == id);
            var updatedChef = _mapper.Map<User>(UpdatechefDto);

            updatedChef.Created = trainee.Created;
            updatedChef.Updated = DateTime.Now;
            _hash.createHashPassword(UpdatechefDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
            updatedChef.PasswordHashed = hashedPass;
            updatedChef.PasswordSlot = hashedSlot;

            _unitOfWork.Users.Update(updatedChef);
            await _unitOfWork.Save();
            var chafDto = _mapper.Map<ChefDto>(trainee);
            return new ResponsChefDto
            {
                ChefDto = chafDto,
                Message = "Done",
            };
        }
    }
}
