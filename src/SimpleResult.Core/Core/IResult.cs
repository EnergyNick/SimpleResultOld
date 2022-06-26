namespace SimpleResult.Core
{
    /// <summary>
    /// Represent result of operation without returning value
    /// </summary>
    public interface IResult : IConclusion
    {
        /// <summary>
        /// Provide conversion to <see cref="IResult{TNewValue}"/> with same reasons
        /// </summary>
        IResult<TNewValue> ToResult<TNewValue>(TNewValue value = default);
    }
}