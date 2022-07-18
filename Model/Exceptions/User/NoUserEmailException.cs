using System;

namespace Model.Exceptions
{
    public class NoUserEmailException : BusinessException
    {

        public NoUserEmailException() : base(NO_USER_EMAIL)
        {
        }

        public NoUserEmailException(string message) : base(NO_USER_EMAIL, message)
        {
        }

        public NoUserEmailException(string message, Exception innerException) : base(NO_USER_EMAIL, message, innerException)
        {
        }
    }
}
