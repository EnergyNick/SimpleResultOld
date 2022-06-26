using SimpleResult.Core;

namespace SimpleResult.Serialization
{
    /// <summary>
    /// Provide serialization methods for <see cref="IConclusion"/> types.
    /// </summary>
    public interface IConclusionSerializer
    {
        /// <summary>
        /// Provide serialization of <see cref="IConclusion"/> types to human readable string
        /// </summary>
        string ConvertToHumanReadableString<TConclusion>(TConclusion conclusion) where TConclusion : IConclusion;
    }
}