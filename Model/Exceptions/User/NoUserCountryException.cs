using System;

namespace Model.Exceptions
{
    public class NoUserCountryException : BusinessException
    {

        public NoUserCountryException() : base(NO_USER_COUNTRY)
        {
        }

        public NoUserCountryException(string message) : base(NO_USER_COUNTRY, message)
        {
        }

        public NoUserCountryException(string message, Exception innerException) : base(NO_USER_COUNTRY, message, innerException)
        {
        }
    }
}
