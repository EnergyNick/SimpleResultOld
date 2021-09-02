using System;

namespace SimpleResult.Core.Exceptions
{
    public class FailedResultException : Exception
    {
        public FailedResultException()
        { }

        public FailedResultException(string message) : base(message)
        { }

        public FailedResultException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}