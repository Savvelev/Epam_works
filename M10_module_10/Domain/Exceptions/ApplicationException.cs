using System;

namespace Domain.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message)
        {

        }
    }
}
