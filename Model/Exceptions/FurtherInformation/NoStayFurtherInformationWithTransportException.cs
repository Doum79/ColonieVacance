using System;

namespace Model.Exceptions
{
    public class NoStayFurtherInformationWithTransportException : BusinessException
    {

        public NoStayFurtherInformationWithTransportException() : base(NO_STAY_FURTHER_INFORMATION_WITH_TRANSPORT)
        {
        }

        public NoStayFurtherInformationWithTransportException(string message) : base(NO_STAY_FURTHER_INFORMATION_WITH_TRANSPORT, message)
        {
        }

        public NoStayFurtherInformationWithTransportException(string message, Exception innerException) : base(NO_STAY_FURTHER_INFORMATION_WITH_TRANSPORT, message, innerException)
        {
        }
    }
}
