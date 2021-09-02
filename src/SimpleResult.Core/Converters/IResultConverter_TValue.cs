using System;

namespace SimpleResult.Core.Converters
{
    /// <summary>
    /// Provides ability to convert <see cref="IResult{TValue}"/> to another form of result
    /// </summary>
    public interface IResultConverter<out TValue> : IResultConverter
    {
        IResult ToResult();
        IResult<TNewValue> ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter);
    }
}