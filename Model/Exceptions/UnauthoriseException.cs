using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Exceptions
{
    public class UnauthoriseException : BusinessException
    {

        public UnauthoriseException() : base(UNAUTHORISE_OPERATION)
        {
        }

        public UnauthoriseException(string message) : base(UNAUTHORISE_OPERATION, message)
        {
        }

        public UnauthoriseException(string message, Exception innerException) : base(UNAUTHORISE_OPERATION, message, innerException)
        {
        }
    }
}
