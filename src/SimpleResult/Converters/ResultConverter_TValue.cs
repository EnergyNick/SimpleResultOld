using System;
using SimpleResult.Core;
using SimpleResult.Core.Converters;

namespace SimpleResult.Converters
{
    public class ResultConverter<TValue> : ResultConverter, IResultConverter<TValue>
    {
        internal ResultConverter(Result<TValue> target)
            : base(target)
        { }

        public Result ToResult() => new Result { Reasons = Target.Reasons };

        public Result<TNewValue> ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter)
        {
            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            if (!Target.IsSuccess)
                throw new InvalidOperationException(
                    "The value cannot be converted because the result is unsuccessful.");

            var target = ((Result<TValue>) Target);
            return new Result<TNewValue> { Reasons = Target.Reasons, Value = converter(target.Value) };
        }

        IResult IResultConverter<TValue>.ToResult() => ToResult();

        IResult<TNewValue> IResultConverter<TValue>.
            ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter) =>
            ToResultWithValueConverting(converter);
    }
}