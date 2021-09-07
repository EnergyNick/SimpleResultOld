using System;
using System.Collections.Generic;
using System.Linq;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;
using SimpleResult.Extensions;
using SimpleResult.ReadOnly.Manipulations;

namespace SimpleResult.ReadOnly
{
    public partial record ReadOnlyResult : IResult
    {
        public bool IsSuccess { get; private init; }

        private readonly List<IReason> _reasons;
        public IReadOnlyList<IReason> Reasons
        {
            get => _reasons;
            init
            {
                if (_reasons.Count == 0)
                {
                    var enumeratedReasons = value as ICollection<IReason> ?? value.ToArray();
                    _reasons.AddRange(enumeratedReasons);
                }
                else
                    _reasons = value.ToList();
                
                IsSuccess = IsSuccess && !Errors.Any();
            }
        }

        protected readonly ReadonlyResultManipulator Manipulator;
        public virtual IResultManipulationMethods<ReadOnlyResult> Copy => Manipulator;
        /// <summary>
        /// Use method for only create correct Manipulator type, don't use type non static fields 
        /// </summary>
        protected virtual ReadonlyResultManipulator GetResultManipulator() => new(this);


        public IReadOnlyList<IError> Errors => Reasons.OfType<IError>().ToList();

        public ReadOnlyResult()
        {
            _reasons = new List<IReason>();
            IsSuccess = true;
            
            Manipulator = GetResultManipulator();
        }

        // Copy constructor
        public ReadOnlyResult(ReadOnlyResult original)
        {
            _reasons = new List<IReason>(original._reasons);
            IsSuccess = original.IsSuccess;

            Manipulator = GetResultManipulator();
        }
        
        public ReadOnlyResult(IResult original)
        {
            _reasons = new List<IReason>(original.Reasons);
            IsSuccess = original.IsSuccess;

            Manipulator = GetResultManipulator();
        }
        
        public ReadOnlyResult<TNewValue> ToResult<TNewValue>(TNewValue value = default)
        {
            return new ReadOnlyResult<TNewValue> { Reasons = _reasons, Value = value };
        }

        public bool HasReason<TReason>(Func<TReason, bool> predicate = null) where TReason : IReason
        {
            return Reasons.HasReasonOfType(predicate);
        }

        public bool HasError<TError>(Func<TError, bool> predicate = null) where TError : IError
        {
            return Errors.HasErrorOfType(predicate);
        }

        internal List<IReason> GetResultList() => _reasons;

        IResult<TNewValue> IResult.ToResult<TNewValue>(TNewValue value) => ToResult(value);
    }
}