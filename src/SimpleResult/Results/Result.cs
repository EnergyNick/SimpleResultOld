using System;
using System.Collections.Generic;
using System.Linq;
using SimpleResult.Converters;
using SimpleResult.Core;
using SimpleResult.Core.Converters;
using SimpleResult.Core.Manipulations;

namespace SimpleResult
{
    public partial class Result : IResult, IResultManipulationMethods<Result>
    {
        public bool IsSuccess { get; private set; }

        private readonly List<IReason> _reasons;
        public IReadOnlyList<IReason> Reasons
        {
            get => _reasons;
            init => AddReasonsAndUpdateResult(value);
        }
        
        protected ResultConverter Converter;
        protected virtual ResultConverter GetResultConverter() => new(this);
        public virtual ResultConverter Convert => Converter;

        public IReadOnlyList<IError> Errors => Reasons.OfType<IError>().ToList();


        public Result()
        {
            _reasons = new List<IReason>();
            IsSuccess = true;
            
            // That method using only for create typed convertor without using any derived variables for that
            // ReSharper disable once VirtualMemberCallInConstructor
            Converter = GetResultConverter();
        }

        public bool HasReason<TReason>(Func<TReason, bool> predicate = null) where TReason : IReason
        {
            return Reasons.HasReasonOfType(predicate);
        }

        public bool HasError<TError>(Func<TError, bool> predicate = null) where TError : IError
        {
            return Errors.HasErrorOfType(predicate);
        }

        public virtual Result WithReason(IReason reason)
        {
            AddReasonAndUpdateResult(reason);
            return this;
        }

        public virtual Result WithReasons(IEnumerable<IReason> reasons)
        {
            AddReasonsAndUpdateResult(reasons);
            return this;
        }

        public virtual Result WithReasons(params IReason[] reasons)
        {
            AddReasonsAndUpdateResult(reasons);
            return this;
        }
        
        
        public virtual Result WithError(IError error)
        {
            AddReasonAndUpdateResult(error);
            return this;
        }

        public virtual Result WithErrors(IEnumerable<IError> errors)
        {
            AddReasonsAndUpdateResult(errors);
            return this;
        }

        public virtual Result WithErrors(params IError[] errors)
        {
            AddReasonsAndUpdateResult(errors);
            return this;
        }
        
        

        internal void AddReasonAndUpdateResult(IReason newReason)
        {
            _reasons.Add(newReason);
            IsSuccess = IsSuccess && newReason is not IError;
        }
        
        internal void AddReasonsAndUpdateResult(IEnumerable<IReason> newReasons)
        {
            var enumeratedReasons = newReasons as ICollection<IReason> ?? newReasons.ToArray();
            
            _reasons.AddRange(enumeratedReasons);
            IsSuccess = IsSuccess && !enumeratedReasons.OfType<IError>().Any();
        }
        
        IResultConverter IResult.Convert => Converter;
    }
}