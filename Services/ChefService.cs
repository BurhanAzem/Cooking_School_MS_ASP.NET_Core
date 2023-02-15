using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Hash;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Cooking_School_ASP.NET.Dtos.CourseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Cooking_School_ASP.NET.Dtos.ApplicationDto;
using Cooking_School_ASP.NET.Dtos.ProjectFileDto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Cooking_School_ASP.NET.Dtos.AdminDto;

namespace Cooking_School_ASP.NET.Services
{
    public class ChefService : IChefService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashPassword _hash;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ChefService(IUnitOfWork unitOfWork, IMapper mapper, IHashPassword hash, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hash = hash;
            _hostingEnvironment = hostingEnvironment;   
        }

        public async Task<ResponsChefDto> AddMealToFovarite(int idMeal, User currentUser)
        {
            FavoriteMeal_chef favoritMeal = new FavoriteMeal_chef();
            favoritMeal.ChefId = currentUser.Id;
            favoritMeal.MealId = idMeal;
            await _unitOfWork.FavoriteMeal_Chefs.Insert(favoritMeal);
            await _unitOfWork.Save();
            return new ResponsChefDto
            {
            };
        }

        public async Task<ResponsChefDto> DeleteMealFromFovarite(int idMeal, User currentUser)
        {
            var meal = await _unitOfWork.FavoriteMeal_Chefs.Get(x => x.Id == idMeal);
            if (meal == null)
            {
                return new ResponsChefDto
                {
                    Exception = new Exception("Failed, Meal Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.FavoriteMeal_Chefs.Delete(idMeal);
            await _unitOfWork.Save();
            return new ResponsChefDto
            {
            };
        }

        public async Task<ResponsChefDto> DeleteUser(int chefId)
        {
            if (await _unitOfWork.Users.Get(x => x.Id == chefId) is null)
            {
                return new ResponsChefDto()
                {
                    Exception = new Exception("Failed, This User Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.Users.Delete(chefId);
            await _unitOfWork.Save();
            return new ResponsChefDto();
        }

        public async Task<ResponsChefDto> FavoriteChef(int chefId, int traineeId)
        {
            if (await _unitOfWork.Users.Get(x => x.Id == chefId) is null)
            {
                return new ResponsChefDto()
                {
                    Exception = new Exception("Failed, chef Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == traineeId) is null)
            {
                return new ResponsChefDto()
                {
                    Exception = new Exception("Failed, trainee Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            Favorite_Chef favoriteChef = new Favorite_Chef();
            favoriteChef.ChefId = chefId;
            favoriteChef.TraineeId = traineeId;
            favoriteChef.Created = DateTime.Now;
            await _unitOfWork.Favorite_Chefs.Insert(favoriteChef);
            await _unitOfWork.Save();
            return new ResponsChefDto()
            {
            };
        }
        //GetAll(x => x.Discriminator == Convert.ToString(Roles.Chef))

        public async Task<IList<FavoriteChefDto>> GetAllFavoriteChefs(RequestParam requestParams = null)
        {
            if (requestParams == null)
            {
                var favorite_Chefs = await _unitOfWork.Favorite_Chefs.GetAll();
                var chefs = await _unitOfWork.Users.GetAll(x => x.Discriminator == "Chef");
                var sortedFavorite_Chefs = from favoriteChef in favorite_Chefs
                                           group favoriteChef by favoriteChef.ChefId into g
                                           orderby g.Count()
                                           select new 
                                           {
                                               ChefId = g.Key,
                                               FavoriteNumber = g.Count()
                                           };
                IList<FavoriteChefDto> favoriteChefs = new List<FavoriteChefDto>();
                foreach (var chef in sortedFavorite_Chefs)
                {
                    favoriteChefs.Add(new FavoriteChefDto
                    {
                        FullName = chefs.FirstOrDefault(x => x.Id == chef.ChefId).FullName,
                        FavoriteNumber = chef.FavoriteNumber
                    }) ;
                }

                //var favoriteChefDto = _mapper.Map<IList<FavoriteChefDto>>(favoriteChefs);
                return favoriteChefs;
            }
            var chefsPag = await _unitOfWork.Users.GetPagedList(requestParams, x => x.Discriminator == Convert.ToString(Roles.Chef));
            var favoriteChefDtoPag = _mapper.Map<IList<FavoriteChefDto>>(chefsPag);
            return favoriteChefDtoPag;
        }

        //[FromQuery] RequestParams requestParams
        public async Task<IList<ChefDTO>> GetAllUser(RequestParam requestParams = null)
        {

            if (requestParams == null)
            {
                var chefs = await _unitOfWork.Users.GetAll(x => x.Discriminator == Convert.ToString(Roles.Chef));
                var chefDto = _mapper.Map<IList<ChefDTO>>(chefs);
                return chefDto;
            }
            var chefsPag = await _unitOfWork.Users.GetPagedList(requestParams, x => x.Discriminator == Convert.ToString(Roles.Chef));
            var chefDtoPag = _mapper.Map<IList<ChefDTO>>(chefsPag);
            return chefDtoPag;
        }



        public async Task<ResponsChefDto> GetUserById(int id)
        {
            var chef = await _unitOfWork.Users.Get(x => x.Id == id);
            if (chef == null)
            {
                return new ResponsChefDto
                {
                    Exception = new Exception($"Failed, this User with this {id} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var chefDto = _mapper.Map<ChefDTO>(chef);
            return new ResponsChefDto
            {
                ChefDto = chefDto,
            };
        }

        public async Task<ResponsChefDto> LogOut(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            while (identity.Claims != null)
            {
                identity.TryRemoveClaim(identity.Claims.FirstOrDefault());
            }
            return new ResponsChefDto
            {
            };
        }

        public async Task<ResponsChefDto> RegisterUser(CreateChefDto createChefDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == createChefDto.Email) is not null) 
            {
                return new ResponsChefDto
                {
                    Exception = new Exception($"Failed, Using Email Already Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var chef = _mapper.Map<Chef>(createChefDto);

            chef.Created = DateTime.Now;

            string fileName = createChefDto.Cv.FileName;

            fileName = Path.GetFileName(fileName);

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cv", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            await createChefDto.Cv.CopyToAsync(stream);

            chef.CvPath = uploadpath;

            _hash.createHashPassword(createChefDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
            chef.PasswordHashed = hashedPass;
            chef.PasswordSlot = hashedSlot;
            Roles role;
            try
            {
                role = (Roles)Enum.Parse(typeof(Roles), createChefDto.Discriminator);
            }
            catch (Exception ex)
            {
                return new ResponsChefDto
                {
                    Exception = new Exception($"Failed, Invaild Input Role"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            chef.Discriminator = Convert.ToString(role);

            await _unitOfWork.Users.Insert(chef);
            await _unitOfWork.Save();

            var chefDto = _mapper.Map<ChefDTO>(chef);
            return new ResponsChefDto
            {
                ChefDto = chefDto,
            };
        }

        public async Task<ResponsChefDto> UnFavoriteChef(int traineeId, int chefId)
        {
            var favoriteChef = await _unitOfWork.Favorite_Chefs.Get(x => x.Id == chefId && x.Id == traineeId);
            if (favoriteChef is not null)
            {
                return new ResponsChefDto()
                {
                    Exception = new Exception($"Failed, Course Favorite Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest

                };
            }
            await _unitOfWork.Favorite_Courses.Delete(favoriteChef.Id);
            await _unitOfWork.Save();
            return new ResponsChefDto()
            {
            };
        }

        public async Task<ResponsChefDto> UpdateUser(int id, UpdateChefDto UpdatechefDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == UpdatechefDto.Email) is not null)
                return new ResponsChefDto
                {
                    Exception = new Exception($"Failed, User Is Not Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
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
            var chafDto = _mapper.Map<ChefDTO>(trainee);
            return new ResponsChefDto
            {
                ChefDto = chafDto,
            };
        }
    }
}
