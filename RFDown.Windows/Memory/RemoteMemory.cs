using System;
using System.Diagnostics;
using System.Text;
using RFDown.Windows.Memory.Exceptions;
using RFDown.Windows.Native;
using RFDown.Windows.Native.Enums;

namespace RFDown.Windows.Memory
{
    /// <summary>
    /// Implementation of <see cref="IRemoteMemory"/>
    /// </summary>
    public unsafe class RemoteMemory : IRemoteMemory
    {
        /// <summary>
        /// If our process is 64Bit or not.
        /// </summary>
        public static readonly bool Self64 = IntPtr.Size == 8;

        /// <summary>
        /// If our and the process we attached to is 64Bit.
        /// </summary>
        public readonly bool Both64;

        /// <summary>
        /// If the process we attached to is 64Bit or not.
        /// </summary>
        public readonly bool Is64;

        /// <summary>
        /// The native process we're interacting with.
        /// </summary>
        public Process NativeProcess { get; }

        /// <summary>
        /// Handle to the process.
        /// </summary>
        public Handle ProcessHandle { get; }

        /// <summary>
        /// Main module of the process.
        /// </summary>
        public ProcessModule MainModule => NativeProcess.MainModule;

        /// <summary>
        /// Initializes a new instance of <see cref="RemoteMemory"/> for a specified <see cref="Process"/>.
        /// </summary>
        /// <param name="proc"><see cref="Process"/> to open.</param>
        public RemoteMemory(Process proc)
        {
            if (!ProcessHelpers.ProcessExists(proc))
            {
                throw new ArgumentException("Process not valid");
            }

            NativeProcess = proc;
            ProcessHandle = ProcessHelpers.OpenProcess(ProcessAccessFlags.AllAccess, NativeProcess.Id);
            Is64 = ProcessHelpers.Is64Bit(NativeProcess);
            Both64 = Is64 && Self64;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RemoteMemory"/> for a specific Process ID.
        /// </summary>
        /// <param name="processID">Process ID to open the <see cref="Process"/> from.</param>
        public RemoteMemory(int processID) : this(Process.GetProcessById(processID)) { }

        /// <summary>
        /// Initializes a new instance of <see cref="RemoteMemory"/> with a process name.
        /// </summary>
        /// <param name="processName">Process name to get the <see cref="Process"/>es for.</param>
        /// <param name="index">Index to use if there are multpiple <see cref="Process"/>es of the same name, default 0 to use first one.</param>
        public RemoteMemory(string processName, int index = 0) : this(ProcessHelpers.GetProcessByName(processName, index)) { }

        /// <summary>
        /// Initializes a new instance of <see cref="RemoteMemory"/> with a process name.
        /// </summary>
        /// <param name="processName">Process name to get the <see cref="Process"/>es for.</param>
        /// <param name="processSelector"><see cref="Func{T, TResult}"/> will be called on every <see cref="Process"/> and returns the <see cref="Process"/> on which this <see cref="Func{T, TResult}"/> returns <see langword="true"/> for</param>
        public RemoteMemory(string processName, Func<Process, bool> processSelector) : this(ProcessHelpers.GetProcessByName(processName, processSelector)) { }

        /// <inheritdoc />
        public T Read<T>(IntPtr address, bool relative = false) where T : unmanaged
        {
            int size;
            if (typeof(T) == typeof(IntPtr))
            {
                size = Both64 ? 8 : 4;
            }
            else
            {
                size = sizeof(T);
            }

            byte[] bytes = ReadBytes(address, size, relative);
            fixed (void* bp = bytes)
            {
                return *(T*)bp;
            }
        }

        /// <inheritdoc />
        public T[] Read<T>(IntPtr address, int count, bool relative = false) where T : unmanaged
        {
            int size;
            if (typeof(T) == typeof(IntPtr))
            {
                size = Both64 ? 8 : 4;
            }
            else
            {
                size = sizeof(T);
            }

            size *= count;

            byte[] bytes = ReadBytes(address, size, relative);
            T[] result = new T[count];

            fixed (void* bp = bytes, rp = result)
            {
                Buffer.MemoryCopy(bp, rp, size, size);
            }

            return result;
        }

        /// <inheritdoc />
        public void Write<T>(IntPtr address, T value, bool relative = false) where T : unmanaged
        {
            int size;
            if (typeof(T) == typeof(IntPtr))
            {
                size = Is64 ? 8 : 4;
            }
            else
            {
                size = sizeof(T);
            }

            WriteBytes(address, &value, size, relative);
        }

        /// <inheritdoc />
        public void Write<T>(IntPtr address, T[] values, bool relative = false) where T : unmanaged
        {
            Type type = typeof(T);
            int size;
            if (type == typeof(IntPtr))
            {
                size = Is64 ? 8 : 4;
            }
            else
            {
                size = sizeof(T);
            }

            fixed (void* vp = values)
            {
                WriteBytes(address, vp, values.Length * size, relative);
            }
        }

        /// <inheritdoc />
        public string ReadString(IntPtr address, Encoding encoding, int maxLength = 512, bool relative = false)
        {
            byte[] bytes = ReadBytes(address, maxLength, relative);
            string data = encoding.GetString(bytes);
            int eosPos = data.IndexOf('\0');

            return eosPos == -1 ? data : data.Substring(0, eosPos);
        }

        /// <inheritdoc />
        public string ReadString(IntPtr address, int maxLength = 512, bool relative = false) => ReadString(address, Encoding.UTF8, maxLength, relative);

        /// <inheritdoc />
        public void WriteString(IntPtr address, string text, Encoding encoding, bool relative = false) => WriteBytes(address, encoding.GetBytes(text + '\0'), relative);

        /// <inheritdoc />
        public void WriteString(IntPtr address, string text, bool relative = false) => WriteString(address, text, Encoding.UTF8, relative);

        /// <inheritdoc />
        public byte[] ReadBytes(IntPtr address, int count, bool relative = false) => ReadBytes(ProcessHandle, relative ? ToAbsolute(address) : address, count);

        /// <inheritdoc />
        public void WriteBytes(IntPtr address, byte[] bytes, bool relative = false) => WriteBytes(ProcessHandle, relative ? ToAbsolute(address) : address, bytes);

        private void WriteBytes(IntPtr address, void* bytes, int numOfBytes, bool relative = false) => WriteBytes(ProcessHandle, relative ? ToAbsolute(address) : address, bytes, numOfBytes);

        private static byte[] ReadBytes(Handle procHandle, IntPtr address, int size)
        {
            if (!procHandle.IsInvalid && !procHandle.IsClosed && address != IntPtr.Zero)
            {
                byte[] buffer = new byte[size];
                if (Kernel32.ReadProcessMemory(procHandle, address, buffer, size, out int numBytesRead)
                    && size == numBytesRead)
                {
                    return buffer;
                }
            }

            throw new ReadMemoryException(address, size);
            //throw new Exception($"Couldn't read {size} bytes from 0x{address.ToString("X")}.");
        }

        private static void WriteBytes(Handle procHandle, IntPtr address, byte[] bytes)
        {
            if (procHandle.IsInvalid || procHandle.IsClosed || address == IntPtr.Zero)
            {
                throw new ArgumentException("Handle invalid/closed or address is zero");
            }

            bool result = Kernel32.WriteProcessMemory(procHandle, address, bytes, bytes.Length, out int numBytesWritten);

            if (!result || numBytesWritten != bytes.Length)
            {
                throw new WriteMemoryException(address, bytes.Length);
            }
        }

        private static void WriteBytes(Handle procHandle, IntPtr address, void* bytes, int numOfBytesToWrite)
        {
            if (procHandle.IsInvalid || procHandle.IsClosed || address == IntPtr.Zero)
            {
                throw new ArgumentException("Handle invalid/closed or address is zero");
            }

            bool result = Kernel32.WriteProcessMemory(procHandle, address, bytes, numOfBytesToWrite, out int numBytesWritten);

            if (!result || numBytesWritten != numOfBytesToWrite)
            {
                throw new WriteMemoryException(address, numOfBytesToWrite);
            }
        }

        /// <inheritdoc />
        public IntPtr ToAbsolute(IntPtr address)
        {
            if (address.ToInt64() > MainModule.ModuleMemorySize)
            {
                throw new ArgumentOutOfRangeException(nameof(address),
                    "Relative address greater than main module size.");
            }

            return new IntPtr(MainModule.BaseAddress.ToInt64() + address.ToInt64());
        }

        /// <inheritdoc />
        public IntPtr GetAddress(IntPtr address, int[] offsets, bool relative = false)
        {
            address = Read<IntPtr>(address, relative);
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                address = Read<IntPtr>(address + offsets[i]);
            }

            return address + offsets[offsets.Length - 1];
        }

        /// <inheritdoc />
        public IntPtr Alloc(int size, MemoryProtection protFlags = MemoryProtection.EXECUTE_READWRITE) => Alloc(ProcessHandle, size, protFlags);

        private static IntPtr Alloc(Handle procHandle, int size, MemoryProtection protFlags = MemoryProtection.EXECUTE_READWRITE, MemoryAllocation allocFlags = MemoryAllocation.Commit)
        {
            if (!procHandle.IsInvalid && !procHandle.IsClosed)
            {
                IntPtr rtn = Kernel32.VirtualAllocEx(procHandle, IntPtr.Zero, size, allocFlags, protFlags);
                if (rtn != IntPtr.Zero)
                {
                    return rtn;
                }
            }

            throw new Exception($"Couldn't allocate {size} bytes of memory.");
        }

        /// <inheritdoc />
        public void Free(IntPtr address) => Free(ProcessHandle, address);

        private static void Free(Handle procHandle, IntPtr address)
        {
            if (!procHandle.IsInvalid && !procHandle.IsClosed && address != IntPtr.Zero)
            {
                if (Kernel32.VirtualFreeEx(procHandle, address, 0, MemoryRelease.Release))
                {
                    return;
                }
            }

            throw new Exception($"Couldn't free memory at 0x{address.ToString("X")}.");
        }

        #region IDisposable Support
        /// <summary>
        /// If this object has been disposed
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    ProcessHandle?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                IsDisposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RemoteMemory()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        /// <inheritdoc />
        public void Dispose() =>
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);// TODO: uncomment the following line if the finalizer is overridden above.// GC.SuppressFinalize(this);
        #endregion
    }
}
