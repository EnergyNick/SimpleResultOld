namespace SimpleResult.Core.Manipulations
{
    /// <summary>
    /// Providing methods for fluent work with <see cref="IResult{TValue}"/>
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