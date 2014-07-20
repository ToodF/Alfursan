using System;

namespace Alfursan.Infrastructure.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException()
        {
        }
        public NotFoundUserException(string message)
            : base(message)
        {
        }
        public NotFoundUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
