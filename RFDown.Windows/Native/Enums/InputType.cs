namespace RFDown.Windows.Native.Enums
{
    /// <summary>
    ///     Types of INPUT
    /// </summary>
    public enum InputType : uint
    {
        /// <summary>
        ///     The event is a mouse event. Use the mi structure of the union.
        /// </summary>
        MOUSE = 0,

        /// <summary>
        ///     The event is a keyboard event. Use the ki structure of the union.
        /// </summary>
        KEYBOARD = 1,

        /// <summary>
        ///     The event is a hardware event. Use the hi structure of the union.
        /// </summary>
        HARDWARE = 2
    }
}
