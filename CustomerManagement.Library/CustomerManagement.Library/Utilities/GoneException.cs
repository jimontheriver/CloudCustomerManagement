using System;
using System.CodeDom.Compiler;

namespace CustomerManagement.Library.Utilities
{

    /// <summary>
    ///  An exception thrown when the item being acted upon no longer exists but once did.
    /// </summary>
    [GeneratedCode("Microsoft Visual Studio Professional 2019", "16.2.5")]
    [Serializable]
    public class GoneException : Exception
    {
        public GoneException() { }
        public GoneException(string message) : base(message) { }
        public GoneException(string message, Exception inner) : base(message, inner) { }
        protected GoneException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
