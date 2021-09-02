namespace SimpleResult.Core.Converters
{
    /// <summary>
    /// Provides ability to convert <see cref="IResult"/>> to another form of result
    /// </summary>
    public interface IResultConverter
    {
        IResult<TNewValue> ToResultWithValue<TNewValue>(TNewValue value = default);
    }
}