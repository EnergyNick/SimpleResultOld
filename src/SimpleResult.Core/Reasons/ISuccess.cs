using System.Collections.Generic;

namespace SimpleResult.Core
{
    /// <summary>
    /// Represents the base type of all success causes.
    /// </summary>
    public interface ISuccess : IReason
    {
        IReadOnlyDictionary<string, object> Metadata { get; }
    }
}