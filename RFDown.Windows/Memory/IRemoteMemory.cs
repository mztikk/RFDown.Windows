using System;
using System.Text;

namespace RFDown.Windows.Memory
{
    /// <summary>
    /// Provides methods to interact with the memory of a remote process.
    /// </summary>
    public interface IRemoteMemory : IOpenProcess
    {
        /// <summary>
        /// Walks a pointer(base + offsets) and returns the final address.
        /// </summary>
        /// <param name="address">BaseAddress</param>
        /// <param name="offsets">Offsets to walk</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>The final address of the pointer.</returns>
        IntPtr GetAddress(IntPtr address, int[] offsets, bool relative = false);

        /// <summary>
        /// Reads a value of type <typeparamref name="T"/> from the remote process at a specified address.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="address">Address where to read the value from.</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>Value read from the remote memory.</returns>
        T Read<T>(IntPtr address, bool relative = false) where T : unmanaged;

        /// <summary>
        /// Reads an array of values of type <typeparamref name="T"/> from the remote process at a specified address.
        /// </summary>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <param name="address">Address where to read the values from.</param>
        /// <param name="count">Number of values to read, size of the return array.</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>Array of values read from the remote memory.</returns>
        T[] Read<T>(IntPtr address, int count, bool relative = false) where T : unmanaged;

        /// <summary>
        /// Reads a string in the remote process memory.
        /// </summary>
        /// <param name="address">Address of the values.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use to get the bytes from the string.</param>
        /// <param name="maxLength">Maximum length of the string(bytes to be read).</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>String at the address.</returns>
        string ReadString(IntPtr address, Encoding encoding, int maxLength = 512, bool relative = false);

        /// <summary>
        /// Reads a string with a default <see cref="Encoding"/> in the remote process memory.
        /// </summary>
        /// <param name="address">Address of the values.</param>
        /// <param name="maxLength">Maximum length of the string(bytes to be read).</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>String at the address.</returns>
        string ReadString(IntPtr address, int maxLength = 512, bool relative = false);

        /// <summary>
        /// Writes the value of type <typeparamref name="T"/> to the remote process at a specified address.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="address">Address where to write the value to.</param>
        /// <param name="value">Value to write.</param>
        /// <param name="relative">If the address is relative or not.</param>
        void Write<T>(IntPtr address, T value, bool relative = false) where T : unmanaged;

        /// <summary>
        /// Writes the array of values of type <typeparamref name="T"/> to the remote process at a specified address.
        /// </summary>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <param name="address">Address where to write the values to.</param>
        /// <param name="values">Values to write.</param>
        /// <param name="relative">If the address is relative or not.</param>
        void Write<T>(IntPtr address, T[] values, bool relative = false) where T : unmanaged;

        /// <summary>
        /// Writes a string, using a default <see cref="Encoding"/>, to the remote process at a specified address.
        /// </summary>
        /// <param name="address">Address where to write the string to.</param>
        /// <param name="text">String to write.</param>
        /// <param name="relative">If the address is relative or not.</param>
        void WriteString(IntPtr address, string text, bool relative = false);

        /// <summary>
        /// Writes a string, using the specified <paramref name="encoding"/>, to the remote process at a specified address.
        /// </summary>
        /// <param name="address">Address where to write the string to.</param>
        /// <param name="text">String to write.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use to get the bytes from the string.</param>
        /// <param name="relative">If the address is relative or not.</param>
        void WriteString(IntPtr address, string text, Encoding encoding, bool relative = false);

        /// <summary>
        /// Reads bytes from the remote process at a specified address.
        /// </summary>
        /// <param name="address">Address where to read the bytes from.</param>
        /// <param name="count">Number of bytes to read.</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>Array of size <paramref name="count"/> with the bytes read.</returns>
        byte[] ReadBytes(IntPtr address, int count, bool relative = false);

        /// <summary>
        /// Writes bytes to the remote process at a specified address.
        /// </summary>
        /// <param name="address">Address where to write the bytes to.</param>
        /// <param name="bytes">Bytes to write.</param>
        /// <param name="relative">If the address is relative or not.</param>
        void WriteBytes(IntPtr address, byte[] bytes, bool relative = false);
    }
}
