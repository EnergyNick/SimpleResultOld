using System;
using System.Threading.Tasks;
using SimpleResult.Core;
using SimpleResult.Settings;

namespace SimpleResult
{
    public partial class Result
    {
        /// <summary>
        /// Create empty result with success status
        /// </summary>
        public static Result Ok() => new();

        /// <summary>
        /// Creates a failed result with the given error
        /// </summary>
        public static Result Fail(IError error)
        {
            return new Result().WithError(error);
        }
        
        /// <summary>
        /// Creates a failed result with the given error message.
        /// Message will be transformed to <see cref="Error"/>
        /// </summary>
        public static Result Fail(string errorMessage)
        {
            return new Result().WithError(new Error(errorMessage));
        }
        
        /// <summary>
        /// Creates a success result with the given value
        /// </summary>
        public static Result<TValue> Ok<TValue>(TValue value = default)
        {
            return new Result<TValue> { Value = value };
        }
        
        /// <summary>
        /// Creates a failed result with the given error
        /// </summary>
        public static Result<TValue> Fail<TValue>(IError error)
        {
            return new Result<TValue>().WithError(error);
        }
        
        /// <summary>
        /// Creates a failed result with the given error message.
        /// Message will be transformed to <see cref="Error"/>
        /// </summary>
        public static Result<TValue> Fail<TValue>(string errorMessage)
        {
            return new Result<TValue>().WithError(new Error(errorMessage));
        }
        
        /// <summary>
        /// Creates a failed result with the given exception.
        /// Message will be transformed to <see cref="ExceptionalError"/>
        /// </summary>
        public static Result<TValue> Fail<TValue>(Exception exception)
        {
            return new Result<TValue>().WithError(new ExceptionalError(exception));
        }

        /// <summary>
        /// Executes the action and catch all exceptions, If they will be thrown within the action.
        /// Exception transforming to Error by <see cref="catchHandler"/> or by default catch handler from <see cref="ResultSettings.Parameters"/> 
        /// </summary>
        public static Result Try(Action action, Func<Exception, Error> catchHandler = null)
        {
            try
            {
                action();
                return Ok();
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return Fail(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the async action and catch all exceptions, If they will be thrown within the action.
        /// Exception transforming to Error by <see cref="catchHandler"/> or by default catch handler from <see cref="ResultSettings.Parameters"/> 
        /// </summary>
        public static async Task<Result> Try(Func<Task> action, Func<Exception, Error> catchHandler = null)
        {
            try
            {
                await action();
                return Ok();
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return Fail(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the action with return value and catch all exceptions, If they will be thrown within the action.
        /// Exception transforming to Error by <see cref="catchHandler"/> or by default catch handler from <see cref="ResultSettings.Parameters"/> 
        /// </summary>
        public static Result<T> Try<T>(Func<T> action, Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return Ok(action());
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return Fail<T>(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the async action with return value and catch all exceptions, If they will be thrown within the action.
        /// Exception transforming to Error by <see cref="catchHandler"/> or by default catch handler from <see cref="ResultSettings.Parameters"/> 
        /// </summary>
        public static async Task<Result<T>> Try<T>(Func<Task<T>> action, Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return Ok(await action());
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return Fail<T>(catchHandler(e));
            }
        }
    }
}