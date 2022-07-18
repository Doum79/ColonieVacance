using System;

namespace Model.Exceptions
{
    public class ThematicLabelException : BusinessException
    {

        public ThematicLabelException() : base(NO_THEMATIC_LABEL)
        {
        }

        public ThematicLabelException(string message) : base(NO_THEMATIC_LABEL, message)
        {
        }

        public ThematicLabelException(string message, Exception innerException) : base(NO_THEMATIC_LABEL, message, innerException)
        {
        }
    }
}
