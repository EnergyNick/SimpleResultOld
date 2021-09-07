﻿using System;
using System.Collections.Generic;
using System.Linq;
using SimpleResult.Core;
using SimpleResult.Core.Manipulations;
using SimpleResult.Extensions;

namespace SimpleResult
{
    public partial class Result : IResult, IResultManipulationMethods<Result>
    {
        public bool IsSuccess { get; private set; }

        private readonly List<IReason> _reasons;
        public IReadOnlyList<IReason> Reasons
        {
            get => _reasons;
#if NET5_0_OR_GREATER
            init => AddReasonsAndUpdateResult(value);
#endif
        }

        public IReadOnlyList<IError> Errors => Reasons.OfType<IError>().ToList();
        
        public Result()
        {
            _reasons = new List<IReason>();
            IsSuccess = true;
        }

        public bool HasReason<TReason>(Func<TReason, bool> predicate = null) where TReason : IReason
        {
            return Reasons.HasReasonOfType(predicate);
        }

        public bool HasError<TError>(Func<TError, bool> predicate = null) where TError : IError
        {
            return Errors.HasErrorOfType(predicate);
        }
        
        public virtual Result<TNewValue> ToResult<TNewValue>(TNewValue value = default)
        {
#if NET5_0_OR_GREATER
            return new Result<TNewValue> { Reasons = _reasons, Value = value };
#else
            return new Result<TNewValue>().WithReasons(_reasons).WithValue(value);
#endif
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
            IsSuccess = IsSuccess && !(newReason is IError);
        }
        
        internal void AddReasonsAndUpdateResult(IEnumerable<IReason> newReasons)
        {
            var enumeratedReasons = newReasons as ICollection<IReason> ?? newReasons.ToArray();
            
            _reasons.AddRange(enumeratedReasons);
            IsSuccess = IsSuccess && !enumeratedReasons.OfType<IError>().Any();
        }
        
        IResult<TNewValue> IResult.ToResult<TNewValue>(TNewValue value) => ToResult(value);
    }
}