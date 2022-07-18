using System;

namespace Model.Exceptions
{
    public class NoStayFurtherInformationPriceException : BusinessException
    {

        public NoStayFurtherInformationPriceException() : base(NO_STAY_FURTHER_INFORMATION_PRICE)
        {
        }

        public NoStayFurtherInformationPriceException(string message) : base(NO_STAY_FURTHER_INFORMATION_PRICE, message)
        {
        }

        public NoStayFurtherInformationPriceException(string message, Exception innerException) : base(NO_STAY_FURTHER_INFORMATION_PRICE, message, innerException)
        {
        }
    }
}
