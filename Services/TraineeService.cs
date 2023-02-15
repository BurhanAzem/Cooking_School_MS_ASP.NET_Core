using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Repository;
using Cooking_School_ASP.NET.Hash;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Drawing;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.AdminDto;
using System.Data;

namespace Cooking_School_ASP.NET.Services
{
    public class TraineeService : ITraineeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashPassword _hash;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public TraineeService(IUnitOfWork unitOfWork, IMapper mapper, IHashPassword hash, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hash = hash;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<ResponsTraineeDto> AddMealToFovarite(int idMeal, User currentUser)
        {
            FavoriteMeal_Trainee favoritMeal = new FavoriteMeal_Trainee();
            favoritMeal.TraineeId = currentUser.Id;
            favoritMeal.MealId = idMeal;
            await _unitOfWork.FavoriteMeal_Trainees.Insert(favoritMeal);
            await _unitOfWork.Save();
            return new ResponsTraineeDto
            {
            };
        }

        public async Task<ResponsTraineeDto> DeleteMealFromFovarite(int idMeal)
        {
            var meal = await _unitOfWork.FavoriteMeal_Trainees.Get(x => x.Id == idMeal);
            if (meal == null)
            {
                return new ResponsTraineeDto
                {
                    Exception = new Exception($"Failed, Meal Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.FavoriteMeal_Trainees.Delete(idMeal);
            await _unitOfWork.Save();
            return new ResponsTraineeDto
            {
            };
        }

        public Task<ResponsTraineeDto> DeleteUser(int traineeId)
        {
            throw new NotImplementedException();
        }

        //[FromQuery] RequestParams requestParams
        public async Task<IList<TraineeDTO>> GetAllUsers(RequestParam requestParams = null)
        {
            if (requestParams == null)
            {
                var trainees = await _unitOfWork.Users.GetAll(x => x.Discriminator == Convert.ToString(Roles.Trainee));
                var traineeDto = _mapper.Map<IList<TraineeDTO>>(trainees);
                return traineeDto;
            }
            var traineesPag = await _unitOfWork.Users.GetPagedList(requestParams, x => x.Discriminator == Convert.ToString(Roles.Trainee) , include: x => x.Include(s => s.TraineeCourses));
            var traineeDtoPag = _mapper.Map<IList<TraineeDTO>>(traineesPag);
            return traineeDtoPag;
        }



        public async Task<ResponsTraineeDto> GetUserById(int id)
        {
            var trainee = await _unitOfWork.Users.Get(x => x.Id == id);
            if (trainee == null)
            {
                return new ResponsTraineeDto
                {
                    Exception = new Exception($"Failed, this User with {id} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var traineeDto = _mapper.Map<TraineeDTO>(trainee);
            return new ResponsTraineeDto
            {
                TraineeDto = traineeDto,
            };
        }

        public async Task<ResponsTraineeDto> LogOut(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            while (identity.Claims != null)
            {
                identity.RemoveClaim(identity.Claims.FirstOrDefault());
            }
            return new ResponsTraineeDto
            {
            };
        }

        public async Task<ResponsTraineeDto> RegisterUser(CreateTraineeDto createTraineeDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == createTraineeDto.Email) is not null)
            {
                return new ResponsTraineeDto
                {
                    Exception = new Exception($"Failed, Useing Email Is Already Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest

                };
            }

            var trainee = _mapper.Map<Trainee>(createTraineeDto);
            trainee.Created = DateTime.Now;

            

            string fileName = createTraineeDto.image.FileName;

            fileName = Path.GetFileName(fileName);

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            await createTraineeDto.image.CopyToAsync(stream);
            trainee.ImagePath = uploadpath;

            _hash.createHashPassword(createTraineeDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
            trainee.PasswordHashed = hashedPass;
            trainee.PasswordSlot = hashedSlot;
            Roles role;
            try
            {

                role = (Roles)Enum.Parse(typeof(Roles), createTraineeDto.Discriminator);
            }
            catch (Exception ex)
            {
                return new ResponsTraineeDto
                {
                    Exception = new Exception($"Failed, Invaild Input Role"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            trainee.Discriminator =  Convert.ToString(role);
            Levels level;
            try
            {
                level = (Levels)Enum.Parse(typeof(Levels), createTraineeDto.Level);
            }
            catch (Exception ex)
            {
                return new ResponsTraineeDto
                {
                    Exception = new Exception($"Failed, Invaild Input Role"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            trainee.Level = level;

            await _unitOfWork.Users.Insert(trainee);
            await _unitOfWork.Save();

            var traineeDto = _mapper.Map<TraineeDTO>(trainee);
            return new ResponsTraineeDto
            {
                TraineeDto = traineeDto,
            };
        }


        public async Task<ResponsTraineeDto> UpdateUser(int id, UpdateTraineeDto updateTraineeDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == updateTraineeDto.Email) is not null)
                return new ResponsTraineeDto
                {
                    Exception = new Exception("Failed, User Is Not Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
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

            var traineeDto = _mapper.Map<TraineeDTO>(trainee);
            return new ResponsTraineeDto
            {
                TraineeDto = traineeDto,
            };
        }
        
    }
}
