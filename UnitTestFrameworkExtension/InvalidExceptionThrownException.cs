using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFrameworkExtension
{
    /// <summary>
    /// Exception thrown when the exception thrown has been validated to be incorrect
    /// <remarks>See exceptionThrown property for the invalidity reason</remarks>
    /// </summary>
    public class InvalidExceptionThrownException : ExceptionAssertExceptionBase
    {
        public Exception exceptionThrown { get; internal set; }

        public ExceptionInvalidReason InvalidReason;
    }


    public enum ExceptionInvalidReason : short
    {
        /// <summary>
        /// Custom validation on the exception has deemed the thrown exception to be invalid
        /// </summary>
        ExceptionValidationFailed = 0,

        /// <summary>
        /// type of expected and actual exception thrown is not matching
        /// </summary>
        ExceptionTypeMismatch,
    }
}
