using System;
using SimpleResult.Settings;

namespace SimpleResult.Extensions
{
    public static partial class ResultsThenExtensions
    {
        public static Result ThenTry(this Result input, 
            Action continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return input.Then(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.WithError(catchHandler(e));
            }
        }

        public static Result<TOutput> ThenTry<TOutput>(this Result<TOutput> input, 
            Action<TOutput> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return input.Then(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.WithError(catchHandler(e));
            }
        }

        public static Result<TOutput> ThenTry<TOutput>(this Result input, 
            Func<TOutput> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return input.Then(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.ToResult<TOutput>().WithError(catchHandler(e));
            }
        }
        
        public static Result<TOutput> ThenTry<TOutput>(this Result input, 
            Func<Result<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return input.Then(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.ToResult<TOutput>().WithError(catchHandler(e));
            }
        }
        
        public static Result<TOutput> ThenTry<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Result<TOutput>> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return input.Then(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.ToResult<TOutput>().WithError(catchHandler(e));
            }
        }

        public static Result<TOutput> ThenTry<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, TOutput> continuation,
            Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return input.Then(continuation);
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;
                
                return input.ToResult<TOutput>().WithError(catchHandler(e));
            }
        }
    }
}