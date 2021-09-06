using System;

namespace SimpleResult.Extensions
{
    public static partial class ResultsExtensions
    {
        public static Result OnSuccess(this Result result, Action<Result> onSuccessAction)
        {
            if (!result.IsSuccess)
                return result;
            
            onSuccessAction(result);
            return result;
        }
        
        public static Result<TValue> OnSuccess<TValue>(this Result<TValue> result, Action<Result<TValue>> onSuccessAction)
        {
            if (!result.IsSuccess)
                return result;
            
            onSuccessAction(result);
            return result;
        }
        
        public static Result OnFail(this Result result, Action<Result> onFailAction)
        {
            if (result.IsSuccess)
                return result;
            
            onFailAction(result);
            return result;
        }
        
        public static Result<TValue> OnFail<TValue>(this Result<TValue> result, Action<Result<TValue>> onFailAction)
        {
            if (result.IsSuccess)
                return result;
            
            onFailAction(result);
            return result;
        }
    }
}