using System;

namespace Model.Exceptions
{
    public class AnyStayFurtherInformationException : BusinessException
    {

        public AnyStayFurtherInformationException() : base(ANY_STAY_FURTHER_INFORMATION)
        {
        }

        public AnyStayFurtherInformationException(string message) : base(ANY_STAY_FURTHER_INFORMATION, message)
        {
        }

        public AnyStayFurtherInformationException(string message, Exception innerException) : base(ANY_STAY_FURTHER_INFORMATION, message, innerException)
        {
        }
    }
}
