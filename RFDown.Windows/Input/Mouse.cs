using System;
using System.Threading;
using RFDown.Windows.Native;
using RFDown.Windows.Native.Enums;

namespace RFDown.Windows.Input
{
    /// <summary>
    /// Class to manipulate and generate mouse input.
    /// </summary>
    public static class Mouse
    {
        /// <summary>
        ///     Posts a WM_LBUTTONDOWN followed by a WM_LBUTTONUP to a window, at the specified x- and y-
        ///     coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        /// <param name="delay">
        ///     Delay between WM_LBUTTONDOWN and WM_LBUTTONUP
        /// </param>
        public static void PostMessageLButtonClick(IntPtr hWnd, int xPos, int yPos, int delay = 5)
        {
            uint lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONDOWN, (uint)MouseKey.MK_LBUTTON, lParam);

            Thread.Sleep(delay);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_LBUTTONDOWN to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        public static void PostMessageLButtonDown(IntPtr hWnd, int xPos, int yPos)
        {
            uint lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONDOWN, (uint)MouseKey.MK_LBUTTON, lParam);
        }

        /// <summary>
        ///     Posts a WM_LBUTTONUP to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        public static void PostMessageLButtonUp(IntPtr hWnd, int xPos, int yPos)
        {
            uint lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_LBUTTONDOWN, WM_LBUTTONUP, WM_LBUTTONDBLCLK and then WM_LBUTTONUP to a window, at the specified
        ///     coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        /// <param name="delay">
        ///     Delay between each PostMessage.
        /// </param>
        public static void PostMessageLButtonDoubleClick(IntPtr hWnd, int xPos, int yPos, int delay = 5)
        {
            uint lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONDOWN, (uint)MouseKey.MK_LBUTTON, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONDBLCLK, (uint)MouseKey.MK_LBUTTON, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_LBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_RBUTTONDOWN followed by a WM_RBUTTONUP to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        /// <param name="delay">
        ///     Delay between WM_RBUTTONDOWN and WM_RBUTTONUP
        /// </param>
        public static void PostMessageRButtonClick(IntPtr hWnd, int xPos, int yPos, int delay = 5)
        {
            uint lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONDOWN, (uint)MouseKey.MK_RBUTTON, lParam);

            Thread.Sleep(delay);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_RBUTTONDOWN, WM_RBUTTONUP, WM_RBUTTONDBLCLK and then WM_RBUTTONUP to a window, at the specified
        ///     coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        /// <param name="delay">
        ///     Delay between each PostMessage.
        /// </param>
        public static void PostMessageRButtonDoubleClick(IntPtr hWnd, int xPos, int yPos, int delay = 5)
        {
            uint lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONDOWN, (uint)MouseKey.MK_LBUTTON, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONUP, 0, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONDBLCLK, (uint)MouseKey.MK_LBUTTON, lParam);
            Thread.Sleep(delay);
            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Posts a WM_RBUTTONDOWN to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        public static void PostMessageRButtonDown(IntPtr hWnd, int xPos, int yPos)
        {
            uint lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONDOWN, (uint)MouseKey.MK_RBUTTON, lParam);
        }

        /// <summary>
        ///     Posts a WM_RBUTTONUP to a window, at the specified coordinates.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window.
        /// </param>
        /// <param name="xPos">
        ///     x-coordinate
        /// </param>
        /// <param name="yPos">
        ///     y-coordinate
        /// </param>
        public static void PostMessageRButtonUp(IntPtr hWnd, int xPos, int yPos)
        {
            uint lParam = (uint)((yPos << 16) | xPos);

            User32.PostMessage(hWnd, WindowsMessage.WM_RBUTTONUP, 0, lParam);
        }

        /// <summary>
        ///     Simulates a left mouse button click at the current cursor position.
        /// </summary>
        /// <param name="delay">
        ///     Delay between down and up.
        /// </param>
        public static void MouseEventLClick(int delay = 5)
        {
            MouseEventLDown();
            Thread.Sleep(delay);
            MouseEventLUp();
        }

        /// <summary>
        ///     Simulates a left mouse button down at the current cursor position.
        /// </summary>
        public static void MouseEventLDown() => User32.mouse_event(MouseEventF.LEFTDOWN, 0, 0, 0, 0);

        /// <summary>
        ///     Simulates a left mouse button up at the current cursor position.
        /// </summary>
        public static void MouseEventLUp() => User32.mouse_event(MouseEventF.LEFTUP, 0, 0, 0, 0);

        /// <summary>
        ///     Simulates a left mouse button down at the current cursor position.
        /// </summary>
        public static void MouseEventLDown(int x, int y) => User32.mouse_event(MouseEventF.LEFTDOWN, x, y, 0, 0);

        /// <summary>
        ///     Simulates a left mouse button up at the current cursor position.
        /// </summary>
        public static void MouseEventLUp(int x, int y) => User32.mouse_event(MouseEventF.LEFTUP, x, y, 0, 0);

        /// <summary>
        ///     Simulates a right mouse button click at the current cursor position.
        /// </summary>
        /// <param name="delay">
        ///     Delay between down and up.
        /// </param>
        public static void MouseEventRClick(int delay = 5)
        {
            MouseEventRDown();
            Thread.Sleep(delay);
            MouseEventRUp();
        }

        /// <summary>
        ///     Simulates a right mouse button down at the current cursor position.
        /// </summary>
        public static void MouseEventRDown() => User32.mouse_event(MouseEventF.RIGHTDOWN, 0, 0, 0, 0);

        /// <summary>
        ///     Simulates a right mouse button up at the current cursor position.
        /// </summary>
        public static void MouseEventRUp() => User32.mouse_event(MouseEventF.RIGHTUP, 0, 0, 0, 0);

        /// <summary>
        ///     Simulates a right mouse button down at the current cursor position.
        /// </summary>
        public static void MouseEventRDown(int x, int y) => User32.mouse_event(MouseEventF.RIGHTDOWN, x, y, 0, 0);

        /// <summary>
        ///     Simulates a right mouse button up at the current cursor position.
        /// </summary>
        public static void MouseEventRUp(int x, int y) => User32.mouse_event(MouseEventF.RIGHTUP, x, y, 0, 0);

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
        ///     TRUE if successful; otherwise FALSE.
        /// </returns>
        public static bool SetCursorPos(int x, int y) => User32.SetCursorPos(x, y);

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
        public static bool SetPhysicalCursorPos(int x, int y) => User32.SetPhysicalCursorPos(x, y);
    }
}
