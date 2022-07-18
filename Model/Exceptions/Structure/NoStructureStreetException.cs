using System;

namespace Model.Exceptions
{
    public class NoStructureStreetException : BusinessException
    {

        public NoStructureStreetException() : base(NO_STRUCTURE_STREET)
        {
        }

        public NoStructureStreetException(string message) : base(NO_STRUCTURE_STREET, message)
        {
        }

        public NoStructureStreetException(string message, Exception innerException) : base(NO_STRUCTURE_STREET, message, innerException)
        {
        }
    }
}
