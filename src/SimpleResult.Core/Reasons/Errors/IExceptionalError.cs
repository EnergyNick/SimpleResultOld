using System;

namespace SimpleResult.Core
{
    /// <summary>
    /// Represents the base type of error causes by exception.
    /// </summary>
    public interface IExceptionalError : IError
    {
        Exception Exception { get; }
    }
}