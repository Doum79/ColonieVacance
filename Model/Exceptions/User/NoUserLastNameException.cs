using System;

namespace Model.Exceptions
{
    public class NoUserLastNameException : BusinessException
    {

        public NoUserLastNameException() : base(NO_USER_LASTNAME)
        {
        }

        public NoUserLastNameException(string message) : base(NO_USER_LASTNAME, message)
        {
        }

        public NoUserLastNameException(string message, Exception innerException) : base(NO_USER_LASTNAME, message, innerException)
        {
        }
    }
}
