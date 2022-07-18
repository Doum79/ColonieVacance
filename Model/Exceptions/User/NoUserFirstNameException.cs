using System;

namespace Model.Exceptions
{
    public class NoUserFirstNameException : BusinessException { 

        public NoUserFirstNameException() : base(NO_USER_FIRSTNAME)
        {
        }

        public NoUserFirstNameException(string message) : base(NO_USER_FIRSTNAME, message)
        {
        }

        public NoUserFirstNameException(string message, Exception innerException) : base(NO_USER_FIRSTNAME, message, innerException)
        {
        }
    }
}
