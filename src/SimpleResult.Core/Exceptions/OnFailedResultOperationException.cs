using System;

namespace SimpleResult.Exceptions
{
    /// <summary>
    /// Thrown when 
    /// </summary>
    public class OnFailedResultOperationException : ResultException
    {
        private const string ExceptionMessage = "Result is in failed status, can't do operation";

        private static string FailedResultMessage(string operationName = null)
        {
            var message = ExceptionMessage;
            if (operationName != null)
                message += $": {operationName}.";
            
            return message;
        }

        public OnFailedResultOperationException(string operationName) : base(FailedResultMessage(operationName))
        { }

        public OnFailedResultOperationException(string operationName, Exception innerException) 
            : base(FailedResultMessage(operationName), innerException)
        { }
        
        public OnFailedResultOperationException(string message, string operationName) 
            : base(FailedResultMessage(operationName) + message)
        { }

        public OnFailedResultOperationException(string message, string operationName, Exception innerException) 
            : base(FailedResultMessage(operationName) + message, innerException)
        { }
    }
}