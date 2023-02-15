using AutoMapper;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET.Dtos.AdminDto;
using Cooking_School_ASP.NET.Dtos.TraineeDto;
using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using RestSharp;
using Newtonsoft.Json;

namespace Cooking_School_ASP.NET.Controllers.AdminControllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminCookClassController> _logger;
        private readonly IAdminService _adminService;
        private readonly IAuthentication _authentication;
        private readonly IMapper _mapper;
        public AdminController(ILogger<AdminCookClassController> logger, IAdminService adminService, IAuthentication authentication, IMapper mapper)
        {
            _logger = logger;
            _adminService = adminService;
            _authentication = authentication;
            _mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CreateAdminDto createAdminDto)
        {

            _logger.LogInformation($" Attempt Sinup for {createAdminDto}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(createAdminDto)}");
                return BadRequest(ModelState);
            }
            var result = await _adminService.RegisterUser(createAdminDto);
            if (result.Exception is not null)
            {
                var code = result.StatusCode;
                throw new StatusCodeException(code.Value, result.Exception);
            }
            return Ok(result.AdminDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto adminLoginDto)
        {
            _logger.LogInformation($"Login Attempt for {nameof(adminLoginDto)} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (await _authentication.IsAuthenticate(adminLoginDto))
            {
                string token = await _authentication.GenerateToken();
                var refreshToken = await _authentication.GenerateRefreshToken();
                var user = await _adminService.GetUserById(adminLoginDto.Id);
                var trainee = _mapper.Map<Admin>(user.AdminDTO);
                SetRefreshToken(refreshToken, trainee);
                return Accepted(new TokenRequest { Token = token, RefreshToken = refreshToken.Token });
            }
            return null;
        }

        [HttpGet("favorite-meal")]
        public async Task<IActionResult> GetAllFavoriteMeals()
        {
            _logger.LogInformation($"Attempt GetAllFavoriteMeals  of Meal");
            using var client = new HttpClient();
            var response = await client.GetAsync("https://www.themealdb.com/api/json/v1/1/filter.php?c=Seafood");
            var content = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<JObject>(content);
            var meals = responseObj["meals"].ToObject<IList<Meal>>();
            var result = await _adminService.GetAllFavoriteMeals(meals);
            return Ok(result);
        }


        private void SetRefreshToken(RefreshToken newRefreshToken, Admin admin)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expire
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            admin.RefreshToken = newRefreshToken.Token;
            admin.CreatedRefreshToken = newRefreshToken.Created;
            admin.ExpireRefreshToken = newRefreshToken.Expire;
            var adminDto = _mapper.Map<UpdateAdminDto>(admin);
            _adminService.UpdateUser(admin.Id, adminDto);
        }
    }
}
