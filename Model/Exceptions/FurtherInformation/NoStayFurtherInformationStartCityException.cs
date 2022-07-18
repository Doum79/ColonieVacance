using System;

namespace Model.Exceptions
{
    public class NoStayFurtherInformationStartCityException : BusinessException
    {

        public NoStayFurtherInformationStartCityException() : base(NO_STAY_FURTHER_INFORMATION_START_CITY)
        {
        }

        public NoStayFurtherInformationStartCityException(string message) : base(NO_STAY_FURTHER_INFORMATION_START_CITY, message)
        {
        }

        public NoStayFurtherInformationStartCityException(string message, Exception innerException) : base(NO_STAY_FURTHER_INFORMATION_START_CITY, message, innerException)
        {
        }
    }
}
