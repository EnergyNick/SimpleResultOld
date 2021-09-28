using System.Collections.Generic;
using SimpleResult.Core;

namespace SimpleResult
{
    public record Success : ISuccess
    {
        public string Message { get; init; }

        public IReadOnlyDictionary<string, object> Metadata { get; init; }

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
    }
}