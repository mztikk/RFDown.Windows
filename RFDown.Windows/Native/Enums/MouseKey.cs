using System;

namespace RFDown.Windows.Native.Enums
{
    /// <summary>
    ///     Indicates whether various virtual keys are down.
    /// </summary>
    [Flags]
#pragma warning disable RCS1135 // Declare enum member with zero value (when enum has FlagsAttribute).
    public enum MouseKey
#pragma warning restore RCS1135 // Declare enum member with zero value (when enum has FlagsAttribute).
    {
        /// <summary>
        ///     The left mouse button is down.
        /// </summary>
        MK_LBUTTON = 0x0001,

        /// <summary>
        ///     The right mouse button is down.
        /// </summary>
        MK_RBUTTON = 0x0002,

        /// <summary>
        ///     The SHIFT key is down.
        /// </summary>
        MK_SHIFT = 0x0004,

        /// <summary>
        ///     The CTRL key is down.
        /// </summary>
        MK_CONTROL = 0x0008,

        /// <summary>
        ///     The middle mouse button is down.
        /// </summary>
        MK_MBUTTON = 0x0010,

        /// <summary>
        ///     The first X button is down.
        /// </summary>
        MK_XBUTTON1 = 0x0020,

        /// <summary>
        ///     The second X button is down.
        /// </summary>
        MK_XBUTTON2 = 0x0040
    }
}
