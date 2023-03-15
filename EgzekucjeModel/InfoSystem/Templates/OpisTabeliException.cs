using System;
using System.Runtime.Serialization;

namespace InfoSystem.Templates
{
    public class OpisTabeliException : Exception
    {
        public OpisTabeliException()
        {
        }

        public OpisTabeliException(string message) : base(message)
        {
        }

        public OpisTabeliException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OpisTabeliException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
