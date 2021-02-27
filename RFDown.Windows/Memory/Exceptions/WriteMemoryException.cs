using System;
using System.Runtime.Serialization;

namespace RFDown.Windows.Memory.Exceptions
{
    public class WriteMemoryException : Exception
    {
        public WriteMemoryException() : base("Failed to write memory")
        {
        }

        public WriteMemoryException(string message) : base(message)
        {
        }

        public WriteMemoryException(IntPtr address, int size) : base(GetMessageFromPtrAndSize(address, size))
        {
        }

        public WriteMemoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public WriteMemoryException(IntPtr address, int size, Exception innerException) : base(GetMessageFromPtrAndSize(address, size), innerException)
        {
        }

        protected WriteMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string GetMessageFromPtrAndSize(IntPtr address, int size) => $"Couldn't write {size} bytes to 0x{address.ToString("X")}";
    }
}
