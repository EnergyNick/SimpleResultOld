using System.Collections.Generic;

namespace SimpleResult.Core.Manipulations
{
    /// <summary>
    /// Providing methods of fluent api for <see cref="IResult"/>
    /// </summary>
    public interface IResultManipulationMethods<out TResult>
        where TResult : IConclusion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        TResult WithReason(IReason reason);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reasons"></param>
        /// <returns></returns>
        TResult WithReasons(IEnumerable<IReason> reasons);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reasons"></param>
        /// <returns></returns>
        TResult WithReasons(params IReason[] reasons);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        TResult WithError(IError error);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        TResult WithErrors(IEnumerable<IError> errors);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        TResult WithErrors(params IError[] errors);
    }
}