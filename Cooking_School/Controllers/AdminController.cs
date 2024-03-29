﻿using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Cooking_School.Services.AdminService;
using Cooking_School.Services.AuthenticationServices;
using Cooking_School.Services.RefreshService;
using Cooking_School.Services.Dtos.AdminDto;
using Cooking_School.Core.ModelUsed;
using Cooking_School.Services.Dtos.UserDto;
using Cooking_School.Core.Models;

namespace Cooking_School.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminCookClassController> _logger;
        private readonly IAdminService _adminService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IMapper _mapper;
        public AdminController(ILogger<AdminCookClassController> logger, IAdminService adminService, IAuthenticationServices authentication, IMapper mapper, IRefreshTokenService refreshTokenService)
        {
            _logger = logger;
            _adminService = adminService;
            _authenticationServices = authentication;
            _mapper = mapper;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost]
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
            return Ok(result.Dto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto adminLoginDto)
        {
            _logger.LogInformation($"Login Attempt for {nameof(adminLoginDto)} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _authenticationServices.IsAuthenticate(adminLoginDto))
            {
                string token = await _authenticationServices.GenerateToken(adminLoginDto.Id);
                var refreshToken = await _refreshTokenService.GenerateRefreshToken();
                refreshToken.UserId = adminLoginDto.Id;
                await SetRefreshToken(refreshToken);
                return Accepted(new TokenRequest { Token = token, RefreshToken = refreshToken.Token });
            }
            return BadRequest("User not authenticate");
        }

        [HttpPost("logout")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> LogOut()
        {
            _logger.LogInformation($"Attempt to logout ");
            string authorizationHeader = Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                var result = await _adminService.LogOut(token);
                if (result.Exception is not null)
                {
                    throw new StatusCodeException(result.StatusCode.Value, result.Exception);
                }
                return Ok("Done");
            }
            _logger.LogInformation($"fail Attempt to logout ");
            throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, new Exception("there is problem in the token"));
        }


        [HttpPost("refresh-token")]  //Authorize(Roles = "Admin")
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await _authenticationServices.GetCurrentUser(HttpContext);
            var refreshToken = Request.Cookies["refreshToken"];
            RefreshToken userRefreshToken = await _refreshTokenService.GetRefrshToken(user.Id);
            if (!userRefreshToken.Token.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (userRefreshToken.ExpirationDate < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            var token = await _authenticationServices.GenerateToken(user.Id);
            var newRefreshToken = await _refreshTokenService.GenerateRefreshToken();
            newRefreshToken.UserId = user.Id;
            await SetRefreshToken(newRefreshToken);
            return Accepted(new TokenRequest { Token = token, RefreshToken = newRefreshToken.Token });
        }


        [HttpGet("favorite-meals")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllFavoriteMeals()
        {
            _logger.LogInformation($"Attempt GetAllFavoriteMeals of Meal");
            using var client = new HttpClient();
            var response = await client.GetAsync("https://www.themealdb.com/api/json/v1/1/filter.php?c=Seafood");
            var content = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<JObject>(content);
            var meals = responseObj["meals"].ToObject<IList<Meal>>();
            var result = await _adminService.GetAllFavoriteMeals(meals);
            return Ok(result);
        }


        private async Task SetRefreshToken(RefreshToken newRefreshToken)
        {
            await _refreshTokenService.SetRefreshToken(newRefreshToken);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.ExpirationDate
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }

    }
}
