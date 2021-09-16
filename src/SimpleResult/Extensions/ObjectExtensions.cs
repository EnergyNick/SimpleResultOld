namespace SimpleResult.Extensions
{
    public static class ObjectExtensions
    {
        public static Result<TValue> ToResult<TValue>(this TValue value)
        {
            return value is Result result 
                ? result.ToResult(value) 
                : Result.Ok(value);
        }
    }
}