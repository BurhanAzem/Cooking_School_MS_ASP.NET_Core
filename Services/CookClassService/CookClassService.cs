using AutoMapper;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.Services.CookClassService
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
        public async Task<ResponsDto<CookClassDTO>> CreateCookClass(CreateCookClassDto createCookClassDto, int chefId)
        {
            if (await _unitOfWork.Courses.Get(x => x.Id == createCookClassDto.CourseId) is null)
            {
                return new ResponsDto<CookClassDTO>()
                {
                    Exception = new Exception($"Faild, Entered Course Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest

                };
            }
            if (await _unitOfWork.Users.Get(x => x.Id == chefId) is null)
            {
                return new ResponsDto<CookClassDTO>()
                {
                    Exception = new Exception($"Faild, Entered Chef Not Exist"),
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var cookClass = _mapper.Map<CookClass>(createCookClassDto);
            cookClass.ChefId = chefId;
            await _unitOfWork.CookClasses.Insert(cookClass);
            await _unitOfWork.Save();
            ICollection<ClassDays> classDays = new List<ClassDays>();
            foreach (var day in createCookClassDto.ClassDays)
            {
                WeekDays _day = (WeekDays)Enum.Parse(typeof(WeekDays), day);
                classDays.Add(new ClassDays()
                {
                    Day = _day,
                    CookClassId = cookClass.Id
                });
            }

            await _unitOfWork.ClassDays.InsertRange(classDays);
            await _unitOfWork.Save();
            return new ResponsDto<CookClassDTO>()
            {
            };

        }


        public async Task<ResponsDto<CookClassDTO>> DeleteCookClass(int cookClassId)
        {
            if (await _unitOfWork.CookClasses.Get(x => x.Id == cookClassId) is null)
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

        public async Task<ResponsDto<CookClassDTO>> GetAllCookClasses(RequestParam requestParam)
        {
            var cookclasses = await _unitOfWork.CookClasses.GetPagedList(requestParam, include: q => q.Include(x => ((CookClass)x).ClassDays));
            var cookclassesDto = _mapper.Map<List<CookClassDTO>>(cookclasses);
            int i = 0;
            //foreach (var cookClass in cookclasses)
            //{
            //    List<string> classDays = new List<string>();
            //    foreach(var day in cookClass.ClassDays)
            //    {
            //        classDays.Add(day.Day.ToString());
            //    }
            //    cookclassesDto[i].ClassDays = classDays;
            //    i++;
            //}
            return new ResponsDto<CookClassDTO>()
            {
                ListDto = cookclassesDto
            };
        }

        public async Task<ResponsDto<CookClassDTO>> GetAllCookClassesForChef(int chefId, RequestParam requestParam)
        {
            var cookclasses = await _unitOfWork.CookClasses.GetPagedList(requestParam, x => x.ChefId == chefId, include: q => q.Include(x => ((CookClass)x).ClassDays));
            var cookclassesDto = _mapper.Map<List<CookClassDTO>>(cookclasses);
            int i = 0;
            //foreach (var cookClass in cookclasses)
            //{
            //    List<string> classDays = new List<string>();
            //    foreach (var day in cookClass.ClassDays)
            //    {
            //        classDays.Add(day.Day.ToString());
            //    }
            //    ClassDaysDTO classDaysDto = new ClassDaysDTO();
            //    classDaysDto.Days = classDays;
            //    cookclassesDto[i].ClassDays = classDaysDto;
            //    i++;
            //}
            return new ResponsDto<CookClassDTO>()
            {
                ListDto = cookclassesDto
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
            if (updateCookClassDto.EndingAt != null) { cookClass.EndingAt = (TimeOnly)updateCookClassDto.EndingAt; }
            if (updateCookClassDto.StartingAt != null) { cookClass.StartingAt = (TimeOnly)updateCookClassDto.StartingAt; }
            if(updateCookClassDto.ClassDays != null)
            {
                var classDays = await _unitOfWork.ClassDays.GetAll(x => x.Id == cookClassId); 
                _unitOfWork.ClassDays.DeleteRange(classDays);
                List<ClassDays> classDaysUpdated = new List<ClassDays>();
                foreach (var day in updateCookClassDto.ClassDays)
                {
                    WeekDays _day = (WeekDays)Enum.Parse(typeof(WeekDays), day);
                    classDaysUpdated.Add(new ClassDays()
                    {
                        Day = _day,
                        CookClassId = cookClass.Id
                    });
                }

                await _unitOfWork.ClassDays.InsertRange(classDaysUpdated);
                await _unitOfWork.Save();
            }
            

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
