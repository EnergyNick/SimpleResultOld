using System.Collections.Generic;
using System.Linq;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;
using SimpleResult.Extensions;

namespace SimpleResult
{
    public record Error : IError, IErrorManipulationMethods<Error>
    {
        private readonly List<IError> _causedErrors = new List<IError>();

        public string Message { get; init; }
        
        public IReadOnlyList<IError> CausedErrors
        {
            get => _causedErrors;
            init => _causedErrors = value as List<IError> ?? value.ToList();
        }

        public Error(string message)
        {
            Message = message ?? string.Empty;
        }

        public Error(string message, IError causedBy)
            : this(message)
        {
            _causedErrors.Add(causedBy);
        }

        public Error(string message, params IError[] causedBy)
            : this(message)
        {
            _causedErrors.AddRange(causedBy);
        }

        public Error(Error original)
        {
            Message = original.Message;
            _causedErrors = new List<IError>(original._causedErrors);
        }

        public virtual Error WithMessage(string message) => 
            this with { Message = message };

        public virtual Error CausedBy(IError error) => 
            this with { CausedErrors = _causedErrors.AddNewReason(error) };

        public virtual Error CausedBy(IEnumerable<IError> errors) =>
            this with { CausedErrors = _causedErrors.AddNewReasons(errors) };

        public virtual Error CausedBy(params IError[] errors) =>
            this with { CausedErrors = _causedErrors.AddNewReasons(errors) };
    }
}