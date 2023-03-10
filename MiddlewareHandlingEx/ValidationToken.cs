using Azure.Core;
using Cooking_School_ASP.NET.Services;

namespace Cooking_School_ASP.NET.MiddlewareHandlingEx
{

    public class ValidationToken
    {
        private readonly RequestDelegate _next;
        private readonly IAdminService _adminService;

        public ValidationToken(RequestDelegate next, IAdminService adminService)
        {
            _next = next;
            _adminService = adminService;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                await _adminService.LogOut(token);
            }
            await _next(context);
        }
    }
}
