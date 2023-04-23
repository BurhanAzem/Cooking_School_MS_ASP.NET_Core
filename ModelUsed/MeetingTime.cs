namespace Cooking_School_ASP.NET.ModelUsed
{
    public class MeetingTime
    {
        private int _hour;
        private int _munite;
        public int Hour 
        {
            get
            {
                return (int)this._hour;
            }
            set
            {
                if (Hour > 24 || Hour < 0)
                    throw new ArgumentException("Enter valid hour value");
                _hour = value;

            }
        }
        
        public int Minute
        {
            get
            {
                return (int)this._munite;
            }
            set
            {
                if (Minute > 60 || Minute < 0)
                    throw new ArgumentException("Enter valid minute value");
                _munite = value;

            }
        }

    }
}
