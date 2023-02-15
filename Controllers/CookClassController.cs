using Cooking_School_ASP.NET.Controllers;
using Cooking_School_ASP.NET.Dtos.CookClassDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages;

namespace Cooking_School_ASP.NET_.Controllers
{
    [Route("api/cook-classes")]
    [ApiController]
    public class CookClassController : ControllerBase
    {
        private readonly ILogger<CookClassController> _logger;
        private readonly ICookClassService _cookClassService;
        public CookClassController(ILogger<CookClassController> logger, ICookClassService cookClassService)
        {
            _logger = logger;
            _cookClassService = cookClassService;
        }
    ///pi/chefs/{chefId}/cook-classes
    [HttpPost("~/api/chefs/{chefId}/cook-classes")]
    public async Task<IActionResult> CreateClass([FromBody] CreateCookClassDto classDto)
        {
            _logger.LogInformation($" Attempt Sinup for {classDto} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt for {nameof(classDto)}");
                return BadRequest(ModelState);
            }
            var result = await _cookClassService.CreateCookClass(classDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.CookClassDto);
        }

        [HttpPut("~/api/chefs/{chefId}/cook-classes/{classId}")]
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
            return Ok(result.CookClassDto);
        }

        [HttpDelete("~/api/chefs/{chefId}/cook-classes/{classId}")]
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
            return Ok(result.CookClassDto);
        }
    }





}
