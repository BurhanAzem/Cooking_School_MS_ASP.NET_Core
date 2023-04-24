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
using Cooking_School_ASP.NET.ModelUsed;
using Microsoft.AspNetCore.Hosting.Server;
using Cooking_School_ASP.NET.Services.FilesService;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET_.Models;

namespace Cooking_School_ASP.NET.Services.ChefService
{
    public class ChefService : IChefService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashPassword _hash;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ChefService(IUnitOfWork unitOfWork, IMapper mapper, IHashPassword hash, IWebHostEnvironment hostingEnvironment, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hash = hash;
            _hostingEnvironment = hostingEnvironment;
            _fileService = fileService;
        }

        public async Task<ResponsDto<ChefDTO>> AddMealToFovarite(int idMeal, User currentUser)
        {
            FavoriteMeal_chef favoritMeal = new FavoriteMeal_chef();
            favoritMeal.ChefId = currentUser.Id;
            favoritMeal.MealId = idMeal;
            await _unitOfWork.FavoriteMeal_Chefs.Insert(favoritMeal);
            await _unitOfWork.Save();
            return new ResponsDto<ChefDTO>
            {
            };
        }

        public async Task<ResponsDto<ChefDTO>> DeleteMealFromFovarite(int idMeal, User currentUser)
        {
            var meal = await _unitOfWork.FavoriteMeal_Chefs.Get(x => x.Id == idMeal);
            if (meal == null)
            {
                return new ResponsDto<ChefDTO>
                {
                    Exception = new Exception("Failed, Meal Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.FavoriteMeal_Chefs.Delete(idMeal);
            await _unitOfWork.Save();
            return new ResponsDto<ChefDTO>
            {
            };
        }

        public async Task<ResponsDto<ChefDTO>> DeleteUser(int chefId)
        {
            var chef = await _unitOfWork.Chefs.Get(x => x.Id == chefId);
            if (chef is null)
            {
                return new ResponsDto<ChefDTO>()
                {
                    Exception = new Exception("Failed, This User Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var fileName = chef.CvPath.Split('/')[0];

            var res = await _fileService.DeleteBlob(fileName);
            if (res.error == true)
            {
                return new ResponsDto<ChefDTO>()
                {
                    Exception = new Exception(res.Status),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.Users.Delete(chefId);
            await _unitOfWork.Save();
            return new ResponsDto<ChefDTO>();
        }

        public async Task<ResponsDto<ChefDTO>> FavoriteChef(int chefId, int traineeId)
        {
            if (await _unitOfWork.Users.Get(x => x.Id == chefId) is null)
            {
                return new ResponsDto<ChefDTO>()
                {
                    Exception = new Exception("Failed, chef Entered Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == traineeId) is null)
            {
                return new ResponsDto<ChefDTO>()
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
            return new ResponsDto<ChefDTO>()
            {
            };
        }
        //GetAll(x => x.Discriminator == Convert.ToString(Roles.Chef))

        public async Task<ResponsDto<FavoriteChefDto>> GetAllFavoriteChefs(RequestParam requestParams = null)
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
                    });
                }

                //var favoriteChefDto = _mapper.Map<IList<FavoriteChefDto>>(favoriteChefs);
                return new ResponsDto<FavoriteChefDto>
                {
                    ListDto = favoriteChefs
                };
            }
            var chefsPag = await _unitOfWork.Chefs.GetPagedList(requestParams);
            var favoriteChefDtoPag = _mapper.Map<IList<FavoriteChefDto>>(chefsPag);
            return new ResponsDto<FavoriteChefDto>
            {
                ListDto = favoriteChefDtoPag
            };

        }

        //[FromQuery] RequestParams requestParams
        public async Task<ResponsDto<ChefDTO>> GetAllUser(RequestParam requestParams = null)
        {

            if (requestParams == null)
            {
                var chefs = await _unitOfWork.Chefs.GetAll();
                var chefDto = _mapper.Map<IList<ChefDTO>>(chefs);
                return new ResponsDto<ChefDTO>
                {
                    ListDto = chefDto
                };
            }
            var chefsPag = await _unitOfWork.Chefs.GetPagedList(requestParams);
            var chefDtoPag = _mapper.Map<IList<ChefDTO>>(chefsPag);
            return new ResponsDto<ChefDTO>
            {
                ListDto = chefDtoPag
            };
        }



        public async Task<ResponsDto<ChefDTO>> GetUserById(int id)
        {
            var chef = await _unitOfWork.Chefs.Get(x => x.Id == id);
            if (chef == null)
            {
                return new ResponsDto<ChefDTO>
                {
                    Exception = new Exception($"Failed, this User with this {id} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var chefDto = _mapper.Map<ChefDTO>(chef);
            return new ResponsDto<ChefDTO>
            {
                Dto = chefDto,
            };
        }

        public async Task<ResponsDto<ChefDTO>> LogOut(string token)
        {
            if (await _unitOfWork.BlackLists.Get(l => l.Invalid_Token == token) is not null)
            {
                return new ResponsDto<ChefDTO> { };
            }
            await _unitOfWork.BlackLists.Insert(new BlackList() { Created = DateTime.Now, Invalid_Token = token });
            return new ResponsDto<ChefDTO> { };
        }

        public async Task<ResponsDto<ChefDTO>> RegisterUser(CreateChefDto createChefDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == createChefDto.Email) is not null)
            {
                return new ResponsDto<ChefDTO>
                {
                    Exception = new Exception($"Failed, Using Email Already Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var chef = _mapper.Map<Chef>(createChefDto);

            chef.Created = DateTime.Now;

            var res = await _fileService.UploadAsync(createChefDto.Cv);
            if (res.error == false)
            {
                return new ResponsDto<ChefDTO>()
                {
                    Exception = new Exception(res.Status),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
            chef.CvPath = res.Blob.Uri;

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
                return new ResponsDto<ChefDTO>
                {
                    Exception = new Exception($"Failed, Invaild Input Role"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            chef.Discriminator = Convert.ToString(role);

            await _unitOfWork.Users.Insert(chef);
            await _unitOfWork.Save();

            var chefDto = _mapper.Map<ChefDTO>(chef);
            return new ResponsDto<ChefDTO>
            {
                Dto = chefDto,
            };
        }

        public async Task<ResponsDto<ChefDTO>> UnFavoriteChef(int traineeId, int chefId)
        {
            var favoriteChef = await _unitOfWork.Favorite_Chefs.Get(x => x.Id == chefId && x.Id == traineeId);
            if (favoriteChef is not null)
            {
                return new ResponsDto<ChefDTO>()
                {
                    Exception = new Exception($"Failed, Course Favorite Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest

                };
            }
            await _unitOfWork.Favorite_Courses.Delete(favoriteChef.Id);
            await _unitOfWork.Save();
            return new ResponsDto<ChefDTO>()
            {
            };
        }

        public async Task<ResponsDto<ChefDTO>> UpdateUser(int id, UpdateChefDto UpdatechefDto)
        {
            Chef chef = (Chef)await _unitOfWork.Users.Get(x => x.Id == id);
            if (chef is null)
                return new ResponsDto<ChefDTO>
                {
                    Exception = new Exception($"Failed, User Is Not Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            if (UpdatechefDto.FirstName is not null) { chef.FirstName = UpdatechefDto.FirstName; }
            if (UpdatechefDto.LastName is not null) { chef.LastName = UpdatechefDto.LastName; }
            if (UpdatechefDto.BirthDate != null) { chef.BirthDate = (DateTime)UpdatechefDto.BirthDate; }
            if (UpdatechefDto.Password is not null)
            {
                _hash.createHashPassword(UpdatechefDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
                chef.PasswordHashed = hashedPass;
                chef.PasswordSlot = hashedSlot;
            }
            if (UpdatechefDto.Address is not null) { chef.Address = UpdatechefDto.Address; }
            if (UpdatechefDto.PhoneNumber != null) { chef.PhoneNumber = (int)UpdatechefDto.PhoneNumber; }
            if (UpdatechefDto.Cv is not null)
            {
                string fileName = UpdatechefDto.Cv.FileName;

                var resDelete = await _fileService.DeleteBlob(UpdatechefDto.Cv.FileName);
                if (resDelete.error == false)
                {
                    return new ResponsDto<ChefDTO>()
                    {
                        Exception = new Exception(resDelete.Status),
                    };
                }
                var res = await _fileService.UploadAsync(UpdatechefDto.Cv);

                if (res.error == false)
                {
                    return new ResponsDto<ChefDTO>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    };
                }

                chef.CvPath = res.Blob.Uri;
            }
            if (UpdatechefDto.Salary != 0) { chef.Salary = (int)UpdatechefDto.Salary; }

            chef.Updated = DateTime.Now;


            _unitOfWork.Users.Update(chef);
            await _unitOfWork.Save();
            var chafDto = _mapper.Map<ChefDTO>(chef);
            return new ResponsDto<ChefDTO>
            {
                Dto = chafDto,
            };
        }
    }
}
