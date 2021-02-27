using System;
using Microsoft.Win32.SafeHandles;

namespace RFDown.Windows.Native
{
    /// <summary>
    /// Win32 SafeHandle
    /// </summary>
    public class Handle : SafeHandleZeroOrMinusOneIsInvalid
    {
        #region Constructors and Destructors

        /// <summary>
        /// Paramterless constructor for handles built by <see cref="Kernel32.OpenProcess"/>
        /// </summary>
        public Handle()
            : base(true)
        {
        }

        /// <summary>
        /// Initializes a new handle with the handle as <see cref="IntPtr"/>
        /// </summary>
        /// <param name="handle"></param>
        public Handle(IntPtr handle)
            : base(true) => SetHandle(handle);

        #endregion

        #region Methods

        /// <summary>
        /// Closes the handle with <see cref="Kernel32.CloseHandle"/>
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle() => handle != IntPtr.Zero && Kernel32.CloseHandle(handle);

        #endregion
    }
}
