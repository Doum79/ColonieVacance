using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Exceptions
{
    public class EmailExistingException : BusinessException
    {

        public EmailExistingException() : base(EMAIL_EXISTING)
        {
        }

        public EmailExistingException(string message) : base(EMAIL_EXISTING, message)
        {
        }

        public EmailExistingException(string message, Exception innerException) : base(EMAIL_EXISTING, message, innerException)
        {
        }
    }
}
