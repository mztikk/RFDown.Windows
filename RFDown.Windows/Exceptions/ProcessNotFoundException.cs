using System;
using System.Runtime.Serialization;

namespace RFDown.Windows
{
    [Serializable]
    public class ProcessNotFoundException : Exception
    {
        public ProcessNotFoundException()
        {
        }

        public ProcessNotFoundException(string procName) : base(GetMessageForProcessName(procName))
        {
        }

        public ProcessNotFoundException(int procId) : base(GetMessageForProcessID(procId))
        {
        }

        public ProcessNotFoundException(string procName, Exception innerException) : base(GetMessageForProcessName(procName), innerException)
        {
        }

        protected ProcessNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string GetMessageForProcessName(string procName) => $"Couldn't find a Process with Name: \"{procName}\"";
        private static string GetMessageForProcessID(int procID) => $"Couldn't find a Process with ID: \"{procID}\"";
    }
}
