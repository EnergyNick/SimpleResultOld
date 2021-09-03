﻿using System;
using SimpleResult.Core;
using SimpleResult.Core.Converters;
using SimpleResult.Core.Exceptions;

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
            if (Target.IsSuccess && converter == null)
                throw new ArgumentNullException(nameof(converter));

            var target = (Result<TValue>) Target;
            return new Result<TNewValue>
            {
                Reasons = Target.Reasons, 
                Value = target.IsSuccess ? converter(target.ValueOrDefault) : default
            };
        }

        IResult IResultConverter<TValue>.ToResult() => ToResult();

        IResult<TNewValue> IResultConverter<TValue>.
            ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter) =>
            ToResultWithValueConverting(converter);
    }
}