using System;
using System.Runtime.Serialization;

namespace InfoSystem.Templates
{
    [Serializable]
    public class RtfTemplateException : Exception
    {
        public RtfTemplateException()
        {
        }

        public RtfTemplateException(string message) : base(message)
        {
        }

        public RtfTemplateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RtfTemplateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}