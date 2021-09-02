using SimpleResult.Core;
using SimpleResult.Core.Converters;

namespace SimpleResult.Converters
{
    public class ResultConverter : IResultConverter
    {
        protected readonly Result Target;
        
        internal ResultConverter(Result target)
        {
            Target = target;
        }

        public Result<TNewValue> ToResultWithValue<TNewValue>(TNewValue value = default)
        {
            return new Result<TNewValue> { Reasons = Target.Reasons, Value = value };
        }
        
        IResult<TNewValue> IResultConverter.ToResultWithValue<TNewValue>(TNewValue value) => ToResultWithValue(value);
    }
}