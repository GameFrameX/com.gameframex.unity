using System;
using System.Buffers.Binary;
using System.Text;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 字节数组缓冲区扩展方法。
    /// </summary>
    /// <remarks>
    /// Extension methods for byte array buffer operations.
    /// </remarks>
    [Preserve]
    public static class BufferExtension
    {
        /// <summary>
        /// 整型的大小
        /// </summary>
        /// <remarks>The size of an integer in bytes.</remarks>
        public const int IntSize = sizeof(int);

        /// <summary>
        /// 无符号整型的大小
        /// </summary>
        /// <remarks>The size of an unsigned integer in bytes.</remarks>
        public const int UIntSize = sizeof(uint);

        /// <summary>
        /// 短整型的大小
        /// </summary>
        /// <remarks>The size of a short integer in bytes.</remarks>
        public const int ShortSize = sizeof(short);

        /// <summary>
        /// 无符号短整型的大小
        /// </summary>
        /// <remarks>The size of an unsigned short integer in bytes.</remarks>
        public const int UShortSize = sizeof(ushort);

        /// <summary>
        /// 长整型的大小
        /// </summary>
        /// <remarks>The size of a long integer in bytes.</remarks>
        public const int LongSize = sizeof(long);

        /// <summary>
        /// 单精度浮点数的大小
        /// </summary>
        /// <remarks>The size of a single-precision floating-point number in bytes.</remarks>
        public const int FloatSize = sizeof(float);

        /// <summary>
        /// 双精度浮点数的大小
        /// </summary>
        /// <remarks>The size of a double-precision floating-point number in bytes.</remarks>
        public const int DoubleSize = sizeof(double);

        /// <summary>
        /// 字节的大小
        /// </summary>
        /// <remarks>The size of a byte in bytes.</remarks>
        public const int ByteSize = sizeof(byte);

        /// <summary>
        /// 有符号字节的大小
        /// </summary>
        /// <remarks>The size of a signed byte in bytes.</remarks>
        public const int SbyteSize = sizeof(sbyte);

        /// <summary>
        /// 布尔值的大小
        /// </summary>
        /// <remarks>The size of a boolean value in bytes.</remarks>
        public const int BoolSize = sizeof(bool);


        #region Write

        /// <summary>
        /// 将整数写入字节数组中的指定偏移量处。
        /// </summary>
        /// <remarks>
        /// Writes an integer to the specified offset in the byte array.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组 / The byte array to write to.</param>
        /// <param name="value">要写入的整数值 / The integer value to write.</param>
        /// <param name="offset">写入操作的偏移量 / The offset for the write operation.</param>
        [Preserve]
        public static unsafe void WriteInt(this byte[] buffer, int value, ref int offset)
        {
            if (offset + IntSize > buffer.Length)
            {
                offset += IntSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(int*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += IntSize;
            }
        }

        /// <summary>
        /// 将无符号整数写入字节数组中的指定偏移量处。
        /// </summary>
        /// <remarks>
        /// Writes an unsigned integer to the specified offset in the byte array.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组 / The byte array to write to.</param>
        /// <param name="value">要写入的整数值 / The unsigned integer value to write.</param>
        /// <param name="offset">写入操作的偏移量 / The offset for the write operation.</param>
        [Preserve]
        public static unsafe void WriteUInt(this byte[] buffer, uint value, ref int offset)
        {
            if (offset + IntSize > buffer.Length)
            {
                offset += IntSize;
                return;
            }

            var span = buffer.AsSpan<byte>();
            ref var local = ref span;
            int start = offset;
            BinaryPrimitives.WriteUInt32BigEndian(local.Slice(start, local.Length - start), value);
            offset += IntSize;
        }

        /// <summary>
        /// 将一个16位无符号整数写入指定的缓冲区，并更新偏移量。
        /// </summary>
        /// <remarks>
        /// Writes a 16-bit unsigned integer to the specified buffer and updates the offset.
        /// </remarks>
        /// <param name="buffer">要写入的缓冲区 / The buffer to write to.</param>
        /// <param name="value">要写入的值 / The value to write.</param>
        /// <param name="offset">要写入值的缓冲区中的偏移量 / The offset in the buffer to write the value.</param>
        [Preserve]
        public static void WriteUShort(this byte[] buffer, ushort value, ref int offset)
        {
            if (offset + 2 > buffer.Length)
            {
                offset += 2;
            }
            else
            {
                Span<byte> span = buffer.AsSpan<byte>();
                ref Span<byte> local = ref span;
                int start = offset;
                BinaryPrimitives.WriteUInt16BigEndian(local.Slice(start, local.Length - start), value);
                offset += 2;
            }
        }

        /// <summary>
        /// 将短整数写入字节数组中的指定偏移量处。
        /// </summary>
        /// <remarks>
        /// Writes a short integer to the specified offset in the byte array.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组 / The byte array to write to.</param>
        /// <param name="value">要写入的短整数值 / The short integer value to write.</param>
        /// <param name="offset">写入操作的偏移量 / The offset for the write operation.</param>
        [Preserve]
        public static unsafe void WriteShort(this byte[] buffer, short value, ref int offset)
        {
            if (offset + ShortSize > buffer.Length)
            {
                offset += ShortSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(short*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += ShortSize;
            }
        }

        /// <summary>
        /// 将长整数写入字节数组中的指定偏移量处。
        /// </summary>
        /// <remarks>
        /// Writes a long integer to the specified offset in the byte array.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组 / The byte array to write to.</param>
        /// <param name="value">要写入的长整数值 / The long integer value to write.</param>
        /// <param name="offset">写入操作的偏移量 / The offset for the write operation.</param>
        [Preserve]
        public static unsafe void WriteLong(this byte[] buffer, long value, ref int offset)
        {
            if (offset + LongSize > buffer.Length)
            {
                offset += LongSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(long*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += LongSize;
            }
        }

        /// <summary>
        /// 将单精度浮点数写入字节数组中的指定偏移量处。
        /// </summary>
        /// <remarks>
        /// Writes a single-precision floating-point number to the specified offset in the byte array.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组 / The byte array to write to.</param>
        /// <param name="value">要写入的单精度浮点数值 / The single-precision floating-point value to write.</param>
        /// <param name="offset">字节数组中的偏移量，传递引用以便更新偏移量 / The offset in the byte array, passed by reference to update the offset.</param>
        [Preserve]
        public static unsafe void WriteFloat(this byte[] buffer, float value, ref int offset)
        {
            if (offset + FloatSize > buffer.Length)
            {
                offset += FloatSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(float*)(ptr + offset) = value;
                *(int*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(*(int*)(ptr + offset));
                offset += FloatSize;
            }
        }

        /// <summary>
        /// 将双精度浮点数写入字节数组中的指定偏移量处。
        /// </summary>
        /// <remarks>
        /// Writes a double-precision floating-point number to the specified offset in the byte array.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组 / The byte array to write to.</param>
        /// <param name="value">要写入的双精度浮点数值 / The double-precision floating-point value to write.</param>
        /// <param name="offset">字节数组中的偏移量，传递引用以便更新偏移量 / The offset in the byte array, passed by reference to update the offset.</param>
        [Preserve]
        public static unsafe void WriteDouble(this byte[] buffer, double value, ref int offset)
        {
            if (offset + DoubleSize > buffer.Length)
            {
                offset += DoubleSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(double*)(ptr + offset) = value;
                *(long*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(*(long*)(ptr + offset));
                offset += DoubleSize;
            }
        }

        /// <summary>
        /// 将字节写入字节数组中的指定偏移量处。
        /// </summary>
        /// <remarks>
        /// Writes a byte to the specified offset in the byte array.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组 / The byte array to write to.</param>
        /// <param name="value">要写入的字节值 / The byte value to write.</param>
        /// <param name="offset">字节数组中的偏移量，传递引用以便更新偏移量 / The offset in the byte array, passed by reference to update the offset.</param>
        [Preserve]
        public static unsafe void WriteByte(this byte[] buffer, byte value, ref int offset)
        {
            if (offset + ByteSize > buffer.Length)
            {
                offset += ByteSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(ptr + offset) = value;
                offset += ByteSize;
            }
        }

        /// <summary>
        /// 在给定的偏移量位置，向缓冲区中写入字节序列，不包含长度信息。
        /// </summary>
        /// <remarks>
        /// Writes a byte sequence to the buffer at the specified offset without length information.
        /// </remarks>
        /// <param name="buffer">目标缓冲区 / The target buffer.</param>
        /// <param name="value">要写入的字节数组 / The byte array to write.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        [Preserve]
        public static unsafe void WriteBytesWithoutLength(this byte[] buffer, byte[] value, ref int offset)
        {
            if (value == null)
            {
                buffer.WriteInt(0, ref offset);
                return;
            }

            if (offset + value.Length + IntSize > buffer.Length)
            {
                throw new ArgumentException($"buffer write out of index {offset + value.Length + IntSize}, {buffer.Length}");
            }

            fixed (byte* ptr = buffer, valPtr = value)
            {
                Buffer.MemoryCopy(valPtr, ptr + offset, value.Length, value.Length);
                offset += value.Length;
            }
        }

        /// <summary>
        /// 将字节数组写入到缓冲区中，同时更新偏移量。
        /// </summary>
        /// <remarks>
        /// Writes a byte array to the buffer and updates the offset.
        /// </remarks>
        /// <param name="buffer">目标缓冲区 / The target buffer.</param>
        /// <param name="value">要写入的字节数组 / The byte array to write.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        [Preserve]
        public static unsafe void WriteBytes(this byte[] buffer, byte[] value, ref int offset)
        {
            if (value == null)
            {
                buffer.WriteInt(0, ref offset);
                return;
            }

            if (offset + value.Length + IntSize > buffer.Length)
            {
                offset += value.Length + IntSize;
                return;
            }

            buffer.WriteInt(value.Length, ref offset);
            System.Array.Copy(value, 0, buffer, offset, value.Length);
            offset += value.Length;
        }

        /// <summary>
        /// 将有符号字节写入到缓冲区中，同时更新偏移量。
        /// </summary>
        /// <remarks>
        /// Writes a signed byte to the buffer and updates the offset.
        /// </remarks>
        /// <param name="buffer">目标缓冲区 / The target buffer.</param>
        /// <param name="value">要写入的有符号字节 / The signed byte to write.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        [Preserve]
        public static unsafe void WriteSByte(this byte[] buffer, sbyte value, ref int offset)
        {
            if (offset + SbyteSize > buffer.Length)
            {
                offset += SbyteSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(sbyte*)(ptr + offset) = value;
                offset += SbyteSize;
            }
        }

        /// <summary>
        /// 将字符串写入到缓冲区中，同时更新偏移量。
        /// </summary>
        /// <remarks>
        /// Writes a string to the buffer and updates the offset.
        /// </remarks>
        /// <param name="buffer">目标缓冲区 / The target buffer.</param>
        /// <param name="value">要写入的字符串 / The string to write.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        [Preserve]
        public static unsafe void WriteString(this byte[] buffer, string value, ref int offset)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            int len = System.Text.Encoding.UTF8.GetByteCount(value);

            if (len > short.MaxValue)
            {
                throw new ArgumentException($"字符串长度超过了 short.MaxValue {len}, {short.MaxValue}");
            }

            // 预判已经超出长度了，直接计算长度就行了
            if (offset + len + ShortSize > buffer.Length)
            {
                offset += len + ShortSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                System.Text.Encoding.UTF8.GetBytes(value, 0, value.Length, buffer, offset + ShortSize);
                buffer.WriteShort((short)len, ref offset);
                offset += len;
            }
        }

        /// <summary>
        /// 将布尔值写入到缓冲区中，同时更新偏移量。
        /// </summary>
        /// <remarks>
        /// Writes a boolean value to the buffer and updates the offset.
        /// </remarks>
        /// <param name="buffer">目标缓冲区 / The target buffer.</param>
        /// <param name="value">要写入的布尔值 / The boolean value to write.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        [Preserve]
        public static unsafe void WriteBool(this byte[] buffer, bool value, ref int offset)
        {
            if (offset + BoolSize > buffer.Length)
            {
                offset += BoolSize;
                return;
            }

            fixed (byte* ptr = buffer)
            {
                *(bool*)(ptr + offset) = value;
                offset += BoolSize;
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// 从字节数组中读取一个整数值。
        /// </summary>
        /// <remarks>
        /// Reads an integer value from the byte array.
        /// </remarks>
        /// <param name="buffer">包含整数值的字节数组 / The byte array containing the integer value.</param>
        /// <param name="offset">从字节数组中读取整数值的偏移量 / The offset to read the integer value from the byte array.</param>
        /// <returns>从字节数组中读取的整数值 / The integer value read from the byte array.</returns>
        [Preserve]
        public static unsafe int ReadInt(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + IntSize)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "buffer read out of index");
            }

            fixed (byte* ptr = buffer)
            {
                var value = *(int*)(ptr + offset);
                offset += IntSize;
                return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        /// <summary>
        /// 从字节数组中读取一个无符号整数值。
        /// </summary>
        /// <remarks>
        /// Reads an unsigned integer value from the byte array.
        /// </remarks>
        /// <param name="buffer">包含整数值的字节数组 / The byte array containing the integer value.</param>
        /// <param name="offset">从字节数组中读取整数值的偏移量 / The offset to read the integer value from the byte array.</param>
        /// <returns>从字节数组中读取的无符号整数值 / The unsigned integer value read from the byte array.</returns>
        /// <exception cref="Exception"></exception>
        [Preserve]
        public static uint ReadUInt(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + UIntSize)
            {
                throw new Exception("buffer read out of index");
            }

            Span<byte> span = buffer.AsSpan<byte>();
            ref Span<byte> local = ref span;
            int start = offset;
            int num = (int)BinaryPrimitives.ReadUInt32BigEndian((ReadOnlySpan<byte>)local.Slice(start, local.Length - start));
            offset += UIntSize;
            return (uint)num;
        }

        /// <summary>
        /// 从字节数组中读取一个短整数值。
        /// </summary>
        /// <remarks>
        /// Reads a short integer value from the byte array.
        /// </remarks>
        /// <param name="buffer">包含短整数值的字节数组 / The byte array containing the short integer value.</param>
        /// <param name="offset">从字节数组中读取短整数值的偏移量 / The offset to read the short integer value from the byte array.</param>
        /// <returns>从字节数组中读取的短整数值 / The short integer value read from the byte array.</returns>
        [Preserve]
        public static unsafe short ReadShort(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + ShortSize)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "buffer read out of index");
            }

            fixed (byte* ptr = buffer)
            {
                var value = *(short*)(ptr + offset);
                offset += ShortSize;
                return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        /// <summary>
        /// 从字节数组中读取16位无符号整数，并将偏移量向前移动。
        /// </summary>
        /// <remarks>
        /// Reads a 16-bit unsigned integer from the byte array and advances the offset.
        /// </remarks>
        /// <param name="buffer">要读取的字节数组 / The byte array to read from.</param>
        /// <param name="offset">引用偏移量 / The reference offset.</param>
        /// <returns>返回读取的16位无符号整数 / The 16-bit unsigned integer read.</returns>
        [Preserve]
        public static ushort ReadUShort(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + UShortSize)
            {
                throw new Exception("buffer read out of index");
            }

            Span<byte> span = buffer.AsSpan<byte>();
            ref Span<byte> local = ref span;
            int start = offset;
            int num = (int)BinaryPrimitives.ReadUInt16BigEndian((ReadOnlySpan<byte>)local.Slice(start, local.Length - start));
            offset += UShortSize;
            return (ushort)num;
        }

        /// <summary>
        /// 从字节数组中读取一个长整型数值。
        /// </summary>
        /// <remarks>
        /// Reads a long integer value from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <returns>长整型数值 / The long integer value.</returns>
        [Preserve]
        public static unsafe long ReadLong(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + LongSize)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "buffer read out of index");
            }

            fixed (byte* ptr = buffer)
            {
                var value = *(long*)(ptr + offset);
                offset += LongSize;
                return System.Net.IPAddress.NetworkToHostOrder(value);
            }
        }

        /// <summary>
        /// 从字节数组中读取一个单精度浮点数值。
        /// </summary>
        /// <remarks>
        /// Reads a single-precision floating-point value from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <returns>单精度浮点数值 / The single-precision floating-point value.</returns>
        [Preserve]
        public static unsafe float ReadFloat(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + FloatSize)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "buffer read out of index");
            }

            fixed (byte* ptr = buffer)
            {
                *(int*)(ptr + offset) = System.Net.IPAddress.NetworkToHostOrder(*(int*)(ptr + offset));
                var value = *(float*)(ptr + offset);
                offset += FloatSize;
                return value;
            }
        }

        /// <summary>
        /// 从字节数组中读取一个双精度浮点数值。
        /// </summary>
        /// <remarks>
        /// Reads a double-precision floating-point value from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <returns>双精度浮点数值 / The double-precision floating-point value.</returns>
        [Preserve]
        public static unsafe double ReadDouble(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + DoubleSize)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "buffer read out of index");
            }

            fixed (byte* ptr = buffer)
            {
                *(long*)(ptr + offset) = System.Net.IPAddress.NetworkToHostOrder(*(long*)(ptr + offset));
                var value = *(double*)(ptr + offset);
                offset += DoubleSize;
                return value;
            }
        }

        /// <summary>
        /// 从字节数组中读取一个字节。
        /// </summary>
        /// <remarks>
        /// Reads a byte from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <returns>字节值 / The byte value.</returns>
        [Preserve]
        public static unsafe byte ReadByte(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + ByteSize)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "buffer read out of index");
            }

            fixed (byte* ptr = buffer)
            {
                var value = *(ptr + offset);
                offset += ByteSize;
                return value;
            }
        }

        /// <summary>
        /// 从字节数组中读取一定长度的字节。
        /// </summary>
        /// <remarks>
        /// Reads a specified length of bytes from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <param name="len">数据长度 / The data length.</param>
        /// <returns>读取的字节数组 / The byte array read.</returns>
        [Preserve]
        public static unsafe byte[] ReadBytes(this byte[] buffer, int offset, int len)
        {
            //数据不可信
            if (len <= 0 || offset > buffer.Length + len * ByteSize)
            {
                return Array.Empty<byte>();
            }

            var data = new byte[len];
            System.Array.Copy(buffer, offset, data, 0, len);
            return data;
        }

        /// <summary>
        /// 从字节数组中读取一定长度的字节。
        /// </summary>
        /// <remarks>
        /// Reads a specified length of bytes from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <param name="len">数据长度 / The data length.</param>
        /// <returns>读取的字节数组 / The byte array read.</returns>
        [Preserve]
        public static unsafe byte[] ReadBytes(this byte[] buffer, ref int offset, int len)
        {
            //数据不可信
            if (len <= 0 || offset > buffer.Length + len * ByteSize)
            {
                return Array.Empty<byte>();
            }

            var data = new byte[len];
            System.Array.Copy(buffer, offset, data, 0, len);
            offset += len;
            return data;
        }

        /// <summary>
        /// 从字节数组中读取一定长度的字节。
        /// </summary>
        /// <remarks>
        /// Reads a length-prefixed byte array from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <returns>读取的字节数组 / The byte array read.</returns>
        [Preserve]
        public static unsafe byte[] ReadBytes(this byte[] buffer, ref int offset)
        {
            var len = ReadInt(buffer, ref offset);
            //数据不可信
            if (len <= 0 || offset > buffer.Length + len * ByteSize)
            {
                return Array.Empty<byte>();
            }

            var data = new byte[len];
            System.Array.Copy(buffer, offset, data, 0, len);
            offset += len;
            return data;
        }

        /// <summary>
        /// 从字节数组中读取有符号字节。
        /// </summary>
        /// <remarks>
        /// Reads a signed byte from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <returns>读取的有符号字节 / The signed byte read.</returns>
        [Preserve]
        public static unsafe sbyte ReadSByte(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + ByteSize)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "buffer read out of index");
            }

            fixed (byte* ptr = buffer)
            {
                var value = *(sbyte*)(ptr + offset);
                offset += ByteSize;
                return value;
            }
        }

        /// <summary>
        /// 从字节数组中读取字符串。
        /// </summary>
        /// <remarks>
        /// Reads a string from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <returns>读取的字符串 / The string read.</returns>
        [Preserve]
        public static unsafe string ReadString(this byte[] buffer, ref int offset)
        {
            fixed (byte* ptr = buffer)
            {
                var len = ReadShort(buffer, ref offset);
                //数据不可信
                if (len <= 0 || offset > buffer.Length + len * ByteSize)
                    return "";

                var value = System.Text.Encoding.UTF8.GetString(buffer, offset, len);
                offset += len;
                return value;
            }
        }

        /// <summary>
        /// 从字节数组中读取布尔值。
        /// </summary>
        /// <remarks>
        /// Reads a boolean value from the byte array.
        /// </remarks>
        /// <param name="buffer">字节数组 / The byte array.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        /// <returns>读取的布尔值 / The boolean value read.</returns>
        [Preserve]
        public static unsafe bool ReadBool(this byte[] buffer, ref int offset)
        {
            if (offset > buffer.Length + BoolSize)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "buffer read out of index");
            }

            fixed (byte* ptr = buffer)
            {
                var value = *(bool*)(ptr + offset);
                offset += BoolSize;
                return value;
            }
        }

        #endregion

        /// <summary>
        /// 将字节数组转换为字符串
        /// </summary>
        /// <remarks>
        /// Converts a byte array to a string representation.
        /// </remarks>
        /// <param name="bytes">要转换的字节数组 / The byte array to convert.</param>
        /// <returns>转换后的字符串 / The converted string.</returns>
        [Preserve]
        public static string ToArrayString(this byte[] bytes)
        {
            StringBuilder.Clear();
            foreach (byte b in bytes)
            {
                StringBuilder.Append(b + " ");
            }

            return StringBuilder.ToString();
        }

        private static readonly StringBuilder StringBuilder = new StringBuilder();

        /// <summary>
        /// 将字节转换为十六进制字符串。
        /// </summary>
        /// <remarks>
        /// Converts a byte to a hexadecimal string.
        /// </remarks>
        /// <param name="b">要转换的字节 / The byte to convert.</param>
        /// <returns>表示字节的十六进制字符串 / The hexadecimal string representation of the byte.</returns>
        [Preserve]
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串。
        /// </summary>
        /// <remarks>
        /// Converts a byte array to a hexadecimal string.
        /// </remarks>
        /// <param name="bytes">要转换的字节数组 / The byte array to convert.</param>
        /// <returns>表示字节数组的十六进制字符串 / The hexadecimal string representation of the byte array.</returns>
        [Preserve]
        public static string ToHex(this byte[] bytes)
        {
            StringBuilder.Clear();
            foreach (byte b in bytes)
            {
                StringBuilder.Append(b.ToString("X2"));
            }

            return StringBuilder.ToString();
        }

        /// <summary>
        /// 使用指定格式将字节数组转换为十六进制字符串。
        /// </summary>
        /// <remarks>
        /// Converts a byte array to a hexadecimal string using the specified format.
        /// </remarks>
        /// <param name="bytes">要转换的字节数组 / The byte array to convert.</param>
        /// <param name="format">十六进制格式 / The hexadecimal format.</param>
        /// <returns>表示字节数组的十六进制字符串 / The hexadecimal string representation of the byte array.</returns>
        [Preserve]
        public static string ToHex(this byte[] bytes, string format)
        {
            StringBuilder.Clear();
            foreach (byte b in bytes)
            {
                StringBuilder.Append(b.ToString(format));
            }

            return StringBuilder.ToString();
        }

        /// <summary>
        /// 将字节数组中指定范围的字节转换为十六进制字符串。
        /// </summary>
        /// <remarks>
        /// Converts a specified range of bytes in the byte array to a hexadecimal string.
        /// </remarks>
        /// <param name="bytes">要转换的字节数组 / The byte array to convert.</param>
        /// <param name="offset">起始偏移量 / The starting offset.</param>
        /// <param name="count">要转换的字节数 / The number of bytes to convert.</param>
        /// <returns>表示指定范围内字节的十六进制字符串 / The hexadecimal string representation of the specified range of bytes.</returns>
        [Preserve]
        public static string ToHex(this byte[] bytes, int offset, int count)
        {
            StringBuilder.Clear();
            for (int i = offset; i < offset + count; ++i)
            {
                StringBuilder.Append(bytes[i].ToString("X2"));
            }

            return StringBuilder.ToString();
        }

        /// <summary>
        /// 将字节数组转换为字符串，使用默认编码。
        /// </summary>
        /// <remarks>
        /// Converts a byte array to a string using the default encoding.
        /// </remarks>
        /// <param name="bytes">要转换的字节数组 / The byte array to convert.</param>
        /// <returns>转换后的字符串 / The converted string.</returns>
        [Preserve]
        public static string ToDefaultString(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 将字节数组的一部分转换为字符串，使用默认编码。
        /// </summary>
        /// <remarks>
        /// Converts a portion of a byte array to a string using the default encoding.
        /// </remarks>
        /// <param name="bytes">要转换的字节数组 / The byte array to convert.</param>
        /// <param name="index">起始位置 / The starting position.</param>
        /// <param name="count">要转换的字节数 / The number of bytes to convert.</param>
        /// <returns>转换后的字符串 / The converted string.</returns>
        [Preserve]
        public static string ToDefaultString(this byte[] bytes, int index, int count)
        {
            return Encoding.Default.GetString(bytes, index, count);
        }

        /// <summary>
        /// 将字节数组转换为字符串，使用UTF-8编码。
        /// </summary>
        /// <remarks>
        /// Converts a byte array to a string using UTF-8 encoding.
        /// </remarks>
        /// <param name="bytes">要转换的字节数组 / The byte array to convert.</param>
        /// <returns>转换后的字符串 / The converted string.</returns>
        [Preserve]
        public static string ToUtf8String(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 将字节数组的一部分转换为字符串，使用UTF-8编码。
        /// </summary>
        /// <remarks>
        /// Converts a portion of a byte array to a string using UTF-8 encoding.
        /// </remarks>
        /// <param name="bytes">要转换的字节数组 / The byte array to convert.</param>
        /// <param name="index">起始位置 / The starting position.</param>
        /// <param name="count">要转换的字节数 / The number of bytes to convert.</param>
        /// <returns>转换后的字符串 / The converted string.</returns>
        [Preserve]
        public static string ToUtf8String(this byte[] bytes, int index, int count)
        {
            return Encoding.UTF8.GetString(bytes, index, count);
        }
    }
}