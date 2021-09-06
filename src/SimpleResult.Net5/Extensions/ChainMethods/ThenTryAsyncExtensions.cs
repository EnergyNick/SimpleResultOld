using System;
using System.Threading.Tasks;
using SimpleResult.Settings;

namespace SimpleResult.Extensions
{
    public static partial class ResultsExtensions
    {
        public static async Task<Result> ThenTryActionAsync(this Result input, 
            Func<Task> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync(() => input.ThenActionAsync(continuation), input.WithError, catchHandler);
        }

        public static async Task<Result> ThenTryActionAsync<TOutput>(this Result<TOutput> input, 
            Func<TOutput, Task> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync<Result>(() => input.ThenActionAsync(continuation), input.WithError, catchHandler);
        }

        public static async Task<Result> ThenTryAsync<TOutput>(this Result input, 
            Func<Task<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync<Result>(() => input.ThenAsync(continuation), 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error),
                catchHandler);
        }
        
        public static async Task<Result> ThenTryAsync<TOutput>(this Result input, 
            Func<Task<Result>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync<Result>(() => input.ThenAsync(continuation), 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error),
                catchHandler);
        }
        
        public static async Task<Result> ThenTryAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<Result>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync<Result>(() => input.ThenAsync(continuation), 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error), 
                catchHandler);
        }

        public static async Task<Result> ThenTryAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync<Result>(() => input.ThenAsync(continuation), 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error), 
                catchHandler);
        }

        private static async Task<TOutput> InternalTryAsync<TOutput>(Func<Task<TOutput>> action,
            Func<Error, TOutput> onErrorAction,
            Func<Exception, Error> catchHandler)
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return onErrorAction(catchHandler(e));
            }
        }
    }
}