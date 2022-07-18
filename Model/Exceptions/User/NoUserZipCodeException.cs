using System;

namespace Model.Exceptions
{
    public class NoUserZipCodeException : BusinessException
    {

        public NoUserZipCodeException() : base(NO_USER_ZIPCODE)
        {
        }

        public NoUserZipCodeException(string message) : base(NO_USER_ZIPCODE, message)
        {
        }

        public NoUserZipCodeException(string message, Exception innerException) : base(NO_USER_ZIPCODE, message, innerException)
        {
        }
    }
}
