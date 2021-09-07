using System.Collections.Generic;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;
using SimpleResult.Extensions;

namespace SimpleResult.ReadOnly.Manipulations
{
    public class ReadonlyResultManipulator : IResultManipulationMethods<ReadOnlyResult>
    {
        protected readonly ReadOnlyResult Target;

        public ReadonlyResultManipulator(ReadOnlyResult target)
        {
            Target = target;
        }

        public virtual ReadOnlyResult WithReason(IReason reason) => 
            Target with { Reasons = Target.GetResultList().AddNewReason(reason) };
        
        public virtual ReadOnlyResult WithReasons(IEnumerable<IReason> reasons) => 
            Target with { Reasons = Target.GetResultList().AddNewReasons(reasons) };
        
        public virtual ReadOnlyResult WithReasons(params IReason[] reasons) => 
            Target with { Reasons = Target.GetResultList().AddNewReasons(reasons) };
        
        public virtual ReadOnlyResult WithError(IError error) => 
            Target with { Reasons = Target.GetResultList().AddNewReason(error) };
        
        public virtual ReadOnlyResult WithErrors(IEnumerable<IError> errors) => 
            Target with { Reasons = Target.GetResultList().AddNewReasons(errors) };
        
        public virtual ReadOnlyResult WithErrors(params IError[] errors) => 
            Target with { Reasons = Target.GetResultList().AddNewReasons(errors) };
    }
}