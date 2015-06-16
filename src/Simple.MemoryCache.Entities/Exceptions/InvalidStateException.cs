using System;

namespace Simple.MemoryCache.Entities.Exceptions
{
    public class InvalidStateException : ApplicationException
    {
        public InvalidStateException() { }
        public InvalidStateException(string message) : base(message) { }
        public InvalidStateException(string message, Exception innerException) : base(message, innerException) { }
    }
}