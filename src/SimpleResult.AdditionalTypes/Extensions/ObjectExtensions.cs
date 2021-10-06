using SimpleResult.ReadOnly;

namespace SimpleResult.Extensions
{
    public static class ObjectReadOnlyExtensions
    {
        public static ReadOnlyResult<TValue> ToReadOnlyResult<TValue>(this TValue value)
        {
            return value is Result result 
                ? new ReadOnlyResult<TValue> {Reasons = result.Reasons, Value = value}
                : ReadOnlyResult.Success(value);
        }
    }
}