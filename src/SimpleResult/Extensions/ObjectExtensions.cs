﻿namespace SimpleResult
{
    public static class ObjectExtensions
    {
        public static Result<TValue> AsResult<TValue>(this TValue value)
        {
            if (value is Result result)
                return result.Convert.ToResultWithValue(value);
            
            return new Result<TValue>()
                .WithValue(value);
        }
    }
}