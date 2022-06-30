﻿using System;
using SimpleResult.Exceptions;

namespace SimpleResult.Core
{
    /// <summary>
    /// Represent result of operation with returning value
    /// </summary>
    /// <typeparam name="TValue">Attached returning value of operation</typeparam>
    public interface IResult<out TValue> : IConclusion
    {
        /// <summary>
        /// Return current result value (If result has failed status, an exception will be thrown)
        /// </summary>
        /// <exception cref="OperationOnFailedResultException">Thrown if result has failed status</exception>
        TValue Value { get; }

        /// <summary>
        /// Return current result value (If result has failed status, will be returned default value)
        /// </summary>
        TValue ValueOrDefault { get; }

        /// <summary>
        /// Provide conversion to <see cref="IResult"/> with same reasons
        /// </summary>
        /// <returns>Copy of current result  </returns>
        IResult ToResult();

        /// <summary>
        /// Provide conversion to <see cref="IResult{TValue}"/> with value changing
        /// </summary>
        /// <param name="converter"></param>
        /// <returns></returns>
        IResult<TNewValue> ToResult<TNewValue>(Func<TValue, TNewValue> converter);
    }
}