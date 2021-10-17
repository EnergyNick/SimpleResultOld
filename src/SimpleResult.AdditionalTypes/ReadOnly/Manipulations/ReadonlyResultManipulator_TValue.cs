using System.Collections.Generic;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;

namespace SimpleResult.ReadOnly.Manipulations
{
    public class ReadonlyResultManipulator<TValue> 
        : ReadonlyResultManipulator, IResultManipulationMethods<ReadOnlyResult<TValue>, TValue>
    {
        public ReadonlyResultManipulator(ReadOnlyResult<TValue> target) : base(target)
        { }

        public override ReadOnlyResult<TValue> WithReason(IReason reason) =>
            (ReadOnlyResult<TValue>) base.WithReason(reason);
        
        public override ReadOnlyResult<TValue> WithReasons(IEnumerable<IReason> reasons) => 
            (ReadOnlyResult<TValue>) base.WithReasons(reasons);
        
        public override ReadOnlyResult<TValue> WithReasons(params IReason[] reasons) => 
            (ReadOnlyResult<TValue>) base.WithReasons(reasons);

        public override ReadOnlyResult<TValue> WithError(IError error) =>
            (ReadOnlyResult<TValue>) base.WithError(error);
        
        public override ReadOnlyResult<TValue> WithErrors(IEnumerable<IError> errors) => 
            (ReadOnlyResult<TValue>) base.WithErrors(errors);
        
        public override ReadOnlyResult<TValue> WithErrors(params IError[] errors) => 
            (ReadOnlyResult<TValue>) base.WithErrors(errors);

        public ReadOnlyResult<TValue> WithValue(TValue value)
        {
            var targetAsTValueResult = (ReadOnlyResult<TValue>) Target;
            return targetAsTValueResult with { Value = value };
        }
    }
}