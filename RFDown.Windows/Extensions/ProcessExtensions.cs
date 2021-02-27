using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace RFDown.Windows.Extensions
{
    /// <summary>
    /// Extends <see cref="Process"/>
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// Gets all commandline arguments a process was started with.
        /// </summary>
        /// <param name="proc">Process to get arguments from.</param>
        /// <returns>String of the commandline arguments if successfull, <see cref="string.Empty"/> otherwise.</returns>
        public static string GetCommandLine(this Process proc)
        {
            using var searcher = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {proc.Id}");

            foreach (ManagementBaseObject item in searcher.Get())
            {
                object? cmd = item["CommandLine"];
                if (cmd is null)
                {
                    continue;
                }

                return cmd.ToString();
            }


            return string.Empty;
        }

        /// <summary>
        /// Searches the <see cref="ProcessModuleCollection"/> for <paramref name="moduleName"/>.
        /// </summary>
        /// <param name="proc"><see cref="Process"/> which modules to search</param>
        /// <param name="moduleName">Module name to search for.</param>
        /// <returns><see cref="ProcessModule"/></returns>
        public static ProcessModule GetModule(this Process proc, string moduleName)
        {
            for (int i = 0; i < proc.Modules.Count; i++)
            {
                ProcessModule module = proc.Modules[i];
                if (module.ModuleName.Equals(moduleName, StringComparison.Ordinal))
                {
                    return module;
                }
            }

            throw new DllNotFoundException($"Module \"{moduleName}\" not found.");
        }

        /// <summary>
        /// Searches the <see cref="ProcessModuleCollection"/> for all <see cref="ProcessModule"/>s that the <paramref name="moduleSelector"/> returns <see langword="true"/> for.
        /// </summary>
        /// <param name="proc"><see cref="Process"/> which modules to search</param>
        /// <param name="moduleSelector">A function to test each <see cref="ProcessModule"/> for a condition.</param>
        /// <returns><see cref="ProcessModule"/></returns>
        public static IEnumerable<ProcessModule> GetModules(this Process proc, Func<ProcessModule, bool> moduleSelector)
        {
            for (int i = 0; i < proc.Modules.Count; i++)
            {
                ProcessModule module = proc.Modules[i];
                if (moduleSelector(module))
                {
                    yield return module;
                }
            }
        }
    }
}
