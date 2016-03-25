using System;
using FluentAssertions;
using NUnit.Framework;
using SimpleMemoryCache.Entities.Exceptions;

namespace SimpleMemoryCache.UnitTest.Exceptions
{
    [TestFixture]
    public class InvalidStateExceptionTests
    {
        private const string CustomErrorMessage = "custom error message - 123";

        [Test]
        public void InvalidStateException_WhenThrowAnInvalidStateException_ShouldReturnAnApplicationException()
        {
            Action action = () => { throw new InvalidStateException(CustomErrorMessage); };

            action.ShouldThrow<ApplicationException>();
        }

        [Test]
        public void InvalidStateException_WhenThrowAnInvalidStateExceptionWithACustomErrorMessage_ShouldReturnExpectedCustomErrorMessage()
        {
            Action action = () => { throw new InvalidStateException(CustomErrorMessage); };

            action
                .ShouldThrow<InvalidStateException>()
                .Which
                .Message
                .Should()
                .Be(CustomErrorMessage);
        }

        [Test]
        public void InvalidStateException_WhenThrowAnInvalidStateExceptionWithAnInnerException_ShouldReturnExpectedInnerException()
        {
            var exception = new Exception("message");

            Action action = () => { throw new InvalidStateException(CustomErrorMessage, exception); };

            action
                .ShouldThrow<InvalidStateException>()
                .Which
                .InnerException
                .Should()
                .Be(exception);
        }
    }
}