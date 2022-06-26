namespace SimpleResult.Core.Manipulations
{
    /// <summary>
    /// Providing methods of fluent api for <see cref="IResult{TValue}"/>
    /// </summary>
    public interface IResultManipulationMethods<out TResult, in TValue> : IResultManipulationMethods<TResult>
        where TResult : IResult<TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        TResult WithValue(TValue value);
    }
}