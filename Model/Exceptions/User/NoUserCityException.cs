using System;

namespace Model.Exceptions
{
    public class NoUserCityException : BusinessException
    {

        public NoUserCityException() : base(NO_USER_CITY)
        {
        }

        public NoUserCityException(string message) : base(NO_USER_CITY, message)
        {
        }

        public NoUserCityException(string message, Exception innerException) : base(NO_USER_CITY, message, innerException)
        {
        }
    }
}
