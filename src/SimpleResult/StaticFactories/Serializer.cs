using System;
using System.Text;

namespace SimpleResult
{
    internal static class Serializer
    {
        public static string BuildStringRepresentation(object currentObj, 
            Action<System.Text.StringBuilder> appendAdditionalFields)
        {
            var builder = new StringBuilder();
            builder.Append($"{currentObj.GetType().Name}: {{");

            appendAdditionalFields(builder);

            builder.Append("}");
            return builder.ToString();
        }
    }
}