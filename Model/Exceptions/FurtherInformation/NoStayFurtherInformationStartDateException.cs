using System;

namespace Model.Exceptions
{
    public class NoStayFurtherInformationStartDateException : BusinessException
    {

        public NoStayFurtherInformationStartDateException() : base(NO_STAY_FURTHER_INFORMATION_START_DATE)
        {
        }

        public NoStayFurtherInformationStartDateException(string message) : base(NO_STAY_FURTHER_INFORMATION_START_DATE, message)
        {
        }

        public NoStayFurtherInformationStartDateException(string message, Exception innerException) : base(NO_STAY_FURTHER_INFORMATION_START_DATE, message, innerException)
        {
        }
    }
}
