using System;

namespace Model.Exceptions
{
    public class NoStructureSiretException : BusinessException
    {

        public NoStructureSiretException() : base(NO_STRUCTURE_SIRET)
        {
        }

        public NoStructureSiretException(string message) : base(NO_STRUCTURE_SIRET, message)
        {
        }

        public NoStructureSiretException(string message, Exception innerException) : base(NO_STRUCTURE_SIRET, message, innerException)
        {
        }
    }
}
