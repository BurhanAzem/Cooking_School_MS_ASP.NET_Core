using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;

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
        public async Task<ResponsCookClass> CreateCookClass(CreateCookClassDto createCookClassDto)
        {
            if (await _unitOfWork.Courses.Get(x => x.Id == createCookClassDto.CourseId) is null)
            {
                return new ResponsCookClass()
                {
                    Exception = new Exception($"Faild, Entered Course Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest

                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == createCookClassDto.ChefId) is null)
            {
                return new ResponsCookClass()
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
            return new ResponsCookClass()
            {
            };
        }

        public async Task<ResponsCookClass> DeleteCookClass(int cookClassId)
        {
            if (await _unitOfWork.CookClasses.Get(x => x.Id ==cookClassId) is null)
            {
                return new ResponsCookClass()
                {
                    Exception = new Exception($"Failed, This CookClass Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _unitOfWork.CookClasses.Delete(cookClassId);
            await _unitOfWork.Save();
            return new ResponsCookClass()
            {
            };
        }

        public async Task<ResponsCookClass> UpdateCookClass(int cookClassId, UpdateCookClassDto updateCookClassDto)
        {
            if (await _unitOfWork.CookClasses.Get(x => x.Id == cookClassId) is null)
            {
                return new ResponsCookClass()
                {
                    Exception = new Exception($"Failed, This CookClass Is Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var updatedCookClass = _mapper.Map<CookClass>(updateCookClassDto);
            updatedCookClass.Updated = DateTime.Now;
            _unitOfWork.CookClasses.Update(updatedCookClass);
            await _unitOfWork.Save();
            return new ResponsCookClass()
            {
            };
        }
    }
}
