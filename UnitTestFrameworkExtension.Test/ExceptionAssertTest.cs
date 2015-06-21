using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestFrameworkExtension.Test
{
    [TestClass]
    public class ExceptionAssertTest
    {
        [TestMethod]
        public void TestThrows_PositiveBase()
        {
            object obj = null;

            var ex = ExceptionAssert.Throws<NullReferenceException>(() => { obj.ToString(); });
            Assert.IsNotNull(ex);
            Assert.IsInstanceOfType(ex, typeof(NullReferenceException));
        }

        [TestMethod]
        public void TestThrows_actionToEvaluateNullArgument()
        {

            try
            {
                ExceptionAssert.Throws<Exception>(null);
                Assert.Fail("ArgumentNullException not thrown for actionToEvaluate parameter with null value");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
                Assert.AreEqual((ex as ArgumentNullException).ParamName, "actionToEvaluate");
            }

        }

        [TestMethod]
        public void TestThrows_IsExceptionValidNullArgument()
        {
            InsufficientMemoryException testEx = new InsufficientMemoryException();
            // should not throw any exception here since functor IsExceptionValid is optional and therefore null is acceptable where no further custom exception validations will take place
            var result = ExceptionAssert.Throws<InsufficientMemoryException>(() => { throw testEx; }, null);

            Assert.IsNotNull(result);
            Assert.AreSame(testEx, result);
        }

        [TestMethod]
        public void TestThrows_VerifyIsExceptionValidArgument()
        {
            string[] testArray = new string[2];

            ExceptionAssert.Throws<IndexOutOfRangeException>(() =>
            {
                testArray[2] = "index of 2 is too big";
            },
            (ex) => string.Equals(ex.Message, "Index was outside the bounds of the array."));
        }
    }
}
