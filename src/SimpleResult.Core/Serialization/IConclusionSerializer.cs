using System.Text;
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

        /// <summary>
        /// Provide details of <see cref="IConclusion"/> type for converting to string
        /// </summary>
        /// <param name="conclusion"></param>
        /// <param name="builder"></param>
        /// <typeparam name="TConclusion"></typeparam>
        bool PrintMembersOf<TConclusion>(TConclusion conclusion, StringBuilder builder) where TConclusion : IConclusion;
    }
}