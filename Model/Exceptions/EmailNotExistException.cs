using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Exceptions
{
    public class EmailNotExistException : BusinessException
    {

        public EmailNotExistException() : base(EMAIL_NOT_EXIST)
        {
        }

        public EmailNotExistException(string message) : base(EMAIL_NOT_EXIST, message)
        {
        }

        public EmailNotExistException(string message, Exception innerException) : base(EMAIL_NOT_EXIST, message, innerException)
        {
        }
    }
}
