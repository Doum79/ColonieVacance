using System;

namespace Model.Exceptions
{
    public class NoUserStreetException : BusinessException
    {

        public NoUserStreetException() : base(NO_USER_STREET)
        {
        }

        public NoUserStreetException(string message) : base(NO_USER_STREET, message)
        {
        }

        public NoUserStreetException(string message, Exception innerException) : base(NO_USER_STREET, message, innerException)
        {
        }
    }
}
