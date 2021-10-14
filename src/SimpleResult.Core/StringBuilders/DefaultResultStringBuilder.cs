using System;
using System.Text;
using SimpleResult.Core;

namespace SimpleResult.StringBuilders
{
    /// <summary>
    /// Provide base serialization for any <see cref="IResult"/> and <see cref="IResult{TValue}"/> types.
    /// Can be customized by overriding virtual methods.
    /// </summary>
    public class DefaultResultStringBuilder : IResultStringBuilder
    {
        public string ConvertToString<TResult>(TResult result)
            where TResult : IResult
        {
            return CreateStringRepresentation(result, null);
        }
        
        public string ConvertToStringWithValue<TResult, TValue>(TResult result)
            where TResult : IResult<TValue>
        {
            return CreateStringRepresentation(result, AddValueForResult<TResult, TValue>);
        }

        protected virtual string CreateStringRepresentation<TResult>(TResult result, 
            Func<TResult, StringBuilder, bool> valueMember)
            where TResult : IResult
        {
            var builder = new StringBuilder();
            builder.Append($"{GetNameOfResultType<TResult>()}[{result.IsSuccess}]: ");

            builder.Append("{");

            var isAddedValueMember = valueMember?.Invoke(result, builder) ?? false;
            var previousStartIndex = builder.Length;
            
            if (result.Reasons.Count != 0)
            {
                PrintReasons(result, builder);
                if(isAddedValueMember)
                    builder.Insert(previousStartIndex, ", ");
            }
            
            previousStartIndex = builder.Length;
            if(PrintAdditionalInfo(result, builder))
                builder.Insert(previousStartIndex, ", ");
            
            builder.Append("}");
            return builder.ToString();
        }

        /// <summary>
        /// Method for printing value of typed <see cref="IResult{TValue}"/>
        /// </summary>
        /// <returns>Is added any info</returns>
        protected virtual bool AddValueForResult<TResult, TValue>(TResult result, StringBuilder builder) 
            where TResult : IResult<TValue>
        {
            var isSuccess = result.IsSuccess;
            if (isSuccess)
                builder.Append($"Value={result.ValueOrDefault.ToString()}");
            return isSuccess;
        }
        
        protected virtual void PrintReasons<TResult>(TResult result, StringBuilder builder) 
            where TResult : IResult
        {
            builder.Append("Reasons={");
            
            var count = result.Reasons.Count;
            for (var i = 0; i < count; i++)
            {
                builder.Append(result.Reasons[i]);
                if (i != count - 2)
                    builder.Append(", ");
            }
            builder.Append("}");
        }

        /// <summary>
        /// Provide for info printing of custom Result.
        /// </summary>
        /// <returns>Is added any info</returns>
        protected virtual bool PrintAdditionalInfo<TResult>(TResult result, StringBuilder builder)
            where TResult : IResult
        {
            return false;
        }

        /// <summary>
        /// Return name of current serializing type.
        /// </summary>
        protected virtual string GetNameOfResultType<TResult>() where TResult : IResult
        {
            return "Result";
        }
    }
}