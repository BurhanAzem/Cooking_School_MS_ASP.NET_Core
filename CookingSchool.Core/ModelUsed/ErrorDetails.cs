namespace Backend_Controller_Burhan.Models
{
    using System.Text.Json;
    namespace Cooking_School.Core
        {
        public class ErrorDetails : Exception
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
    }
}
