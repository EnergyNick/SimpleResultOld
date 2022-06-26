using System.Collections.Generic;

namespace SimpleResult.Core
{
    /// <summary>
    /// Represents the base type of all success causes.
    /// </summary>
    public interface ISuccess : IReason
    {
        /// <summary>
        /// Additional data from method, use only for utilities
        /// </summary>
        IReadOnlyDictionary<string, object> Metadata { get; }
    }
}