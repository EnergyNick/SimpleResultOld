using System;
using System.Collections.Generic;
using SimpleResult.Core.Converters;

namespace SimpleResult.Core
{
    public interface IResult
    {
        /// <summary>
        /// Is true if Reasons contains no errors.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Get all result reasons.
        /// </summary>
        IReadOnlyList<IReason> Reasons { get; }

        /// <summary>
        /// Get all errors reasons.
        /// </summary>
        IReadOnlyList<IError> Errors { get; }
        
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

        /// <summary>
        /// Provides ability to convert to another form of result
        /// </summary>
        IResultConverter Convert { get; }
    }
}