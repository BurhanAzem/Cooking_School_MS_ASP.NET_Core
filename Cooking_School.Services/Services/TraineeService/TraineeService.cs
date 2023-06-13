using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Cooking_School.Core.IRepository.IUnitOfWork;
using Cooking_School.Core.Hash;
using Cooking_School.Services.FilesService;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Cooking_School.Services.Dtos;
using Cooking_School.Services.Dtos.TraineeDto;
using Cooking_School.Services.Dtos.CookClassDto;

namespace Cooking_School.Services.TraineeService
{
    public class TraineeService : ITraineeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashPassword _hash;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public TraineeService(IUnitOfWork unitOfWork, IMapper mapper, IHashPassword hash, IWebHostEnvironment hostingEnvironment, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hash = hash;
            _hostingEnvironment = hostingEnvironment;
            _fileService = fileService;
        }

        public async Task<ResponsDto<TraineeDTO>> AddMealToFovarite(int idMeal, User currentUser)
        {
            FavoriteMeal_Trainee favoritMeal = new FavoriteMeal_Trainee();
            favoritMeal.TraineeId = currentUser.Id;
            favoritMeal.MealId = idMeal;
            await _unitOfWork.FavoriteMeal_Trainees.Insert(favoritMeal);
            await _unitOfWork.Save();
            return new ResponsDto<TraineeDTO>
            {
            };
        }

        public async Task<ResponsDto<TraineeDTO>> DeleteMealFromFovarite(int idMeal)
        {
            var meal = await _unitOfWork.FavoriteMeal_Trainees.Get(x => x.Id == idMeal);
            if (meal == null)
            {
                return new ResponsDto<TraineeDTO>
                {
                    Exception = new Exception($"Failed, Meal Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.FavoriteMeal_Trainees.Delete(idMeal);
            await _unitOfWork.Save();
            return new ResponsDto<TraineeDTO>
            {
            };
        }

        public async Task<ResponsDto<TraineeDTO>> DeleteUser(int traineeId)
        {
            var trainee = await _unitOfWork.Trainees.Get(x => x.Id == traineeId);
            if (trainee is null)
            {
                return new ResponsDto<TraineeDTO>()
                {
                    Exception = new Exception("Failed, This User Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var fileName = trainee.ImagePath.Split('/')[0];

            var res = await _fileService.DeleteBlob(fileName);
            if (res.error == true)
            {
                return new ResponsDto<TraineeDTO>()
                {
                    Exception = new Exception(res.Status),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.Trainees.Delete(traineeId);
            await _unitOfWork.Save();
            return new ResponsDto<TraineeDTO>();
        }

        public async Task<ResponsDto<CookClassDTO>> GetAllCookClassesForTrainee(int traineeId, RequestParam requestParam = null)
        {
            var cookclasses = await _unitOfWork.CookClasses.GetPagedList(requestParam, include: q => q.Include(x => ((CookClass)x).ClassDays));

            List<CookClass> cookClassesForTrainee = new List<CookClass>();
            foreach (var cookclass in cookclasses)
            {
                var course = await _unitOfWork.Courses.Get(x => x.Id == cookclass.CourseId);
                var trainee = await _unitOfWork.Trainees.Get(x => x.Id == traineeId);
                if (course.CourseLevel == trainee.Level)
                {
                    cookClassesForTrainee.Add(cookclass);
                }
            }
            var cookclassesDto = _mapper.Map<List<CookClassDTO>>(cookClassesForTrainee);
            int i = 0;
            foreach (var cookClass in cookclasses)
            {
                List<string> classDays = new List<string>();
                foreach (var day in cookClass.ClassDays)
                {
                    classDays.Add(day.Day.ToString());
                }
                cookclassesDto[i].ClassDays = classDays;
                i++;
            }
            return new ResponsDto<CookClassDTO>()
            {
                ListDto = cookclassesDto
            };
        }

        public async Task<ResponsDto<TraineeDTO>> GetAllUsers(RequestParam requestParams = null)
        {
            if (requestParams == null)
            {
                var trainees = await _unitOfWork.Trainees.GetAll(x => x.Discriminator == Convert.ToString(Roles.Trainee));
                var traineesDto = _mapper.Map<IList<TraineeDTO>>(trainees);
                return new ResponsDto<TraineeDTO>
                {
                    ListDto = traineesDto,
                };
            }
            var traineesPag = await _unitOfWork.Trainees.GetPagedList(requestParams, x => x.Discriminator == Convert.ToString(Roles.Trainee), include: x => x.Include(s => ((Trainee)s).TraineeCourses));
            var traineeDtoPag = _mapper.Map<IList<TraineeDTO>>(traineesPag);
            return new ResponsDto<TraineeDTO>
            {
                ListDto = traineeDtoPag,
            };
        }



        public async Task<ResponsDto<TraineeDTO>> GetUserById(int id)
        {
            var trainee = await _unitOfWork.Trainees.Get(x => x.Id == id);
            if (trainee == null)
            {
                return new ResponsDto<TraineeDTO>
                {
                    Exception = new Exception($"Failed, this User with {id} Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var traineeDto = _mapper.Map<TraineeDTO>(trainee);
            return new ResponsDto<TraineeDTO>
            {
                Dto = traineeDto,
            };
        }

        public async Task<ResponsDto<TraineeDTO>> LogOut(string token)
        {
            if (await _unitOfWork.BlackLists.Get(l => l.Invalid_Token == token) is not null)
            {
                return new ResponsDto<TraineeDTO> { };
            }
            await _unitOfWork.BlackLists.Insert(new BlackList() { Created = DateTime.Now, Invalid_Token = token });
            return new ResponsDto<TraineeDTO> { };
        }

        public async Task<ResponsDto<TraineeDTO>> RegisterUser(CreateTraineeDto createTraineeDto)
        {
            if (await _unitOfWork.Trainees.Get(c => c.Email == createTraineeDto.Email) is not null)
            {
                return new ResponsDto<TraineeDTO>
                {
                    Exception = new Exception($"Failed, Useing Email Is Already Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest

                };
            }

            var trainee = _mapper.Map<Trainee>(createTraineeDto);
            trainee.Created = DateTime.Now;



            string fileName = createTraineeDto.image.FileName;

            var res = await _fileService.UploadAsync(createTraineeDto.image);
            if (res.error == true)
            {
                return new ResponsDto<TraineeDTO>()
                {
                    Exception = new Exception(res.Status),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
            trainee.ImagePath = res.Blob.Uri;

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
                return new ResponsDto<TraineeDTO>
                {
                    Exception = new Exception($"Failed, Invaild Input Role"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            trainee.Discriminator = Convert.ToString(role);
            Levels level;
            try
            {
                level = (Levels)Enum.Parse(typeof(Levels), createTraineeDto.Level);
            }
            catch (Exception ex)
            {
                return new ResponsDto<TraineeDTO>
                {
                    Exception = new Exception($"Failed, Invaild Input Role"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            trainee.Level = level;

            await _unitOfWork.Trainees.Insert(trainee);
            await _unitOfWork.Save();

            var traineeDto = _mapper.Map<TraineeDTO>(trainee);
            return new ResponsDto<TraineeDTO>
            {
                Dto = traineeDto,
            };
        }


        public async Task<ResponsDto<TraineeDTO>> UpdateUser(int id, UpdateTraineeDto updateTraineeDto)
        {
            Trainee trainee = (Trainee)await _unitOfWork.Trainees.Get(c => c.Id == id);
            if (trainee is null)
                return new ResponsDto<TraineeDTO>
                {
                    Exception = new Exception("Failed, User Is Not Exists"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };


            if (updateTraineeDto.FirstName is not null) { trainee.FirstName = updateTraineeDto.FirstName; }
            if (updateTraineeDto.LastName is not null) { trainee.LastName = updateTraineeDto.LastName; }
            if (updateTraineeDto.BirthDate != null) { trainee.BirthDate = (DateTime)updateTraineeDto.BirthDate; }
            if (updateTraineeDto.Password is not null)
            {
                _hash.createHashPassword(updateTraineeDto.Password, out byte[] hashedPass, out byte[] hashedSlot);
                trainee.PasswordHashed = hashedPass;
                trainee.PasswordSlot = hashedSlot;
            }
            if (updateTraineeDto.Address is not null) { trainee.Address = updateTraineeDto.Address; }
            if (updateTraineeDto.PhoneNumber != null) { trainee.PhoneNumber = (int)updateTraineeDto.PhoneNumber; }
            if (updateTraineeDto.CardN != null) { trainee.CardN = (int)updateTraineeDto.CardN; }
            if (updateTraineeDto.image is not null)
            {
                string fileName = updateTraineeDto.image.FileName;

                var resDelete = await _fileService.DeleteBlob(updateTraineeDto.image.FileName);
                if (resDelete.error == false)
                {
                    return new ResponsDto<TraineeDTO>()
                    {
                        Exception = new Exception(resDelete.Status),
                    };
                }
                var res = await _fileService.UploadAsync(updateTraineeDto.image);

                if (res.error == false)
                {
                    return new ResponsDto<TraineeDTO>()
                    {
                        Exception = new Exception(res.Status),
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    };
                }

                trainee.ImagePath = res.Blob.Uri;
            }
            if (updateTraineeDto.Level is not null) { trainee.Level = (Levels)Enum.Parse(typeof(Levels), updateTraineeDto.Level); }

            trainee.Updated = DateTime.Now;

            trainee.Created = trainee.Created;
            trainee.Updated = DateTime.Now;


            _unitOfWork.Users.Update(trainee);
            await _unitOfWork.Save();

            var traineeDto = _mapper.Map<TraineeDTO>(trainee);
            return new ResponsDto<TraineeDTO>
            {
                Dto = traineeDto,
            };
        }
    }
}
