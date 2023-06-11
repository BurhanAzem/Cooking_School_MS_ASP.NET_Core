using Cooking_School.Core.ModelUsed;
using Cooking_School.Dtos.CookClassDto;
using Cooking_School.Services.AuthenticationServices;
using Cooking_School.Services.CookClassService;
using Cooking_School.Services.TraineeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages;

namespace Cooking_School.Controllers
{
    [Route("api/cook-classes")]
    [ApiController]
    public class CookClassController : ControllerBase
    {
        private readonly ILogger<CookClassController> _logger;
        private readonly ICookClassService _cookClassService;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly ITraineeService _traineeService;
        public CookClassController(ILogger<CookClassController> logger, ICookClassService cookClassService, IAuthenticationServices authenticationServices, ITraineeService traineeService)
        {
            _logger = logger;
            _cookClassService = cookClassService;
            _authenticationServices = authenticationServices;
            _traineeService = traineeService;
        }
        ///pi/chefs/{chefId}/cook-classes
        [HttpPost("")]
        [Authorize(Roles = "Administrator, Chef")]
        public async Task<IActionResult> CreateClass([FromBody] CreateCookClassDto classDto)
        {
            var user = await _authenticationServices.GetCurrentUser(HttpContext);
            _logger.LogInformation($" Attempt Sinup for {classDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt for {nameof(classDto)}");
                return BadRequest(ModelState);
            }
            var result = await _cookClassService.CreateCookClass(classDto, user.Id);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        [HttpPut("{classId}")]
        [Authorize(Roles = "Administrator, Chef")]
        public async Task<IActionResult> UpdateClass(int classId, [FromBody] UpdateCookClassDto classDto)
        {
            _logger.LogInformation($" Attempt Update for {classDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Update attempt for {nameof(classDto)}");
                return BadRequest(ModelState);
            }
            var result = await _cookClassService.UpdateCookClass(classId, classDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }


        [HttpGet("~/api/trainees/{traineeId}/cook-classes")]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> GetAllCookClassesForTrainee([FromQuery] RequestParam requestParam = null)
        {
            var trainee = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _traineeService.GetAllCookClassesForTrainee(trainee.Id, requestParam);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ListDto);
        }



        [HttpDelete("{classId}")]
        [Authorize(Roles = "Administrator, Chef")]
        public async Task<IActionResult> DleteClass(int classId)
        {
            _logger.LogInformation($" Attempt Delete CookClass {classId} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Delete CookClass {classId}");
                return BadRequest(ModelState);
            }
            var result = await _cookClassService.DeleteCookClass(classId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        [HttpGet("")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> GetAllCookClasses([FromQuery]RequestParam requestParam)
        {
            var chef = await _authenticationServices.GetCurrentUser(HttpContext);
            var result = await _cookClassService.GetAllCookClassesForChef(chef.Id, requestParam);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.ListDto);
        }
    }





}
