using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Exceptions
{
    public class StayDataException : BusinessException
    {

        public StayDataException() : base(INVALID_DATA_STAY)
        {
        }

        public StayDataException(string message) : base(INVALID_DATA_STAY, message)
        {
        }

        public StayDataException(string message, Exception innerException) : base(INVALID_DATA_STAY, message, innerException)
        {
        }
    }
}
