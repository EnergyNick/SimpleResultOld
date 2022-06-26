using System;
using System.Collections.Generic;
using System.Linq;
using SimpleResult.Core;

namespace SimpleResult.Extensions
{
    public static class ReasonExtensions
    {
        public static bool HasReasonOfType<TReason>(this IEnumerable<IReason> reasons, Func<TReason, bool> predicate)
            where TReason : IReason
        {
            return reasons.Any(reason =>
                reason is TReason reasonOfType && (predicate == null || predicate(reasonOfType)));
        }

        public static bool HasErrorOfType<TError>(this IEnumerable<IError> errors, Func<TError, bool> predicate)
            where TError : IError
        {
            var enumeratedReasons = errors as ICollection<IError> ?? errors.ToArray();

            var anyErrors = HasReasonOfType(enumeratedReasons, predicate);
            return anyErrors || enumeratedReasons.Any(error => HasErrorOfType(error.CausedErrors, predicate));
        }

        /// <summary>
        /// Add reason to list and return that list. Used to improve readability of the code
        /// </summary>
        public static List<TReason> AddNewReason<TReason>(this List<TReason> reasons, TReason newReason)
            where TReason : IReason
        {
            reasons.Add(newReason);
            return reasons;
        }

        /// <summary>
        /// Add reason to list and return that list. Used to improve readability of the code
        /// </summary>
        public static List<TReason> AddNewReasons<TReason>(this List<TReason> reasons, IEnumerable<TReason> newReasons)
            where TReason : IReason
        {
            reasons.AddRange(newReasons);
            return reasons;
        }
    }
}