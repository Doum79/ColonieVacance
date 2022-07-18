using System;

namespace Model.Exceptions
{
    public class NoStayFurtherInformationEndDateException : BusinessException
    {

        public NoStayFurtherInformationEndDateException() : base(NO_STAY_FURTHER_INFORMATION_END_DATE)
        {
        }

        public NoStayFurtherInformationEndDateException(string message) : base(NO_STAY_FURTHER_INFORMATION_END_DATE, message)
        {
        }

        public NoStayFurtherInformationEndDateException(string message, Exception innerException) : base(NO_STAY_FURTHER_INFORMATION_END_DATE, message, innerException)
        {
        }
    }
}
