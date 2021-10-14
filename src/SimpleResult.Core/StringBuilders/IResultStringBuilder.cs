using SimpleResult.Core;

namespace SimpleResult.StringBuilders
{
    /// <summary>
    /// Provide serialization methods for <see cref="IResult"/>
    /// </summary>
    public interface IResultStringBuilder
    {
        string ConvertToString<TResult>(TResult result) where TResult : IResult;
        
        string ConvertToStringWithValue<TResult, TValue>(TResult result) where TResult : IResult<TValue>;
    }
}