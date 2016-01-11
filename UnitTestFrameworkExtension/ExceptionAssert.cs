using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFrameworkExtension
{
    public static class ExceptionAssert
    {
        /// <summary>
        /// To evaluate that an expected exception of type T is to be thrown by the given actionToEvaluate action parameter
        /// Custom verify that the exception thrown is valid via IsExceptionValid functor which returns true or false based on the given thrown exception passed to it
        /// <remarks>To use this, wrap the line or a small block of codes to be evaluated into a parameterless in-line functor and passed into this method as actionToEvaluate parameter</remarks>
        /// </summary>
        /// <typeparam name="T">Expected exception thrown</typeparam>
        /// <param name="actionToEvaluate">Action parameter to be evaluated for the expected exception</param>
        /// <param name="IsExceptionValid">optional functor that receives the thrown exception instance and determine that the thrown exception instance is valid or not. To return true if valid otherwise false</param>
        /// <returns>Returns the valid thrown exception instance when the expected exception is thrown as expected and has been validated to be ok</returns>
        /// <exception cref="System.ArgumentNullException">When actionToEvaluate is null</exception>
        /// <exception cref="UnitTestFrameworkExtension.NoExceptionThrownException">When no exception has been thrown by the actionToEvaluate</exception>
        /// <exception cref="UnitTestFrameworkExtension.InvalidExceptionThrownException">When exception thrown by the actionToEvaluate fails the IsExceptionValid functor validation. The InvalidReason property will be set to ExceptionInvalidReason.ExceptionValidationFailed</exception>
        /// <exception cref="UnitTestFrameworkExtension.InvalidExceptionThrownException">When exception thrown by the actionToEvaluate is not of the specified type T. The InvalidReason property will be set to ExceptionInvalidReason.ExceptionTypeMismatch</exception>
        public static T Throws<T>(Action actionToEvaluate, Func<T, bool> IsExceptionValid = null) where T : Exception
        {
            if (actionToEvaluate == null) throw new ArgumentNullException("actionToEvaluate", "Specify the operations or actions to be performed that are expected to the exception of type <T>");

            ExceptionAssertExceptionBase UnitTestException = null;

            try
            {
                actionToEvaluate();

                UnitTestException = new NoExceptionThrownException();
            }
            catch (T expectedEx)
            {
                // verify the thrown exception expectedEx with Functor validator if exists
                // If functor validator returns false, fails the assertion by throwing InvalidExceptionThrownException
                if (IsExceptionValid != null && !IsExceptionValid(expectedEx))
                    UnitTestException = new InvalidExceptionThrownException() { exceptionThrown = expectedEx, InvalidReason = ExceptionInvalidReason.ExceptionValidationFailed };

                return expectedEx;
            }
            catch (Exception otherEx)
            {
                // catch all as an unexpected exception is encountered
                UnitTestException = new InvalidExceptionThrownException() { exceptionThrown = otherEx, InvalidReason = ExceptionInvalidReason.ExceptionTypeMismatch };
            }

            throw UnitTestException;
        }
    }
}
