using System;

namespace Model.Exceptions
{
    public class NoStructureNameException : BusinessException
    {

        public NoStructureNameException() : base(NO_STRUCTURE_NAME)
        {
        }

        public NoStructureNameException(string message) : base(NO_STRUCTURE_NAME, message)
        {
        }

        public NoStructureNameException(string message, Exception innerException) : base(NO_STRUCTURE_NAME, message, innerException)
        {
        }
    }
}
