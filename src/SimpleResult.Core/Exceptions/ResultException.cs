﻿using System;

namespace SimpleResult.Exceptions
{
    /// <summary>
    /// Base exception for all incorrect operations with result
    /// </summary>
    public class ResultException : Exception
    {
        /// <inheritdoc />
        public ResultException()
        { }

        /// <inheritdoc />
        public ResultException(string message) : base(message)
        { }

        /// <inheritdoc />
        public ResultException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}