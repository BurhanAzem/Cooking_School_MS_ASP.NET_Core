using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;

namespace Cooking_School_ASP.NET.Services
{
    public class CookClassService : ICookClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CookClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponsDto<CookClassDTO>> CreateCookClass(CreateCookClassDto createCookClassDto)
        {
            if (await _unitOfWork.Courses.Get(x => x.Id == createCookClassDto.CourseId) is null)
            {
                return new ResponsDto<CookClassDTO>()
                {
                    Exception = new Exception($"Faild, Entered Course Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest

                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == createCookClassDto.ChefId) is null)
            {
                return new ResponsDto<CookClassDTO>()
                {
                    Exception = new Exception($"Faild, Entered Chef Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var cookClass = _mapper.Map<CookClass>(createCookClassDto);
            await _unitOfWork.CookClasses.Insert(cookClass);
            await _unitOfWork.Save();
            var classDaysDTO = createCookClassDto.ClassDays;
            ICollection<ClassDays> classDays = new List<ClassDays>();    
            foreach(var day in classDaysDTO)
            {
                WeekDays _day = (WeekDays)Enum.Parse(typeof(WeekDays), day.Day);
                classDays.Add(new ClassDays()
                {
                    Day = _day,
                    CookClassId = day.CookClassId
                });
            }
            await _unitOfWork.ClassDays.InsertRange(classDays);
            return new ResponsDto<CookClassDTO>()
            {
            };
        }

        public async Task<ResponsDto<CookClassDTO>> DeleteCookClass(int cookClassId)
        {
            if (await _unitOfWork.CookClasses.Get(x => x.Id ==cookClassId) is null)
            {
                return new ResponsDto<CookClassDTO>()
                {
                    Exception = new Exception($"Failed, This CookClass Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.CookClasses.Delete(cookClassId);
            await _unitOfWork.Save();
            return new ResponsDto<CookClassDTO>()
            {
            };
        }

        public async Task<ResponsDto<CookClassDTO>> UpdateCookClass(int cookClassId, UpdateCookClassDto updateCookClassDto)
        {
            var cookClass = await _unitOfWork.CookClasses.Get(x => x.Id == cookClassId);
            if (cookClass is null)
            {
                return new ResponsDto<CookClassDTO>()
                {
                    Exception = new Exception($"Failed, This CookClass Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            if (updateCookClassDto.ChefId != 0) { cookClass.ChefId = (int)updateCookClassDto.ChefId; }
            if (updateCookClassDto.CourseId != 0) { cookClass.CourseId = (int)updateCookClassDto.CourseId; }
            if (updateCookClassDto.EndingAt != null) { cookClass.EndingAt = (DateTime)updateCookClassDto.EndingAt; }
            if (updateCookClassDto.StartingAt != null) { cookClass.StartingAt = (DateTime)updateCookClassDto.StartingAt; }


            cookClass.Updated = DateTime.Now;
            _unitOfWork.CookClasses.Update(cookClass);
            await _unitOfWork.Save();
            var cookClassDTO = _mapper.Map<CookClassDTO>(cookClass);
            return new ResponsDto<CookClassDTO>()
            {
                Dto = cookClassDTO
            };
        }
    }
}
