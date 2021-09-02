using System;

namespace SimpleResult.Core.Manipulations
{
    /// <summary>
    /// Providing methods for fluent work with <see cref="IExceptionalError"/>
    /// </summary>
    public interface IExceptionalErrorManipulationMethods<out TError> : IErrorManipulationMethods<TError>
        where TError: IExceptionalError
    {
        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        TError CausedBy(Exception exception);
    }
}