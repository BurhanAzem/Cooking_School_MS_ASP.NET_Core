namespace Cooking_School_ASP.NET.ModelUsed
{
    public class RefreshToken
    {
        public string Token { get; set; }   
        public DateTime Created { get; set; }
        public DateTime Expire { get; set; }
    }
}
