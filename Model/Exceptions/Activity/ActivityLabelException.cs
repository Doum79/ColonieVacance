using System;

namespace Model.Exceptions
{
    public class ActivityLabelException : BusinessException
    {

        public ActivityLabelException() : base(NO_ACTIVITY_LABEL)
        {
        }

        public ActivityLabelException(string message) : base(NO_ACTIVITY_LABEL, message)
        {
        }

        public ActivityLabelException(string message, Exception innerException) : base(NO_ACTIVITY_LABEL, message, innerException)
        {
        }
    }
}
