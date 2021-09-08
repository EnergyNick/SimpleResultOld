#if !NET
using System;
using System.Collections.Generic;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;

namespace SimpleResult
{
    public class ExceptionalError : Error, IExceptionalError, IExceptionalErrorManipulationMethods<ExceptionalError>
    {
        public Exception Exception { get; private set; }

        public ExceptionalError(Exception exception) 
            : base(exception.Message)
        {
            Exception = exception;
        }

        public ExceptionalError(string message, Exception exception) 
            : base(message)
        {
            Exception = exception;
        }

        public ExceptionalError(Exception exception, IError causedBy) 
            : base(exception.Message, causedBy)
        {
            Exception = exception;
        }

        public ExceptionalError(Exception exception, params IError[] causedBy) 
            : base(exception.Message, causedBy)
        {
            Exception = exception;
        }

        protected ExceptionalError(ExceptionalError original) 
            : base(original)
        {
            Exception = original.Exception;
        }

        public new ExceptionalError WithMessage(string message) => 
            (ExceptionalError) base.WithMessage(message);

        public new ExceptionalError CausedBy(IError error) => 
            (ExceptionalError) base.CausedBy(error);

        public new ExceptionalError CausedBy(IEnumerable<IError> errors) => 
            (ExceptionalError) base.CausedBy(errors);

        public new ExceptionalError CausedBy(params IError[] errors) => 
            (ExceptionalError) base.CausedBy(errors);

        public ExceptionalError CausedBy(Exception exception)
        {
            var clonedError = CloneSelf() as ExceptionalError;
            clonedError.Exception = exception;
            
            return clonedError;
        }

        protected override Error CloneSelf()
        {
            return new ExceptionalError(this);
        }
    }
}
#endif