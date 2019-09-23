using System;
using System.CodeDom.Compiler;

namespace CustomerManagement.Library.Utilities
{
    /// <summary>
    /// An exception thrown when the item being acted upon is in a state that is inconsisted with the operation being performed. For example: trying to act on an older version of an item.
    /// </summary>
    [GeneratedCode("Microsoft Visual Studio Professional 2019", "16.2.5")]
    [Serializable]
    public class ConflictException : Exception
    {
        public ConflictException() { }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, Exception inner) : base(message, inner) { }
        protected ConflictException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
