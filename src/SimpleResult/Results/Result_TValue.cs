using System;
using System.Collections.Generic;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;
using SimpleResult.Exceptions;

namespace SimpleResult
{
    public class Result<TValue> : Result, IResult<TValue>, IResultManipulationMethods<Result<TValue>, TValue>
    {
        private TValue _value;

        /// <summary>
        /// Get the Value. If result is failed then a default value is returned. Opposite see property <see cref="Value"/>.
        /// </summary>
        public TValue ValueOrDefault => _value;

        /// <summary>
        /// Get the Value. If result is failed then an Exception is thrown because a failed result has no value.
        /// Opposite see property <see cref="ValueOrDefault"/>.
        /// </summary>
        public TValue Value
        {
            get
            {
                if (!IsSuccess)
                    throw new OnFailedResultOperationException("Get value");

                return _value;
            }
            set
            {
                if (!IsSuccess)
                    throw new OnFailedResultOperationException("Set value");

                _value = value;
            }
        }

        public Result()
        {
            _value = default;
        }
        
        public Result ToResult() => new() { Reasons = Reasons };

        public Result<TNewValue> ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter)
        {
            if (IsSuccess && converter == null)
                throw new ArgumentNullException(nameof(converter));
            
            return new Result<TNewValue>
            {
                Reasons = Reasons, 
                Value = IsSuccess ? converter(_value) : default
            };
        }

        public override Result<TValue> WithReason(IReason reason) => 
            (Result<TValue>) base.WithReason(reason);
        
        public override Result<TValue> WithReasons(IEnumerable<IReason> reasons) => 
            (Result<TValue>) base.WithReasons(reasons);
        
        public override Result<TValue> WithReasons(params IReason[] reasons) => 
            (Result<TValue>) base.WithReasons(reasons);
        
        public override Result<TValue> WithError(IError error) => 
            (Result<TValue>) base.WithError(error);

        public override Result<TValue> WithErrors(IEnumerable<IError> errors) =>
            (Result<TValue>) base.WithErrors(errors);
        
        public override Result<TValue> WithErrors(params IError[] errors) => 
            (Result<TValue>) base.WithErrors(errors);


        public Result<TValue> WithValue(TValue value)
        {
            Value = value;
            return this;
        }

        IResult IResult<TValue>.ToResult() => ToResult();

        IResult<TNewValue> IResult<TValue>.
            ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter) =>
            ToResultWithValueConverting(converter);
    }
}