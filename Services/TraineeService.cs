using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Repository;
using E_Commerce_System.Hash;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cooking_School_ASP.NET.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashPassword _hash;
        public TraineeService(IUnitOfWork unitOfWork, IMapper mapper, IHashPassword hash)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hash = hash;
        }

        public async Task<ResponsTraineeDto> AddMealToFovarite(int idMeal, User currentUser)
        {
            FavoriteMeal_Trainee favoritMeal = new FavoriteMeal_Trainee();
            favoritMeal.TraineeId = currentUser.Id;
            favoritMeal.MealId = idMeal;
            await _unitOfWork.FavoriteMeal_Trainees.Insert(favoritMeal);
            return new ResponsTraineeDto
            {
                Message = "Done"
            };
        }

        public async Task<ResponsTraineeDto> DeleteMealFromFovarite(int idMeal)
        {
            var meal = await _unitOfWork.FavoriteMeal_Trainees.Get(x => x.Id == idMeal);
            if (meal == null)
            {
                return new ResponsTraineeDto
                {
                    Message = "Failed, Meal Not Exist"
                };
            }
            await _unitOfWork.FavoriteMeal_Trainees.Delete(idMeal);
            return new ResponsTraineeDto
            {
                Message = "Done"
            };
        }

        //[FromQuery] RequestParams requestParams
        public async Task<IList<TraineeDto>> GetAllUser(RequestParam requestParams = null)
        {

            if (requestParams == null)
            {
                var trainees = await _unitOfWork.Users.GetAll(x => x.Discriminator == "Tarinee");
                var traineeDto = _mapper.Map<IList<TraineeDto>>(trainees);
                return traineeDto;
            }
            var traineesPag = await _unitOfWork.Users.GetPagedList(requestParams, x => x.Discriminator == "Tarinee", include: x => x.Include(s => s.TraineeCourses));
            var traineeDtoPag = _mapper.Map<IList<TraineeDto>>(traineesPag);
            return traineeDtoPag;
        }



        public async Task<ResponsTraineeDto> GetUserById(int id)
        {
            var trainee = await _unitOfWork.Users.Get(x => x.Id == id);
            if (trainee == null)
            {
                return new ResponsTraineeDto
                {
                    Message = $"Failed, this User with {id} Is Not Exist"
                };
            }

            var traineeDto = _mapper.Map<TraineeDto>(trainee);
            return new ResponsTraineeDto
            {
                TraineeDto = traineeDto,
                Message = "Done"
            };
        }

        public async Task<ResponsTraineeDto> LogOut(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            await httpContext.SignOutAsync();
            while (identity.Claims != null)
            {
                identity.RemoveClaim(identity.Claims.FirstOrDefault());
            }
            return new ResponsTraineeDto
            {
                Message = "Done"
            };
        }

        public async Task<ResponsTraineeDto> RegisterUser(CreateTraineeDto createTraineeDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == createTraineeDto.Email) is not null)
                return new ResponsTraineeDto
                {
                    Message = "Failed, User Is Already Exists",
                };
            var trainee = _mapper.Map<Trainee>(createTraineeDto);
            trainee.Created = DateTime.Now;
            _hash.createHashPassword(createTraineeDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
            trainee.PasswordHashed = hashedPass;
            trainee.PasswordSlot = hashedSlot;
            await _unitOfWork.Users.Insert(trainee);
            await _unitOfWork.Save();
            var traineeDto = _mapper.Map<TraineeDto>(trainee);
            return new ResponsTraineeDto
            {
                TraineeDto = traineeDto,
                Message = "Done",
            };
        }


        public async Task<ResponsTraineeDto> UpdateUser(int id, UpdateTraineeDto updateTraineeDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == updateTraineeDto.Email) is not null)
                return new ResponsTraineeDto
                {
                    Message = "Failed, User Is Not Exists",
                };

            var trainee = await _unitOfWork.Users.Get(x => x.Id == id);
            var updatedTrainee = _mapper.Map<User>(updateTraineeDto);

            updatedTrainee.Created = trainee.Created;
            updatedTrainee.Updated = DateTime.Now;
            _hash.createHashPassword(updateTraineeDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
            updatedTrainee.PasswordHashed = hashedPass;
            updatedTrainee.PasswordSlot = hashedSlot;

            _unitOfWork.Users.Update(updatedTrainee);
            await _unitOfWork.Save();
            var traineeDto = _mapper.Map<TraineeDto>(trainee);
            return new ResponsTraineeDto
            {
                TraineeDto = traineeDto,
                Message = "Done",
            };
        }
        
    }
}
