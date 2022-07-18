using System;

namespace Model.Exceptions
{
    public class NoStructureEmailException : BusinessException
    {

        public NoStructureEmailException() : base(NO_STRUCTURE_EMAIL)
        {
        }

        public NoStructureEmailException(string message) : base(NO_STRUCTURE_EMAIL, message)
        {
        }

        public NoStructureEmailException(string message, Exception innerException) : base(NO_STRUCTURE_EMAIL, message, innerException)
        {
        }
    }
}
