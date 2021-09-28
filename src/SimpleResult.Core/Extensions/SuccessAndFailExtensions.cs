using System;
using System.Collections.Generic;
using SimpleResult.Core;

namespace SimpleResult.Extensions
{
    public static class ResultsSuccessExtensions
    {
        /// <summary>
        /// Call action only if <see cref="result"/> is success
        /// </summary>
        public static TResult OnSuccess<TResult>(this TResult result, Action<TResult> onSuccessAction)
            where TResult : IResult
        {
            if (!result.IsSuccess)
                return result;

            onSuccessAction(result);
            return result;
        }

        /// <summary>
        /// Call action only if <see cref="result"/> is failed
        /// </summary>
        public static TResult OnFail<TResult>(this TResult result, Action<TResult> onFailAction)
            where TResult : IResult
        {
            if (result.IsSuccess)
                return result;
            
            onFailAction(result);
            return result;
        }
        
        /// <summary>
        /// Call action only if <see cref="result"/> is failed
        /// </summary>
        public static TResult OnFail<TResult>(this TResult result, Action<TResult, IEnumerable<IError>> onFailAction)
            where TResult : IResult
        {
            if (result.IsSuccess)
                return result;
            
            onFailAction(result, result.Errors);
            return result;
        }
    }
}