using System;

namespace Model.Exceptions
{
    public class NoStructureCityException : BusinessException
    {

        public NoStructureCityException() : base(NO_STRUCTURE_CITY)
        {
        }

        public NoStructureCityException(string message) : base(NO_STRUCTURE_CITY, message)
        {
        }

        public NoStructureCityException(string message, Exception innerException) : base(NO_STRUCTURE_CITY, message, innerException)
        {
        }
    }
}
