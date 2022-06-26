using System;
using System.Collections.Generic;

namespace SimpleResult.Core
{
    /// <summary>
    /// The base type of results, represent only final state of operation.
    /// Used for core abstraction of operations results.
    /// </summary>
    /// <remarks>It is more preferable to use <see cref="IResult"/> or <see cref="IResult{TValue}"/></remarks>
    public interface IConclusion
    {
        /// <summary>
        /// Is true if Reasons contains no errors.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Is true if Reasons contains errors.
        /// </summary>
        bool IsFailed { get; }

        /// <summary>
        /// Get all result reasons.
        /// </summary>
        IEnumerable<IReason> Reasons { get; }

        /// <summary>
        /// Get all errors reasons.
        /// </summary>
        IEnumerable<IError> Errors { get; }

        /// <summary>
        /// Used to find the necessary reason in result.
        /// </summary>
        /// <typeparam name="TReason">Type of searching reason</typeparam>
        bool HasReason<TReason>(Func<TReason, bool> predicate = null) where TReason : IReason;

        /// <summary>
        /// Used to find the necessary error in result.
        /// </summary>
        /// <typeparam name="TError">Type of searching error</typeparam>
        bool HasError<TError>(Func<TError, bool> predicate = null) where TError : IError;
    }
}