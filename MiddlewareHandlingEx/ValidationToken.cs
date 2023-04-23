using Azure.Core;
using Cooking_School_ASP.NET.IRepository;
using Cooking_School_ASP.NET.ModelUsed;
using Cooking_School_ASP.NET.Services.AdminService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace Cooking_School_ASP.NET.MiddlewareHandlingEx
{

    public class ValidationToken
    {
        private readonly RequestDelegate _next;
        private IUnitOfWork _unitOfWork;

        public ValidationToken(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            string authorizationHeader = context.Request.Headers["Authorization"];
            string route = context.Request.Path.Value; // Get the request path as a string

            if (!IsLogin(route) && !string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                var Invalid = await unitOfWork.BlackLists.Get(t => t.Invalid_Token == token);
                if (Invalid is not null)
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    string message = "You have invalid token (Logged Out)";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message }));
                    return; // Stop the request processing
                }
            }
            await _next(context);
        }

        private bool IsLogin(string route)
        {
            if (route.Equals("/api/admins/login") ||
                route.Equals("/api/trainees/login") ||
                route.Equals("/api/chefs/login"))
                return true;
            else
                return false;
        }
    }
}
