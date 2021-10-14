#if !NET

using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;

namespace SimpleResult
{
    public class Error : IError, IErrorManipulationMethods<Error>
    {
        private List<IError> _causedErrors = new List<IError>();

        public string Message { get; protected set; }
        
        public IReadOnlyList<IError> CausedErrors
        {
            get => _causedErrors;
            protected set => _causedErrors = value as List<IError> ?? value.ToList();
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

        protected Error(Error original)
        {
            Message = original.Message;
            _causedErrors = new List<IError>(original._causedErrors);
        }

        public Error WithMessage(string message)
        {
            var clonedError = CloneSelf();
            clonedError.Message = message;
            
            return clonedError;
        }

        public Error CausedBy(IError error)
        {
            var clonedError = CloneSelf();
            clonedError._causedErrors.Add(error);
            
            return clonedError;
        }

        public Error CausedBy(IEnumerable<IError> errors)
        {
            var clonedError = CloneSelf();
            clonedError._causedErrors.AddRange(errors);
            
            return clonedError;
        }

        public Error CausedBy(params IError[] errors)
        {
            var clonedError = CloneSelf();
            clonedError._causedErrors.AddRange(errors);
            
            return clonedError;
        }

        public override string ToString()
        {
            return Serializer.BuildStringRepresentation(this, BuildFieldsStringRepresentation);
        }
        
        protected virtual void BuildFieldsStringRepresentation(StringBuilder builder)
        {
            builder.Append($"Message = {Message}, ");
            if(_causedErrors.Count != 0)
                builder.Append($"Caused errors count = {_causedErrors.Count}");
        }

        protected virtual Error CloneSelf()
        {
            return new Error(this);
        }
    }
}
#endif