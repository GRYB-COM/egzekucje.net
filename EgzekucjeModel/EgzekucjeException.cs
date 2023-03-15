using System;
using System.Runtime.Serialization;

namespace Egzekucje.NET
{
    [Serializable]
    public class EgzekucjeException : Exception
    {
        public EgzekucjeException()
        {
        }

        public EgzekucjeException(string message) : base(message)
        {
        }

        public EgzekucjeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EgzekucjeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}