using System;
using System.Runtime.InteropServices;

namespace RFDown.Windows.Native.Structs
{
    public struct INPUT
    {
        // Token: 0x04000016 RID: 22
        public uint Type;

        // Token: 0x04000017 RID: 23
        public MOUSEKEYBDHARDWAREINPUT Data;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct MOUSEKEYBDHARDWAREINPUT
    {
        // Token: 0x04000034 RID: 52
        [FieldOffset(0)]
        public MOUSEINPUT Mouse;

        // Token: 0x04000035 RID: 53
        [FieldOffset(0)]
        public KEYBDINPUT Keyboard;

        // Token: 0x04000036 RID: 54
        [FieldOffset(0)]
        public HARDWAREINPUT Hardware;
    }

    public struct MOUSEINPUT
    {
        // Token: 0x0400002E RID: 46
        public int X;

        // Token: 0x0400002F RID: 47
        public int Y;

        // Token: 0x04000030 RID: 48
        public uint MouseData;

        // Token: 0x04000031 RID: 49
        public uint Flags;

        // Token: 0x04000032 RID: 50
        public uint Time;

        // Token: 0x04000033 RID: 51
        public IntPtr ExtraInfo;
    }

    public struct KEYBDINPUT
    {
        // Token: 0x04000018 RID: 24
        public ushort KeyCode;

        // Token: 0x04000019 RID: 25
        public ushort Scan;

        // Token: 0x0400001A RID: 26
        public uint Flags;

        // Token: 0x0400001B RID: 27
        public uint Time;

        // Token: 0x0400001C RID: 28
        public IntPtr ExtraInfo;
    }

    public struct HARDWAREINPUT
    {
        // Token: 0x04000013 RID: 19
        public uint Msg;

        // Token: 0x04000014 RID: 20
        public ushort ParamL;

        // Token: 0x04000015 RID: 21
        public ushort ParamH;
    }
}
