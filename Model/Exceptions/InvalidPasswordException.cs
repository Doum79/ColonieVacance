using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Exceptions
{
    public class InvalidPasswordException : BusinessException
    {

        public InvalidPasswordException() : base(INVALID_PASSWORD)
        {
        }

        public InvalidPasswordException(string message) : base(INVALID_PASSWORD, message)
        {
        }

        public InvalidPasswordException(string message, Exception innerException) : base(INVALID_PASSWORD, message, innerException)
        {
        }
    }
}
