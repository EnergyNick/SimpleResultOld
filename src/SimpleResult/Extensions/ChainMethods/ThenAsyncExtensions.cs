using System;
using System.Threading.Tasks;

namespace SimpleResult
{
    public static partial class ResultsExtensions
    {
        public static async Task<Result> ThenActionAsync(this Result input, Func<Task> continuation)
        {
            if (input.IsSuccess)
                await continuation();
            return input;
        }

        public static async Task<Result<TValue>> ThenActionAsync<TValue>(this Result<TValue> input, 
            Func<TValue, Task> continuation)
        {
            if (input.IsSuccess)
                await continuation(input.ValueOrDefault);
            return input;
        }

        public static async Task<Result<TOutput>> ThenAsync<TOutput>(this Result input, 
            Func<Task<TOutput>> continuation)
        {
            return input.IsSuccess
                ? Result.Success(await continuation())
                : input.Convert.ToResultWithValue<TOutput>();
        }
        
        public static async Task<Result<TOutput>> ThenAsync<TOutput>(this Result input, 
            Func<Task<Result<TOutput>>> continuation)
        {
            return input.IsSuccess
                ? await continuation()
                : input.Convert.ToResultWithValue<TOutput>();
        }

        public static async Task<Result<TOutput>> ThenAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<TOutput>> continuation)
        {
            return input.IsSuccess
                ? Result.Success(await continuation(input.ValueOrDefault))
                : input.Convert.ToResultWithValue<TOutput>();
        }
        
        public static async Task<Result<TOutput>> ThenAsync<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Task<Result<TOutput>>> continuation)
        {
            return input.IsSuccess
                ? await continuation(input.ValueOrDefault)
                : input.Convert.ToResultWithValue<TOutput>();
        }
    }
}