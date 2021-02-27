using System;
using System.Diagnostics;
using RFDown.Windows.Native;
using RFDown.Windows.Native.Enums;

namespace RFDown.Windows.Memory
{
    /// <summary>
    /// Provides methods to interact with an opened remote process.
    /// </summary>
    public interface IOpenProcess : IDisposable
    {
        /// <summary>
        /// The native process we're interacting with.
        /// </summary>
        Process NativeProcess { get; }

        /// <summary>
        /// Handle to the process.
        /// </summary>
        Handle ProcessHandle { get; }

        /// <summary>
        /// Main module of the process.
        /// </summary>
        ProcessModule MainModule { get; }

        /// <summary>
        /// Converts a relative address to an absolute one.
        /// </summary>
        /// <param name="address">Relative address.</param>
        /// <returns>Absolute address</returns>
        IntPtr ToAbsolute(IntPtr address);

        /// <summary>
        /// Frees allocated memory(ex with <see cref="Alloc"/>
        /// </summary>
        /// <param name="address">Address of the allocated memory to free.</param>
        void Free(IntPtr address);

        /// <summary>
        /// Allocates memory in the remote process and returns the address of it.
        /// </summary>
        /// <param name="size">Size of memory to allocate.</param>
        /// <param name="protFlags">Protection flags of the newly allocated memory. Default is <see cref="MemoryProtection.EXECUTE_READWRITE"/></param>
        /// <returns>The address to the allocated memory.</returns>
        IntPtr Alloc(int size, MemoryProtection protFlags = MemoryProtection.EXECUTE_READWRITE);
    }
}
