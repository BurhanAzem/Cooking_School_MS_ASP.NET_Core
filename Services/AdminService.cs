using AutoMapper;
using Cooking_School_ASP.NET.Dtos.AdminDto;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Hash;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashPassword _hash;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper, IHashPassword hash, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hash = hash;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IList<Meal>> GetAllFavoriteMeals(IList<Meal> meals)
        {
            var favoriteMeal_Chef = await _unitOfWork.FavoriteMeal_Chefs.GetAll();
            var favoriteMeal_Trainee = await _unitOfWork.FavoriteMeal_Trainees.GetAll();
            IList<Meal> FavoriteMeal = new List<Meal>();
            foreach(var f in favoriteMeal_Chef)
            {
                FavoriteMeal.Add(meals.FirstOrDefault(x => x.idMeal == f.Id));
            }
            foreach(var f in favoriteMeal_Trainee)
            {
                FavoriteMeal.Add(meals.FirstOrDefault(x => x.idMeal == f.Id));
            }
            return FavoriteMeal;
        }

        public async Task<ResponsAdminDto> GetUserById(int adminId)
        {
            var admin = await _unitOfWork.Users.Get(x => x.Id == adminId);
            if (admin == null)
            {
                return new ResponsAdminDto
                {
                    Exception = new Exception($"Failed, this User with this {adminId} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var adminDto = _mapper.Map<AdminDTO>(admin);
            return new ResponsAdminDto
            {
                AdminDTO = adminDto,
            };
        }

        public async Task<ResponsAdminDto> RegisterUser(CreateAdminDto createAdminDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == createAdminDto.Email) is not null)
            {
                return new ResponsAdminDto
                {
                    Exception = new Exception($"Failed, Using Email Already Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var admin = _mapper.Map<Admin>(createAdminDto);

            admin.Created = DateTime.Now;

            _hash.createHashPassword(createAdminDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
            admin.PasswordHashed = hashedPass;
            admin.PasswordSlot = hashedSlot;
            Roles role;
            try
            {

                role = (Roles)Enum.Parse(typeof(Roles), createAdminDto.Discriminator);
            }
            catch (Exception ex)
            {
                return new ResponsAdminDto
                {
                    Exception = new Exception($"Failed, Invaild Input Role"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            admin.Discriminator = Convert.ToString(role);

            await _unitOfWork.Users.Insert(admin);
            await _unitOfWork.Save();

            var adminDto = _mapper.Map<AdminDTO>(admin);
            return new ResponsAdminDto
            {
                AdminDTO = adminDto,
            };
        }

        public async Task<ResponsAdminDto> UpdateUser(int adminId, UpdateAdminDto updateAdminDto)
        {
            if (await _unitOfWork.Users.Get(c => c.Email == updateAdminDto.Email) is not null)
                return new ResponsAdminDto
                {
                    Exception = new Exception($"Failed, User Is Not Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

            var admin = await _unitOfWork.Users.Get(x => x.Id == adminId);
            var updatedChef = _mapper.Map<User>(updateAdminDto);

            updatedChef.Created = admin.Created;
            updatedChef.Updated = DateTime.Now;
            _hash.createHashPassword(updateAdminDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
            updatedChef.PasswordHashed = hashedPass;
            updatedChef.PasswordSlot = hashedSlot;

            _unitOfWork.Users.Update(updatedChef);
            await _unitOfWork.Save();
            var adminDto = _mapper.Map<AdminDTO>(admin);
            return new ResponsAdminDto
            {
                AdminDTO = adminDto,
            };
        }
    }
}
