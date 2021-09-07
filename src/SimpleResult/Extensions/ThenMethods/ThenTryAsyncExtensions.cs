using System;
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
            try
            {
                return await input.ThenAsync(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.WithError(catchHandler(e));
            }
        }

        public static async Task<Result<TOutput>> ThenTryAsync<TOutput>(this Result<TOutput> input, 
            Func<TOutput, Task> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return await input.ThenAsync(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.WithError(catchHandler(e));
            }
        }

        public static async Task<Result<TOutput>> ThenTryAsync<TOutput>(this Result input, 
            Func<Task<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return await input.ThenAsync(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.ToResult<TOutput>().WithError(catchHandler(e));
            }
        }
        
        public static async Task<Result<TOutput>> ThenTryAsync<TOutput>(this Result input, 
            Func<Task<Result<TOutput>>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return await input.ThenAsync(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.ToResult<TOutput>().WithError(catchHandler(e));
            }
        }
        
        public static async Task<Result<TOutput>> ThenTryAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<Result<TOutput>>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return await input.ThenAsync(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.ToResult<TOutput>().WithError(catchHandler(e));
            }
        }

        public static async Task<Result<TOutput>> ThenTryAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return await input.ThenAsync(continuation);
            }
            catch (Exception e)
            {
                catchHandler = catchHandler ?? ResultSettings.Parameters.DefaultTryCatchHandler;

                return input.ToResult<TOutput>().WithError(catchHandler(e));
            }
        }
    }
}