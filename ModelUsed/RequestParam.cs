namespace Cooking_School_ASP.NET.Dtos
{
    public class RequestParam
    {
        const int MAXPAGESIZE = 15;
        public int PageNumber { get; set; }
        public int PageSize
        {
            get
            {
                return PageSize;
            }
            set
            {
                PageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE: value;
            }
        }
     }
}
