namespace SimpleResult.Core.Manipulations
{
    /// <summary>
    /// Providing methods of fluent api for <see cref="IResult{TValue}"/>
    /// </summary>
    public interface IConclusionManipulationMethods<out TConclusion, in TValue>
        : IConclusionManipulationMethods<TConclusion> where TConclusion : IResult<TValue>
    {
        /// <summary>
        /// Return copy of <see cref="TConclusion"/> with changed <see cref="value"/>
        /// </summary>
        /// <returns>Copy of current <see cref="TConclusion"/> with new value</returns>
        TConclusion WithValue(TValue value);
    }
}