using System.Collections.Generic;

namespace SimpleResult.Core
{
    /// <summary>
    /// Represents the base type of all error causes.
    /// </summary>
    public interface IError : IReason
    {
        /// <summary>
        /// Errors causing this error
        /// </summary>
        IReadOnlyList<IError> CausedErrors { get; }
    }
}