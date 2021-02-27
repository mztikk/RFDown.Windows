using System;

namespace RFDown.Windows.Native.Enums
{
    /// <summary>
    /// Memory-state options list.
    /// </summary>
    [Flags]
#pragma warning disable RCS1135 // Declare enum member with zero value (when enum has FlagsAttribute).
    public enum MemoryState
#pragma warning restore RCS1135 // Declare enum member with zero value (when enum has FlagsAttribute).
    {
        /// <summary>
        /// Indicates committed pages for which physical storage has been allocated, either in memory or in the paging file on disk.
        /// </summary>
        Commit = 0x1000,

        /// <summary>
        /// Indicates free pages not accessible to the calling process and available to be allocated.
        /// For free pages, the information in the AllocationBase, AllocationProtect, Protect, and Type members is undefined.
        /// </summary>
        Free = 0x10000,

        /// <summary>
        /// Indicates reserved pages where a range of the process's virtual address space is reserved without any physical storage being allocated.
        /// For reserved pages, the information in the Protect member is undefined.
        /// </summary>
        Reserve = 0x2000
    }
}
