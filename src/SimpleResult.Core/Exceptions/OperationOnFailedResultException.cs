using System;
#pragma warning disable CS1573

namespace SimpleResult.Exceptions
{
    /// <summary>
    /// Thrown when operation invoked on unsuccessful result
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

        /// <inheritdoc />
        /// <param name="operationName">Name of operation, that threw the exception</param>
        public OperationOnFailedResultException(string operationName) : base(FailedResultMessage(operationName))
        { }

        /// <inheritdoc />
        /// <param name="operationName">Name of operation, that threw the exception</param>
        public OperationOnFailedResultException(string operationName, Exception innerException)
            : base(FailedResultMessage(operationName), innerException)
        { }

        /// <inheritdoc />
        /// <param name="operationName">Name of operation, that threw the exception</param>
        public OperationOnFailedResultException(string message, string operationName)
            : base(FailedResultMessage(operationName) + message)
        { }

        /// <inheritdoc />
        public OperationOnFailedResultException(string message, string operationName, Exception innerException)
            : base(FailedResultMessage(operationName) + message, innerException)
        { }
    }
}