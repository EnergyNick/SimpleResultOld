using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleResult.Core;
using SimpleResult.Settings;

namespace SimpleResult.ReadOnly
{
    public partial record ReadOnlyResult
    {
        /// <summary>
        /// Create empty ReadOnlyResult with success status
        /// </summary>
        public static ReadOnlyResult Success() => new();

        /// <summary>
        /// Creates a failed ReadOnlyResult with the given error
        /// </summary>
        public static ReadOnlyResult Fail(IError error)
        {
            return CreateResultWithReason<ReadOnlyResult>(error);
        }
        
        /// <summary>
        /// Creates a failed ReadOnlyResult with the given error message.
        /// Message will be transformed to <see cref="Error"/>
        /// </summary>
        public static ReadOnlyResult Fail(string errorMessage)
        {
            return CreateResultWithReason<ReadOnlyResult>(new Error(errorMessage));
        }
        
        /// <summary>
        /// Creates a success ReadOnlyResult with the given value
        /// </summary>
        public static ReadOnlyResult<TValue> Success<TValue>(TValue value = default)
        {
            return new ReadOnlyResult<TValue> { Value = value };
        }
        
        /// <summary>
        /// Creates a failed ReadOnlyResult with the given error
        /// </summary>
        public static ReadOnlyResult<TValue> Fail<TValue>(IError error)
        {
            return CreateResultWithReason<ReadOnlyResult<TValue>>(error);
        }
        
        /// <summary>
        /// Creates a failed ReadOnlyResult with the given error message.
        /// Message will be transformed to <see cref="Error"/>
        /// </summary>
        public static ReadOnlyResult<TValue> Fail<TValue>(string errorMessage)
        {
            return CreateResultWithReason<ReadOnlyResult<TValue>>(new Error(errorMessage));
        }
        
        /// <summary>
        /// Creates a failed ReadOnlyResult with the given exception.
        /// Message will be transformed to <see cref="ExceptionalError"/>
        /// </summary>
        public static ReadOnlyResult<TValue> Fail<TValue>(Exception exception)
        {
            return CreateResultWithReason<ReadOnlyResult<TValue>>(new ExceptionalError(exception));
        }

        /// <summary>
        /// Executes the action and catch all exceptions, If they will be thrown within the action.
        /// Exception transforming to Error by <see cref="catchHandler"/> or by default catch handler from <see cref="ReadOnlyResultSettings.Parameters"/> 
        /// </summary>
        public static ReadOnlyResult Try(Action action, Func<Exception, Error> catchHandler = null)
        {
            try
            {
                action();
                return Success();
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return Fail(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the async action and catch all exceptions, If they will be thrown within the action.
        /// Exception transforming to Error by <see cref="catchHandler"/> or by default catch handler from <see cref="ReadOnlyResultSettings.Parameters"/> 
        /// </summary>
        public static async Task<ReadOnlyResult> Try(Func<Task> action, Func<Exception, Error> catchHandler = null)
        {
            try
            {
                await action();
                return Success();
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return Fail(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the action with return value and catch all exceptions, If they will be thrown within the action.
        /// Exception transforming to Error by <see cref="catchHandler"/> or by default catch handler from <see cref="ReadOnlyResultSettings.Parameters"/> 
        /// </summary>
        public static ReadOnlyResult<T> Try<T>(Func<T> action, Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return Success(action());
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return Fail<T>(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the async action with return value and catch all exceptions, If they will be thrown within the action.
        /// Exception transforming to Error by <see cref="catchHandler"/> or by default catch handler from <see cref="ReadOnlyResultSettings.Parameters"/> 
        /// </summary>
        public static async Task<ReadOnlyResult<T>> Try<T>(Func<Task<T>> action, Func<Exception, Error> catchHandler = null)
        {
            try
            {
                return Success(await action());
            }
            catch (Exception e)
            {
                catchHandler ??= ResultSettings.Parameters.DefaultTryCatchHandler;

                return Fail<T>(catchHandler(e));
            }
        }

        private static TResult CreateResultWithReason<TResult>(IReason reason)
            where TResult : ReadOnlyResult, new()
        {
            return new TResult() { Reasons = new List<IReason> { reason } };
        }
    }
}