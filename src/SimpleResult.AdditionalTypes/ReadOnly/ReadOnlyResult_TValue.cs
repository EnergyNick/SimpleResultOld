using System;
using SimpleResult.Core;
using SimpleResult.ReadOnly.Manipulations;

namespace SimpleResult.ReadOnly
{
    public record ReadOnlyResult<TValue> : ReadOnlyResult, IResult<TValue>
    {
        private readonly TValue _value;
        public TValue ValueOrDefault => _value;

        public TValue Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Result is in status failed. Value is not set.");

                return _value;
            }
            init
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Result is in status failed. Value is not set.");

                _value = value;
            }
        }
        
        protected override ReadonlyResultManipulator CreateResultManipulator() =>
            new ReadonlyResultManipulator<TValue>(this);
        public override ReadonlyResultManipulator<TValue> Copy => (ReadonlyResultManipulator<TValue>) Manipulator;

        public ReadOnlyResult()
        {
            _value = default;
        }

        // Copy constructor
        public ReadOnlyResult(ReadOnlyResult<TValue> original) : base(original)
        {
            _value = original._value;
        }
        
        public ReadOnlyResult(IResult<TValue> original) : base(original)
        {
            _value = original.ValueOrDefault;
        }

        public ReadOnlyResult ToResult() => new() { Reasons = Reasons };

        public ReadOnlyResult<TNewValue> ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter)
        {
            if (IsSuccess && converter == null)
                throw new ArgumentNullException(nameof(converter));
            
            return new ReadOnlyResult<TNewValue>
            {
                Reasons = Reasons, 
                Value = IsSuccess ? converter(ValueOrDefault) : default
            };
        }
        
        
        IResult IResult<TValue>.ToResult() => ToResult();
        
        IResult<TNewValue> IResult<TValue>.
            ToResultWithValueConverting<TNewValue>(Func<TValue, TNewValue> converter) =>
            ToResultWithValueConverting(converter);
    }
}