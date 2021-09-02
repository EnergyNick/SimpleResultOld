using System;

namespace SimpleResult.Core.Exceptions
{
    public class FailedResultOperationException : FailedResultException
    {
        private const string ExceptionMessage = "Result is in failed status, can't do operation";

        private static string FailedResultMessage(string operationName = null)
        {
            var message = ExceptionMessage;
            if (operationName != null)
                message += $": {operationName}.";
            
            return message;
        }

        public FailedResultOperationException(string operationName) : base(FailedResultMessage(operationName))
        { }

        public FailedResultOperationException(string operationName, Exception innerException) 
            : base(FailedResultMessage(operationName), innerException)
        { }
        
        public FailedResultOperationException(string message, string operationName) 
            : base(FailedResultMessage(operationName) + message)
        { }

        public FailedResultOperationException(string message, string operationName, Exception innerException) 
            : base(FailedResultMessage(operationName) + message, innerException)
        { }
    }
}