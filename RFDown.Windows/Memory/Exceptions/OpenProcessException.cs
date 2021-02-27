using System;
using System.Runtime.Serialization;

namespace RFDown.Windows.Memory.Exceptions
{
    public class OpenProcessException : Exception
    {
        public OpenProcessException() : base("Failed to open process")
        {
        }

        public OpenProcessException(string message) : base(message)
        {
        }

        public OpenProcessException(int pid) : base(GetMessageForProcess(pid))
        {
        }

        public OpenProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OpenProcessException(int pid, Exception innerException) : base(GetMessageForProcess(pid), innerException)
        {
        }

        protected OpenProcessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string GetMessageForProcess(int pid) => $"Couldn't open process: {pid}";
    }
}
