using System;

namespace SimpleResult
{
    public static partial class ResultsExtensions
    {
        public static Result ThenAction(this Result input, Action continuation)
        {
            if (input.IsSuccess)
                continuation();
            return input;
        }

        public static Result<TValue> ThenAction<TValue>(this Result<TValue> input, Action<TValue> continuation)
        {
            if (input.IsSuccess)
                continuation(input.ValueOrDefault);
            return input;
        }

        public static Result<TOutput> Then<TOutput>(this Result input, Func<TOutput> continuation)
        {
            return input.IsSuccess
                ? continuation().AsResult()
                : input.ToResult<TOutput>();
        }
        
        public static Result<TOutput> Then<TOutput>(this Result input, Func<Result<TOutput>> continuation)
        {
            return input.IsSuccess
                ? continuation()
                : input.ToResult<TOutput>();
        }

        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, TOutput> continuation)
        {
            return input.IsSuccess
                ? continuation(input.ValueOrDefault).AsResult()
                : input.ToResult<TOutput>();
        }
        
        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Result<TOutput>> continuation)
        {
            return input.IsSuccess
                ? continuation(input.ValueOrDefault)
                : input.ToResult<TOutput>();
        }
    }
}