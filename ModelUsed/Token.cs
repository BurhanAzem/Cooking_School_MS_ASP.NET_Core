using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace Cooking_School_ASP.NET.ModelUsed
{
    public class TokenClaims
    {
        public string token { get; set; }
        public IList<Claim> claims { get; set; }
    }
}
