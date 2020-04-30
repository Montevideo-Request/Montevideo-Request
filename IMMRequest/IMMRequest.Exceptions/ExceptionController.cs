using System;

namespace IMMRequest.Exceptions
{
   public class ExceptionController: Exception
    {
        public ExceptionController(): base(){ }

        public ExceptionController(string message) : base(message) { }

        public ExceptionController(string message, Exception innerException)
            : base(message, innerException){ }
    }
}