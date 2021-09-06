using System;
using SimpleResult.Exceptions;

namespace SimpleResult.Core
{
    public interface IResult<out TValue> : IResult
    {
        /// <summary>
        /// Return current result value (If result has failed status thrown exception) 
        /// </summary>
        /// <exception cref="OnFailedResultOperationException">Thrown if result has failed status</exception>
        TValue Value { get; }
        
        /// <summary>
        /// Return current result value (If result has failed status return default value)
        /// </summary>
        TValue ValueOrDefault { get; }

        /// <summary>
        /// Provide conversion with same reasons to <see cref="IResult"/>
        /// </summary>
        IResult ToResult();
        
        /// <summary>
        /// Provide conversion with same reasons and value changing
        /// </summary>
        IResult<TNewValue> ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter);
    }
}