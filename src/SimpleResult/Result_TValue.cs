using System;
using System.Collections.Generic;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;
using SimpleResult.Exceptions;
using SimpleResult.Settings;
using SimpleResult.StringBuilders;

namespace SimpleResult
{
    public class Result<TValue> : Result, IResult<TValue>, IResultManipulationMethods<Result<TValue>, TValue>
    {
        private TValue _value;

        public TValue ValueOrDefault => _value;

        public TValue Value
        {
            get
            {
                if (!IsSuccess)
                    throw new OperationOnFailedResultException("Get value");

                return _value;
            }
            set
            {
                if (!IsSuccess)
                    throw new OperationOnFailedResultException("Set value");

                _value = value;
            }
        }

        public Result(TValue value = default)
        {
            _value = value;
        }
        
        public Result ToResult() => new Result().WithReasons(Reasons);

        public Result<TNewValue> ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter)
        {
            if (IsSuccess && converter == null)
                throw new ArgumentNullException(nameof(converter));
            
            return new Result<TNewValue>()
                .WithReasons(Reasons)
                .WithValue(IsSuccess ? converter(_value) : default);
        }

#if NET5_0_OR_GREATER
        
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

#else
        
        public new Result<TValue> WithReason(IReason reason) => 
            (Result<TValue>) base.WithReason(reason);
        
        public new Result<TValue> WithReasons(IEnumerable<IReason> reasons) => 
            (Result<TValue>) base.WithReasons(reasons);
        
        public new Result<TValue> WithReasons(params IReason[] reasons) => 
            (Result<TValue>) base.WithReasons(reasons);
        
        public new Result<TValue> WithError(IError error) => 
            (Result<TValue>) base.WithError(error);

        public new Result<TValue> WithErrors(IEnumerable<IError> errors) =>
            (Result<TValue>) base.WithErrors(errors);
        
        public new Result<TValue> WithErrors(params IError[] errors) => 
            (Result<TValue>) base.WithErrors(errors);

#endif
        public Result<TValue> WithValue(TValue value)
        {
            Value = value;
            return this;
        }

        public override string ToString() =>
            DefaultStringBuilder.ConvertToStringWithValue<Result<TValue>, TValue>(this);


        IResult IResult<TValue>.ToResult() => ToResult();

        IResult<TNewValue> IResult<TValue>.
            ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter) =>
            ToResultWithValueConverting(converter);
    }
}