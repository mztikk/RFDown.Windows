using System;
using System.Runtime.InteropServices;
using RFDown.Windows.Native.Enums;
using RFDown.Windows.Native.Structs;

namespace RFDown.Windows.Native
{
    /// <summary>
    /// Methods from the User32.dll
    /// </summary>
    public static class User32
    {
        /// <summary>
        ///     Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the
        ///     specified window and does not return until the window procedure has processed the message.
        ///     To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function.To post a
        ///     message to a thread's message queue and return immediately, use the PostMessage or PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure will receive the message. If this parameter is HWND_BROADCAST
        ///     ((HWND)0xffff), the message is sent to all top-level windows in the system, including disabled or invisible unowned
        ///     windows, overlapped windows, and pop-up windows; but the message is not sent to child windows.
        ///     Message sending is subject to UIPI.
        ///     The thread of a process can send messages only to message queues of threads in
        ///     processes of lesser or equal integrity level.
        /// </param>
        /// <param name="uMsg">
        ///     The message to be sent.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     The return value specifies the result of the message processing; it depends on the message sent.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage([In] IntPtr hWnd, [In] int uMsg, [In] uint wParam, [In] uint lParam);

        /// <summary>
        ///     Translates a character to the corresponding virtual-key code and shift state for the current keyboard.
        /// </summary>
        /// <param name="ch">
        ///     The character to be translated into a virtual-key code.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the low-order byte of the return value contains the virtual-key code and the high-order
        ///     byte contains the shift state, which can be a combination of the following flag bits.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern short VkKeyScan(char ch);

        /// <summary>
        ///     Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a
        ///     virtual-key code.
        ///     To specify a handle to the keyboard layout to use for translating the specified code, use the MapVirtualKeyEx
        ///     function.
        /// </summary>
        /// <param name="uCode">
        ///     The virtual key code or scan code for a key. How this value is interpreted depends on the value of the uMapType
        ///     parameter.
        /// </param>
        /// <param name="uMapType">
        ///     The translation to be performed. The value of this parameter depends on the value of the uCode parameter.
        /// </param>
        /// <returns>
        ///     The return value is either a scan code, a virtual-key code, or a character value, depending on the value of uCode
        ///     and uMapType. If there is no translation, the return value is zero.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     HWND_BROADCAST ((HWND)0xffff): The message is posted to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to child windows.
        ///     NULL: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of
        ///     the current thread.
        /// </param>
        /// <param name="msg">
        ///     The message to be posted.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage([In] IntPtr hWnd, [In] uint msg, [In] uint wParam, [In] uint lParam);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     HWND_BROADCAST ((HWND)0xffff): The message is posted to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to child windows.
        ///     NULL: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of
        ///     the current thread.
        /// </param>
        /// <param name="msg">
        ///     The message to be posted.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage([In] IntPtr hWnd, [In] uint msg, [In] ulong wParam, [In] uint lParam);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     HWND_BROADCAST ((HWND)0xffff): The message is posted to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to child windows.
        ///     NULL: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of
        ///     the current thread.
        /// </param>
        /// <param name="msg">
        ///     The message to be posted.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage([In] IntPtr hWnd, [In] WindowsMessage msg, [In] uint wParam,
            [In] uint lParam);

        /// <summary>
        ///     Places (posts) a message in the message queue associated with the thread that created the specified window and
        ///     returns without waiting for the thread to process the message.
        ///     To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure is to receive the message. The following values have special
        ///     meanings.
        ///     HWND_BROADCAST ((HWND)0xffff): The message is posted to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows. The message is not posted to child windows.
        ///     NULL: The function behaves like a call to PostThreadMessage with the dwThreadId parameter set to the identifier of
        ///     the current thread.
        /// </param>
        /// <param name="msg">
        ///     The message to be posted.
        /// </param>
        /// <param name="wParam">
        ///     Additional message-specific information.
        /// </param>
        /// <param name="lParam">
        ///     Additional message-specific information.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero.To get extended error information, call GetLastError.GetLastError
        ///     returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage([In] IntPtr hWnd, [In] WindowsMessage msg, [In] ulong wParam,
            [In] uint lParam);

        /// <summary>
        ///     The mouse_event function synthesizes mouse motion and button clicks.
        /// </summary>
        /// <param name="dwFlags">
        ///     Controls various aspects of mouse motion and button clicking. This parameter can be certain combinations of
        ///     <see cref="MouseEventF" />.
        /// </param>
        /// <param name="dx">
        ///     The mouse's absolute position along the x-axis or its amount of motion since the last mouse event was generated,
        ///     depending on the setting of MOUSEEVENTF_ABSOLUTE. Absolute data is specified as the mouse's actual x-coordinate;
        ///     relative data is specified as the number of mickeys moved. A mickey is the amount that a mouse has to move for it
        ///     to report that it has moved.
        /// </param>
        /// <param name="dy">
        ///     The mouse's absolute position along the y-axis or its amount of motion since the last mouse event was generated,
        ///     depending on the setting of MOUSEEVENTF_ABSOLUTE. Absolute data is specified as the mouse's actual y-coordinate;
        ///     relative data is specified as the number of mickeys moved.
        /// </param>
        /// <param name="dwData">
        ///     If dwFlags contains MOUSEEVENTF_WHEEL, then dwData specifies the amount of wheel movement. A positive value
        ///     indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was
        ///     rotated backward, toward the user. One wheel click is defined as WHEEL_DELTA, which is 120.
        ///     If dwFlags contains MOUSEEVENTF_HWHEEL, then dwData specifies the amount of wheel movement.A positive value
        ///     indicates that the wheel was tilted to the right; a negative value indicates that the wheel was tilted to the left.
        ///     If dwFlags contains MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP, then dwData specifies which X buttons were pressed or
        ///     released. This value may be any combination of the following flags.
        ///     If dwFlags is not MOUSEEVENTF_WHEEL, MOUSEEVENTF_XDOWN, or MOUSEEVENTF_XUP, then dwData should be zero.
        /// </param>
        /// <param name="dwExtraInfo">
        ///     An additional value associated with the mouse event. An application calls GetMessageExtraInfo to obtain this extra
        ///     information.
        /// </param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event([In] MouseEventF dwFlags, [In] int dx, [In] int dy, [In] uint dwData,
            [In] uint dwExtraInfo);

        /// <summary>
        ///     Moves the cursor to the specified screen coordinates. If the new coordinates are not within the screen rectangle
        ///     set by the most recent ClipCursor function call, the system automatically adjusts the coordinates so that the
        ///     cursor stays within the rectangle.
        /// </summary>
        /// <param name="x">
        ///     The new x-coordinate of the cursor, in screen coordinates.
        /// </param>
        /// <param name="y">
        ///     The new y-coordinate of the cursor, in screen coordinates.
        /// </param>
        /// <returns>
        ///     Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos([In] int x, [In] int y);

        /// <summary>
        ///     Sets the position of the cursor in physical coordinates.
        /// </summary>
        /// <param name="x">
        ///     The new x-coordinate of the cursor, in physical coordinates.
        /// </param>
        /// <param name="y">
        ///     The new y-coordinate of the cursor, in physical coordinates.
        /// </param>
        /// <returns>
        ///     TRUE if successful; otherwise FALSE.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetPhysicalCursorPos([In] int x, [In] int y);

        /// <summary>
        /// Determines whether a key is up or down at the time the function is called, and whether the key was pressed after a previous call to GetAsyncKeyState
        /// </summary>
        /// <param name="vKey">
        /// The virtual-key code. For more information, see Virtual Key Codes.
        /// You can use left- and right-distinguishing constants to specify certain keys.See the Remarks section for further information.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value specifies whether the key was pressed since the last call to GetAsyncKeyState, and whether the key is currently up or down.
        /// If the most significant bit is set, the key is down, and if the least significant bit is set, the key was pressed after the previous call to GetAsyncKeyState.
        /// However, you should not rely on this last behavior; for more information, see the Remarks.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetAsyncKeyState([In] VirtualKeyCode vKey);

        /// <summary>
        ///     Synthesizes a keystroke. The system can use such a synthesized keystroke to generate a WM_KEYUP or WM_KEYDOWN
        ///     message. The keyboard driver's interrupt handler calls the keybd_event function.
        /// </summary>
        /// <param name="bVk">
        ///     A virtual-key code. The code must be a value in the range 1 to 254. For a complete list,
        ///     <see cref="VirtualKeyCode" />.
        /// </param>
        /// <param name="bScan">
        ///     A hardware scan code for the key.
        /// </param>
        /// <param name="dwFlags">
        ///     Controls various aspects of function operation. This parameter can be one or more of the following values.
        ///     <see cref="KeyEventF" />
        /// </param>
        /// <param name="dwExtraInfo">
        ///     An additional value associated with the key stroke.
        /// </param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event([In] VirtualKeyCode bVk, [In] byte bScan, [In] KeyEventF dwFlags,
            [In] uint dwExtraInfo);

        /// <summary>
        ///     The set foreground window.
        /// </summary>
        /// <param name="hWnd">
        ///     The h wnd.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///     Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="nInputs">
        ///     The number of structures in the pInputs array.
        /// </param>
        /// <param name="pInputs">
        ///     An array of INPUT structures. Each structure represents an event to be inserted into the keyboard or mouse input
        ///     stream.
        /// </param>
        /// <param name="cbSize">
        ///     The size, in bytes, of an INPUT structure. If cbSize is not the size of an INPUT structure, the function fails.
        /// </param>
        /// <returns>
        ///     The function returns the number of events that it successfully inserted into the keyboard or mouse input stream. If
        ///     the function returns zero, the input was already blocked by another thread. To get extended error information, call
        ///     GetLastError.
        ///     This function fails when it is blocked by UIPI.Note that neither GetLastError nor the return value will indicate
        ///     the failure was caused by UIPI blocking.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput([In] uint nInputs, [MarshalAs(UnmanagedType.LPArray)] [In]
            INPUT[] pInputs, [In] int cbSize);
    }
}
