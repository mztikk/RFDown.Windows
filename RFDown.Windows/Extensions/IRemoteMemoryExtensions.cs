using System;
using System.Diagnostics;
using RFDown.AoB;
using RFDown.Windows.Memory;
using RFDown.Windows.Memory.Exceptions;

namespace RFDown.Windows.Extensions
{
    /// <summary>
    /// Extends <see cref="IRemoteMemory"/>
    /// </summary>
    public static class IRemoteMemoryExtensions
    {
        #region AoB_Scanner
        /// <summary>
        /// Searches for a PEiD style string signature inside of the processes main module.
        /// </summary>
        /// <param name="remoteMem"><see cref="IRemoteMemory"/> used to read from the process.</param>
        /// <param name="signature">PEiD style string signature to search for.</param>
        /// <returns>The address of the <see cref="Signature"/> if it is found; -1 otherwise</returns>
        public static long FindSignature(this IRemoteMemory remoteMem, string signature) => FindSignature(remoteMem, new Signature(signature), remoteMem.MainModule);

        /// <summary>
        /// Searches for a given <see cref="Signature"/> inside of the processes main module.
        /// </summary>
        /// <param name="remoteMem"><see cref="IRemoteMemory"/> used to read from the process.</param>
        /// <param name="signature">The <see cref="Signature"/> to search for.</param>
        /// <returns>The address of the <see cref="Signature"/> if it is found; -1 otherwise</returns>
        public static long FindSignature(this IRemoteMemory remoteMem, Signature signature) => FindSignature(remoteMem, signature, remoteMem.MainModule);

        /// <summary>
        /// Searches for a given <see cref="Signature"/> inside of a <see cref="ProcessModule"/>.
        /// </summary>
        /// <param name="remoteMem"><see cref="IRemoteMemory"/> used to read from the process.</param>
        /// <param name="signature">The <see cref="Signature"/> to search for.</param>
        /// <param name="processModule">The <see cref="ProcessModule"/> to search in.</param>
        /// <returns>The address of the <see cref="Signature"/> if it is found; -1 otherwise</returns>
        public static long FindSignature(this IRemoteMemory remoteMem, Signature signature, ProcessModule processModule)
        {
            byte[] dumpedModule;
            try
            {
                dumpedModule = remoteMem.ReadBytes(processModule.BaseAddress, processModule.ModuleMemorySize);
            }
            catch (ReadMemoryException)
            {
                return -1;
            }

            long found = Scanner.FindSignature(dumpedModule, signature);
            if (found == -1)
            {
                return -1;
            }

            return processModule.BaseAddress.ToInt64() + found;
        }
        #endregion AoB_Scanner

        #region Pointer
        /// <inheritdoc cref="RemoteMemory.GetAddress(IntPtr, int[], bool)"/>
        public static IntPtr GetAddress(this IRemoteMemory remoteMemory, int address, int[] offsets, bool relative = false) => remoteMemory.GetAddress(new IntPtr(address), offsets, relative);
        /// <inheritdoc cref="RemoteMemory.GetAddress(IntPtr, int[], bool)"/>
        public static IntPtr GetAddress(this IRemoteMemory remoteMemory, long address, int[] offsets, bool relative = false) => remoteMemory.GetAddress(new IntPtr(address), offsets, relative);

        /// <summary>
        /// Walks a pointer(base + offsets) and reads + returns the final value.
        /// </summary>
        /// <param name="remoteMemory"><see cref="IRemoteMemory"/> used to read from the process.</param>
        /// <param name="address">BaseAddress</param>
        /// <param name="offsets">Offsets to walk</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>The final value of the pointer.</returns>
        public static T ReadPointer<T>(this IRemoteMemory remoteMemory, IntPtr address, int[] offsets, bool relative = false) where T : unmanaged => remoteMemory.Read<T>(remoteMemory.GetAddress(address, offsets, relative));
        /// <inheritdoc cref="ReadPointer{T}(IRemoteMemory, IntPtr, int[], bool)" />
        public static T ReadPointer<T>(this IRemoteMemory remoteMemory, int address, int[] offsets, bool relative = false) where T : unmanaged => remoteMemory.Read<T>(remoteMemory.GetAddress(address, offsets, relative));
        /// <inheritdoc cref="ReadPointer{T}(IRemoteMemory, IntPtr, int[], bool)" />
        public static T ReadPointer<T>(this IRemoteMemory remoteMemory, long address, int[] offsets, bool relative = false) where T : unmanaged => remoteMemory.Read<T>(remoteMemory.GetAddress(address, offsets, relative));
        #endregion Pointer

        #region Read
        /// <inheritdoc cref="RemoteMemory.Read{T}(IntPtr, bool)" />
        public static T Read<T>(this IRemoteMemory remoteMemory, int address, bool relative = false) where T : unmanaged => remoteMemory.Read<T>(new IntPtr(address), relative);
        /// <inheritdoc cref="RemoteMemory.Read{T}(IntPtr, bool)" />
        public static T Read<T>(this IRemoteMemory remoteMemory, long address, bool relative = false) where T : unmanaged => remoteMemory.Read<T>(new IntPtr(address), relative);
        #endregion Read

        #region Write
        /// <inheritdoc cref="RemoteMemory.Write{T}(IntPtr, T, bool)" />
        public static T Write<T>(this IRemoteMemory remoteMemory, int address, bool relative = false) where T : unmanaged => remoteMemory.Read<T>(new IntPtr(address), relative);
        /// <inheritdoc cref="RemoteMemory.Write{T}(IntPtr, T, bool)" />
        public static T Write<T>(this IRemoteMemory remoteMemory, long address, bool relative = false) where T : unmanaged => remoteMemory.Read<T>(new IntPtr(address), relative);
        #endregion Write
    }
}
