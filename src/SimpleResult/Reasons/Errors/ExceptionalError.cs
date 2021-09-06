using System;
using System.Collections.Generic;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;

namespace SimpleResult
{
    public record ExceptionalError : Error, IExceptionalError, IExceptionalErrorManipulationMethods<ExceptionalError>
    {
        public Exception Exception { get; init; }

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

        public ExceptionalError(ExceptionalError original) 
            : base(original)
        {
            Exception = original.Exception;
        }

        public override ExceptionalError WithMessage(string message) => 
            (ExceptionalError) base.WithMessage(message);

        public override ExceptionalError CausedBy(IError error) => 
            (ExceptionalError) base.CausedBy(error);

        public override ExceptionalError CausedBy(IEnumerable<IError> errors) => 
            (ExceptionalError) base.CausedBy(errors);

        public override ExceptionalError CausedBy(params IError[] errors) => 
            (ExceptionalError) base.CausedBy(errors);

        public virtual ExceptionalError CausedBy(Exception exception) =>
            this with { Exception = exception };
    }
}