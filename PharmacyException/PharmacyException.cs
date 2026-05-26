using System;

namespace PharmacyException
{
    public class MedicineException : Exception
    {
        public MedicineException(string message) : base(message)
        {
        }
    }
}
