using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SimpleResult.Settings;

namespace SimpleResult.Extensions
{
    public static partial class ResultsThenExtensions
    {
        public static async Task<Result> ThenTryAsync(this Result input, 
            Func<Task> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync(continuation, input.ThenAsync, input.WithError, catchHandler);
        }

        public static async Task<Result<TOutput>> ThenTryAsync<TOutput>(this Result<TOutput> input, 
            Func<TOutput, Task> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync(continuation, input.ThenAsync, input.WithError, catchHandler);
        }

        public static async Task<Result<TOutput>> ThenTryAsync<TOutput>(this Result input, 
            Func<Task<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync(continuation, input.ThenAsync, 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error),
                catchHandler);
        }
        
        public static async Task<Result<TOutput>> ThenTryAsync<TOutput>(this Result input, 
            Func<Task<Result<TOutput>>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync(continuation, input.ThenAsync,  
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error),
                catchHandler);
        }
        
        public static async Task<Result<TOutput>> ThenTryAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<Result<TOutput>>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync(continuation, input.ThenAsync,  
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error), 
                catchHandler);
        }

        public static async Task<Result<TOutput>> ThenTryAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            return await InternalTryAsync(continuation, input.ThenAsync, 
                error => new Result<TOutput>().WithReasons(input.Reasons).WithError(error), 
                catchHandler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<TOutput> InternalTryAsync<TInput, TOutput>(TInput argument, 
            Func<TInput, Task<TOutput>> action,
            Func<Error, TOutput> onErrorAction,
            Func<Exception, Error> catchHandler)
        {
            try
            {
                return await action(argument);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return onErrorAction(catchHandler(e));
            }
        }
    }
}