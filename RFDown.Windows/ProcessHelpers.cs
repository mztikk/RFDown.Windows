using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RFDown.AoB;
using RFDown.Windows.Extensions;
using RFDown.Windows.Memory;
using RFDown.Windows.Memory.Exceptions;
using RFDown.Windows.Native;
using RFDown.Windows.Native.Enums;

namespace RFDown.Windows
{
    /// <summary>
    /// Adds various methods to interact with <see cref="Process"/>
    /// </summary>
    public static class ProcessHelpers
    {
        /// <summary>
        ///     Opens an existing local process object.
        /// </summary>
        /// <param name="accessFlags">
        ///     The access to the process object. This access right is checked against the security descriptor for the process.
        ///     This parameter can be one or more of the process access rights. If the caller has enabled the SeDebugPrivilege
        ///     privilege, the requested access is granted regardless of the contents of the security descriptor.
        /// </param>
        /// <param name="procId">
        ///     The identifier of the local process to be opened. If the specified process is the System Process(0x00000000), the
        ///     function fails and the last error code is ERROR_INVALID_PARAMETER.If the specified process is the Idle process or
        ///     one of the CSRSS processes, this function fails and the last error code is ERROR_ACCESS_DENIED because their access
        ///     restrictions prevent user-level code from opening them. If you are using GetCurrentProcessId as an argument to this
        ///     function, consider using GetCurrentProcess instead of OpenProcess, for improved performance.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is an open handle to the specified process. If the function fails, the
        ///     return value is NULL.To get extended error information, call GetLastError.
        /// </returns>
        public static Handle OpenProcess(ProcessAccessFlags accessFlags, int procId)
        {
            Handle handle = Kernel32.OpenProcess(accessFlags, false, procId);

            if (!handle.IsInvalid && !handle.IsClosed)
            {
                return handle;
            }

            throw new OpenProcessException(procId);
        }

        /// <summary>
        ///     Opens an existing local process object.
        /// </summary>
        /// <param name="accessFlags">
        ///     The access to the process object. This access right is checked against the security descriptor for the process.
        ///     This parameter can be one or more of the process access rights. If the caller has enabled the SeDebugPrivilege
        ///     privilege, the requested access is granted regardless of the contents of the security descriptor.
        /// </param>
        /// <param name="proc">
        ///     The identifier of the local process to be opened. If the specified process is the System Process(0x00000000), the
        ///     function fails and the last error code is ERROR_INVALID_PARAMETER.If the specified process is the Idle process or
        ///     one of the CSRSS processes, this function fails and the last error code is ERROR_ACCESS_DENIED because their access
        ///     restrictions prevent user-level code from opening them. If you are using GetCurrentProcessId as an argument to this
        ///     function, consider using GetCurrentProcess instead of OpenProcess, for improved performance.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is an open handle to the specified process. If the function fails, the
        ///     return value is NULL.To get extended error information, call GetLastError.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Handle OpenProcess(ProcessAccessFlags accessFlags, Process proc) => OpenProcess(accessFlags, proc.Id);

        /// <summary>
        /// Determines whether the specified process is 64bit or not.
        /// </summary>
        /// <param name="proc">The process to check.</param>
        /// <returns><see langword="true"/> if 64bit, otherwise <see langword="false"/>.</returns>
        public static bool Is64Bit(Process proc)
        {
            Kernel32.IsWow64Process(proc.Handle, out bool iswow64);
            return !iswow64 && Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines whether the specified process exists.
        /// </summary>
        /// <param name="process">The process to check.</param>
        /// <returns><see langword="true"/> if process exists, otherwise <see langword="false"/>.</returns>
        public static bool ProcessExists(Process process)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            try
            {
                Process? proc = Process.GetProcessById(process.Id);
                if (proc?.HasExited != false)
                {
                    return false;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Win32Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets a <see cref="Process"/> by name and optional index
        /// </summary>
        /// <param name="processName">Process name to get the <see cref="Process"/>es for.</param>
        /// <param name="index">Index to use if there are multpiple <see cref="Process"/>es of the same name, default 0 to use first one.</param>
        /// <exception cref="ProcessNotFoundException">Thrown if no process with the specified <paramref name="processName"/> is found</exception>
        public static Process GetProcessByName(string processName, int index = 0)
        {
            Process[] procs = Process.GetProcessesByName(processName);
            if (procs.Length == 0)
            {
                throw new ProcessNotFoundException(processName);
            }

            return procs[index];
        }

        /// <summary>
        /// Gets a <see cref="Process"/> by name and optional index
        /// </summary>
        /// <param name="processName">Process name to get the <see cref="Process"/>es for.</param>
        /// <param name="processSelector"><see cref="Func{T, TResult}"/> will be called on every <see cref="Process"/> and returns the <see cref="Process"/> on which this <see cref="Func{T, TResult}"/> returns <see langword="true"/> for</param>
        /// <exception cref="ProcessNotFoundException">Thrown if no process with the specified <paramref name="processName"/> is found</exception>
        public static Process GetProcessByName(string processName, Func<Process, bool> processSelector)
        {
            Process[] procs = Process.GetProcessesByName(processName);
            if (procs.Length == 0)
            {
                throw new ProcessNotFoundException(processName);
            }

            foreach (Process proc in procs)
            {
                if (processSelector(proc))
                {
                    return proc;
                }
            }

            throw new ProcessNotFoundException(processName);
        }

        /// <summary>
        /// Enumerates all <see cref="Process"/>es that match a given predicate
        /// </summary>
        /// <param name="processSelector">Predicate used to select <see cref="Process"/>es</param>
        public static IEnumerable<Process> GetProcesses(Func<IRemoteMemory, bool> processSelector)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (!ProcessExists(proc))
                {
                    continue;
                }

                bool yield = false;
                try
                {
                    using var remoteMemory = new RemoteMemory(proc);
                    yield = processSelector(remoteMemory);
                }
                catch (OpenProcessException)
                {
                    continue;
                }

                if (yield)
                {
                    yield return proc;
                }
            }
        }

        /// <summary>
        /// Enumerates all <see cref="Process"/>es that match a given <see cref="Signature"/>
        /// </summary>
        /// <param name="signature"><see cref="Signature"/> to check the <see cref="Process"/>es for</param>
        public static IEnumerable<Process> GetProcessesBySignature(Signature signature) => GetProcesses(remoteMemory => remoteMemory.FindSignature(signature) != -1);
    }
}
