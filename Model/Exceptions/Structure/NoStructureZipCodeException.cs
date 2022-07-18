using System;

namespace Model.Exceptions
{
    public class NoStructureZipCodeException : BusinessException
    {

        public NoStructureZipCodeException() : base(NO_STRUCTURE_ZIPCODE)
        {
        }

        public NoStructureZipCodeException(string message) : base(NO_STRUCTURE_ZIPCODE, message)
        {
        }

        public NoStructureZipCodeException(string message, Exception innerException) : base(NO_STRUCTURE_ZIPCODE, message, innerException)
        {
        }
    }
}
