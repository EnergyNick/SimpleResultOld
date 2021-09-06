using System;
using System.Threading.Tasks;

namespace SimpleResult.Extensions
{
    public static partial class ResultsExtensions
    {
        public static async Task<Result> ThenActionAsync(this Result input, Func<Task> continuation)
        {
            if (input.IsSuccess)
                await continuation();
            return input;
        }

        public static async Task<Result> ThenActionAsync<TValue>(this Result<TValue> input, 
            Func<TValue, Task> continuation)
        {
            if (input.IsSuccess)
                await continuation(input.ValueOrDefault);
            return input;
        }

        public static async Task<Result> ThenAsync<TOutput>(this Result input, 
            Func<Task<TOutput>> continuation)
        {
            return input.IsSuccess
                ? Result.Success(await continuation())
                : input.ToResult<TOutput>();
        }
        
        public static async Task<Result> ThenAsync<TOutput>(this Result input, 
            Func<Task<Result>> continuation)
        {
            return input.IsSuccess
                ? await continuation()
                : input.ToResult<TOutput>();
        }

        public static async Task<Result> ThenAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<TOutput>> continuation)
        {
            return input.IsSuccess
                ? Result.Success(await continuation(input.ValueOrDefault))
                : input.ToResult<TOutput>();
        }
        
        public static async Task<Result> ThenAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<Result>> continuation)
        {
            return input.IsSuccess
                ? await continuation(input.ValueOrDefault)
                : input.ToResult<TOutput>();
        }
    }
}