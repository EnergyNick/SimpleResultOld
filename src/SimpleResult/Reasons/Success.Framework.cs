#if !NET
using System.Collections.Generic;
using System.Text;
using SimpleResult.Core;

namespace SimpleResult
{
    public class Success : ISuccess
    {
        public string Message { get; }

        public IReadOnlyDictionary<string, object> Metadata { get; }

        public Success()
        {
            Metadata = new Dictionary<string, object>();
            Message = string.Empty;
        }
        
        public Success(string message) 
            : this()
        {
            Message = message;
        }
        
        public Success(string message, IReadOnlyDictionary<string, object> metadata)
        {
            Message = message;
            Metadata = metadata;
        }
        
        public override string ToString()
        {
            return Serializer.BuildStringRepresentation(this, BuildFieldsStringRepresentation);
        }
        
        protected virtual void BuildFieldsStringRepresentation(StringBuilder builder)
        {
            builder.Append($"Message = {Message}, ");
        }
    }
}

#endif