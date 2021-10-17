using SimpleResult.ReadOnly;

namespace SimpleResult.Extensions
{
    public static partial class ReadOnlyResultsExtensions
    {
        public static ReadOnlyResult ToReadonlyResult(this Result result)
        {
            return new ReadOnlyResult { Reasons = result.Reasons };
        }
        
        public static ReadOnlyResult<TValue> ToReadonlyResult<TValue>(this Result<TValue> result)
        {
            return new ReadOnlyResult<TValue> { Reasons = result.Reasons, Value = result.ValueOrDefault };
        }
        
        public static Result ToModifiableResult(this ReadOnlyResult result)
        {
            return new Result { Reasons = result.Reasons };
        }
        
        public static Result<TValue> ToModifiableResult<TValue>(this ReadOnlyResult<TValue> result)
        {
            return new Result<TValue> { Reasons = result.Reasons, Value = result.ValueOrDefault };
        }
    }
}