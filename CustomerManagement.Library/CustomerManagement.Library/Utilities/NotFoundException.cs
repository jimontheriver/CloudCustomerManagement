using System;
using System.CodeDom.Compiler;

namespace CustomerManagement.Library.Utilities
{
    /// <summary>
    /// An exception thrown when the item being acted on doesn't exist.
    /// </summary>
    [GeneratedCode("Microsoft Visual Studio Professional 2019", "16.2.5")]
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
