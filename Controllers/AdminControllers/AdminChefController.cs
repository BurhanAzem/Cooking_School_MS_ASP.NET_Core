using Cooking_School_ASP.NET.Dtos;
using Cooking_School_ASP.NET.Dtos.ChefDto;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services.AdminService;
using Cooking_School_ASP.NET.Services.ChefService;
using Cooking_School_ASP.NET_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cooking_School_ASP.NET.Controllers.AdminControllers
{
    [Route("api/admin/chefs")]
    [ApiController]
    public class AdminChefController : ControllerBase
    {
        private readonly ILogger<AdminCookClassController> _logger;
        private readonly IAdminService _adminService;
        private readonly IChefService _chefService;
        public AdminChefController(IAdminService adminService, IChefService chefService, ILogger<AdminCookClassController> logger)
        {
            _adminService = adminService;
            _chefService = chefService;
            _logger = logger;
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RegisterChef([FromForm] CreateChefDto createChefDto)
        {
            if (createChefDto.Cv == null || createChefDto.Cv.Length == 0)
                return Content("file not selected");

            _logger.LogInformation($" Attempt Sinup for {nameof(createChefDto)} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(createChefDto)}");
                return BadRequest(ModelState);
            }
            var result = await _chefService.RegisterUser(createChefDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        [HttpPut("{chefId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateChef(int chefId, [FromForm] UpdateChefDto updateChefDto)
        {
            _logger.LogInformation($" Attempt Update for {nameof(updateChefDto)} ");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Update attempt in {nameof(updateChefDto)}");
                return BadRequest(ModelState);
            }
            var result = await _chefService.UpdateUser(chefId, updateChefDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.Dto);
        }

        [HttpDelete("{chefId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteChef(int chefId)
        {
            _logger.LogInformation($"Attempt To Delete {nameof(Chef)}");
            if (chefId < 0)
            {
                _logger.LogInformation($"Invalid Attempt To Delete {nameof(Chef)}");
                return BadRequest();
            }
            var result = await _chefService.DeleteUser(chefId);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok("Done");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllChefs([FromQuery] RequestParam requestParams)
        {
            var result = await _chefService.GetAllUser(requestParams);
            return Ok(result.ListDto);
        }

        [HttpGet("favorite")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllFavoriteChef()
        {
            var result = await _chefService.GetAllFavoriteChefs();
            return Ok(result.ListDto);
        }
    }
}
