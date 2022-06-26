using System.Collections.Generic;

namespace SimpleResult.Core.Manipulations
{
    /// <summary>
    /// Providing methods of fluent api for <see cref="IResult"/>
    /// </summary>
    public interface IConclusionManipulationMethods<out TConclusion>
        where TConclusion : IConclusion
    {
        /// <summary>
        /// Return copy of <see cref="TConclusion"/> with appended <see cref="IReason"/> and updated status
        /// </summary>
        /// <returns>Copy of current <see cref="TConclusion"/> with new reason</returns>
        TConclusion WithReason(IReason reason);
        /// <summary>
        /// Return copy of <see cref="TConclusion"/> with appended <see cref="IReason"/>-s and updated status
        /// </summary>
        /// <returns>Copy of current <see cref="TConclusion"/> with new reasons</returns>
        TConclusion WithReasons(IEnumerable<IReason> reasons);
        /// <summary>
        /// Return copy of <see cref="TConclusion"/> with appended <see cref="IReason"/>-s and updated status
        /// </summary>
        /// <returns>Copy of current <see cref="TConclusion"/> with new reasons</returns>
        TConclusion WithReasons(params IReason[] reasons);

        /// <summary>
        /// Return copy of <see cref="TConclusion"/> with appended <see cref="IError"/> and failed status
        /// </summary>
        /// <returns>Copy of current <see cref="TConclusion"/> with new error</returns>
        TConclusion WithError(IError error);
        /// <summary>
        /// Return copy of <see cref="TConclusion"/> with appended <see cref="IError"/>-s and failed status
        /// </summary>
        /// <returns>Copy of current <see cref="TConclusion"/> with new errors</returns>
        TConclusion WithErrors(IEnumerable<IError> errors);
        /// <summary>
        /// Return copy of <see cref="TConclusion"/> with appended <see cref="IError"/>-s and failed status
        /// </summary>
        /// <returns>Copy of current <see cref="TConclusion"/> with new errors</returns>
        TConclusion WithErrors(params IError[] errors);
    }
}