using System;

namespace SimpleResult.Exceptions
{
    /// <summary>
    /// Thrown when 
    /// </summary>
    public class OperationOnFailedResultException : ResultException
    {
        private const string ExceptionMessage = "Result is in failed status, can't do operation";

        private static string FailedResultMessage(string operationName = null)
        {
            var message = ExceptionMessage;
            if (operationName != null)
                message += $": {operationName}.";
            
            return message;
        }

        public OperationOnFailedResultException(string operationName) : base(FailedResultMessage(operationName))
        { }

        public OperationOnFailedResultException(string operationName, Exception innerException) 
            : base(FailedResultMessage(operationName), innerException)
        { }
        
        public OperationOnFailedResultException(string message, string operationName) 
            : base(FailedResultMessage(operationName) + message)
        { }

        public OperationOnFailedResultException(string message, string operationName, Exception innerException) 
            : base(FailedResultMessage(operationName) + message, innerException)
        { }
    }
}