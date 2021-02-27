using System;
using System.Runtime.Serialization;

namespace RFDown.Windows.Memory.Exceptions
{
    public class ReadMemoryException : Exception
    {
        public ReadMemoryException() : base("Failed to read memory")
        {
        }

        public ReadMemoryException(string message) : base(message)
        {
        }

        public ReadMemoryException(IntPtr address, int size) : base(GetMessageFromPtrAndSize(address, size))
        {
        }

        public ReadMemoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ReadMemoryException(IntPtr address, int size, Exception innerException) : base(GetMessageFromPtrAndSize(address, size), innerException)
        {
        }

        protected ReadMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string GetMessageFromPtrAndSize(IntPtr address, int size) => $"Couldn't read {size} bytes from 0x{address:X}.";
    }
}
