using System;

namespace Model.Exceptions
{
    public class NoStayFurtherInformationRedirectionLinkException : BusinessException
    {

        public NoStayFurtherInformationRedirectionLinkException() : base(NO_STAY_FURTHER_INFORMATION_REDIRECTION_LINK)
        {
        }

        public NoStayFurtherInformationRedirectionLinkException(string message) : base(NO_STAY_FURTHER_INFORMATION_REDIRECTION_LINK, message)
        {
        }

        public NoStayFurtherInformationRedirectionLinkException(string message, Exception innerException) : base(NO_STAY_FURTHER_INFORMATION_REDIRECTION_LINK, message, innerException)
        {
        }
    }
}
