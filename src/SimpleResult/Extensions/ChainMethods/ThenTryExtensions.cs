using System;
using SimpleResult.Settings;

namespace SimpleResult
{
    public static partial class ResultsExtensions
    {
        public static Result ThenTryAction(this Result input, 
            Action continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return InternalTry(() => input.ThenAction(continuation), input.WithError, catchHandler);
        }

        public static Result<TOutput> ThenTryAction<TOutput>(this Result<TOutput> input, 
            Action<TOutput> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return InternalTry(() => input.ThenAction(continuation), input.WithError, catchHandler);
        }

        public static Result<TOutput> ThenTry<TOutput>(this Result input, 
            Func<TOutput> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return InternalTry(() => input.Then(continuation), 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error),
                catchHandler);
        }
        
        public static Result<TOutput> ThenTry<TOutput>(this Result input, 
            Func<Result<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return InternalTry(() => input.Then(continuation), 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error),
                catchHandler);
        }
        
        public static Result<TOutput> ThenTry<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Result<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return InternalTry(() => input.Then(continuation), 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error), 
                catchHandler);
        }

        public static Result<TOutput> ThenTry<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, TOutput> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return InternalTry(() => input.Then(continuation), 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error), 
                catchHandler);
        }


        private static TOutput InternalTry<TOutput>(Func<TOutput> action,
            Func<Error, TOutput> onErrorAction,
            Func<Exception, Error> catchHandler)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return onErrorAction(catchHandler(e));
            }
        }
    }
}