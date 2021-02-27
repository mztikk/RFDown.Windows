using System;
using RFDown.Windows.Native.Structs;

namespace RFDown.Windows.Input
{
    public static class CommonInputs
    {
        // Token: 0x0600015B RID: 347 RVA: 0x000060B8 File Offset: 0x000042B8
        static CommonInputs()
        {
            INPUT[] array = new INPUT[4];
            int num = 0;
            INPUT input = default;
            input.Type = 1U;
            input.Data.Keyboard = new KEYBDINPUT
            {
                KeyCode = 17,
                Scan = 0,
                Flags = 1U,
                Time = 0U,
                ExtraInfo = IntPtr.Zero
            };
            array[num] = input;
            int num2 = 1;
            input = default;
            input.Type = 1U;
            input.Data.Keyboard = new KEYBDINPUT
            {
                KeyCode = 65,
                Scan = 0,
                Flags = 0U,
                Time = 0U,
                ExtraInfo = IntPtr.Zero
            };
            array[num2] = input;
            int num3 = 2;
            input = default;
            input.Type = 1U;
            input.Data.Keyboard = new KEYBDINPUT
            {
                KeyCode = 17,
                Scan = 0,
                Flags = 3U,
                Time = 0U,
                ExtraInfo = IntPtr.Zero
            };
            array[num3] = input;
            int num4 = 3;
            input = default;
            input.Type = 1U;
            input.Data.Keyboard = new KEYBDINPUT
            {
                KeyCode = 65,
                Scan = 0,
                Flags = 2U,
                Time = 0U,
                ExtraInfo = IntPtr.Zero
            };
            array[num4] = input;
            CtrlA = array;
            INPUT[] array2 = new INPUT[1];
            int num5 = 0;
            input = default;
            input.Type = 1U;
            input.Data.Keyboard = new KEYBDINPUT
            {
                KeyCode = 13,
                Scan = 0,
                Flags = 0U,
                Time = 0U,
                ExtraInfo = IntPtr.Zero
            };
            array2[num5] = input;
            Enter = array2;
            CtrlAEnter = new INPUT[CtrlA.Length + Enter.Length];
            Array.Copy(CtrlA, 0, CtrlAEnter, 0, CtrlA.Length);
            Array.Copy(Enter, 0, CtrlAEnter, CtrlA.Length, Enter.Length);
        }

        // Token: 0x0400029D RID: 669
        public static readonly INPUT[] CtrlA;

        // Token: 0x0400029E RID: 670
        public static readonly INPUT[] Enter;

        // Token: 0x0400029F RID: 671
        public static readonly INPUT[] CtrlAEnter;
    }
}
