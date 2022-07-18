using System;

namespace Model.Exceptions
{
    public class NoStructureCountryException : BusinessException
    {

        public NoStructureCountryException() : base(NO_STRUCTURE_COUNTRY)
        {
        }

        public NoStructureCountryException(string message) : base(NO_STRUCTURE_COUNTRY, message)
        {
        }

        public NoStructureCountryException(string message, Exception innerException) : base(NO_STRUCTURE_COUNTRY, message, innerException)
        {
        }
    }
}
