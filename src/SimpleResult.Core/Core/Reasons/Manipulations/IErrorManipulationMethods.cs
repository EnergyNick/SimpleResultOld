using System.Collections.Generic;

namespace SimpleResult.Core.Manipulations
{
    /// <summary>
    /// Providing methods for fluent work with <see cref="IError"/>
    /// </summary>
    public interface IErrorManipulationMethods<out TError>
        where TError: IError
    {
        /// <summary>
        /// Set the message
        /// </summary>
        TError WithMessage(string message);

        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        TError CausedBy(IError error);

        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        TError CausedBy(IEnumerable<IError> errors);

        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        TError CausedBy(params IError[] errors);
    }
}