using System;
using FluentAssertions;
using NUnit.Framework;
using Simple.MemoryCache.Entities.Exceptions;

namespace Simple.MemoryCache.UnitTest.Exceptions
{
    [TestFixture]
    public class InvalidStateExceptionTests
    {
        private const string CustomErrorMessage = "custom error message - 123";

        [Test]
        public void InvalidStateException_WhenThrowAnInvalidStateException_ShouldReturnAnApplicationException()
        {
            try
            {
                throw new InvalidStateException();
            }
            catch (InvalidStateException invalidStateException)
            {
                invalidStateException.Should().BeAssignableTo<ApplicationException>();
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidStateException), ExpectedMessage = CustomErrorMessage)]
        public void InvalidStateException_WhenThrowAnInvalidStateExceptionWithACustomErrorMessage_ShouldReturnExpectedCustomErrorMessage()
        {
            throw new InvalidStateException(CustomErrorMessage);
        }

        [Test]
        public void InvalidStateException_WhenThrowAnInvalidStateExceptionWithAnInnerException_ShouldReturnExpectedInnerException()
        {
            try
            {
                var four = 4;
                var zero = 0;

                var result = four / zero;
            }
            catch (DivideByZeroException divideByZeroException)
            {
                try
                {
                    throw new InvalidStateException(CustomErrorMessage, divideByZeroException);
                }
                catch (InvalidStateException invalidStateException)
                {
                    invalidStateException.InnerException.Should().Be(divideByZeroException);
                }
            }
        }
    }
}