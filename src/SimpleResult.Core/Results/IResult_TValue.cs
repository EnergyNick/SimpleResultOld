using System;
using SimpleResult.Core.Converters;
using SimpleResult.Core.Exceptions;

namespace SimpleResult.Core
{
    public interface IResult<out TValue> : IResult
    {
        /// <summary>
        /// Return current result value (If result has failed status thrown exception) 
        /// </summary>
        /// <exception cref="FailedResultOperationException">Thrown if result has failed status</exception>
        TValue Value { get; }
        
        /// <summary>
        /// Return current result value (If result has failed status return default value)
        /// </summary>
        TValue ValueOrDefault { get; }

        IResult ToResult();
        IResult<TNewValue> ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter);
    }
}