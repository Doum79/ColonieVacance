using System;

namespace Model.Exceptions
{
    public class StructureSiretExistException : BusinessException
    {

        public StructureSiretExistException() : base(STRUCTURE_SIRET_EXIST)
        {
        }

        public StructureSiretExistException(string message) : base(STRUCTURE_SIRET_EXIST, message)
        {
        }

        public StructureSiretExistException(string message, Exception innerException) : base(STRUCTURE_SIRET_EXIST, message, innerException)
        {
        }
    }
}
