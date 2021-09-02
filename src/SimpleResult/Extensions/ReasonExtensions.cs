using System;
using System.Collections.Generic;
using System.Linq;
using SimpleResult.Core;

namespace SimpleResult
{
    public static class ReasonExtensions
    {
        public static bool HasReasonOfType<TReason>(this IEnumerable<IReason> reasons, Func<TReason, bool> predicate)
            where TReason : IReason
        {
            return reasons.Any(reason => 
                reason is TReason reasonOfType && (predicate == null || predicate(reasonOfType)));
        }
        
        public static bool HasErrorOfType<TError>(this IReadOnlyList<IError> errors, Func<TError, bool> predicate) 
            where TError : IError
        {
            var anyErrors = HasReasonOfType(errors, predicate);
            return anyErrors || errors.Any(error => HasErrorOfType(error.CausedErrors, predicate));
        }
        
        public static List<TReason> AddNewReason<TReason>(this List<TReason> reasons, TReason newReason)
            where TReason : IReason
        {
            reasons.Add(newReason);
            return reasons;
        }

        public static List<TReason> AddNewReasons<TReason>(this List<TReason> reasons, IEnumerable<TReason> newReasons)
            where TReason : IReason
        {
            reasons.AddRange(newReasons);
            return reasons;
        }
    }
}