using System;
using System.Runtime.InteropServices;

namespace GameFrameX.Runtime
{
        /// <summary>
        /// 提供针对字节数据范围的二进制读写扩展方法。
        /// </summary>
        /// <remarks>
        /// Provides binary read and write extension methods for byte spans.
        /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class SpanExtension
    {
        /// <summary>
        /// 整型的大小
        /// </summary>
        /// <remarks>
        /// The size of an integer, in bytes.
        /// </remarks>
        public const int IntSize = sizeof(int);

        /// <summary>
        /// 短整型的大小
        /// </summary>
        /// <remarks>
        /// The size of a short integer, in bytes.
        /// </remarks>
        public const int ShortSize = sizeof(short);

        /// <summary>
        /// 长整型的大小
        /// </summary>
        /// <remarks>
        /// The size of a long integer, in bytes.
        /// </remarks>
        public const int LongSize = sizeof(long);

        /// <summary>
        /// 单精度浮点数的大小
        /// </summary>
        /// <remarks>
        /// The size of a single-precision floating-point number, in bytes.
        /// </remarks>
        public const int FloatSize = sizeof(float);

        /// <summary>
        /// 双精度浮点数的大小
        /// </summary>
        /// <remarks>
        /// The size of a double-precision floating-point number, in bytes.
        /// </remarks>
        public const int DoubleSize = sizeof(double);

        /// <summary>
        /// 字节的大小
        /// </summary>
        /// <remarks>
        /// The size of a byte.
        /// </remarks>
        public const int ByteSize = sizeof(byte);

        /// <summary>
        /// 有符号字节的大小
        /// </summary>
        /// <remarks>
        /// The size of a signed byte.
        /// </remarks>
        public const int SbyteSize = sizeof(sbyte);

        /// <summary>
        /// 布尔值的大小
        /// </summary>
        /// <remarks>
        /// The size of a boolean value, in bytes.
        /// </remarks>
        public const int BoolSize = sizeof(bool);


        #region WriteSpan

        /// <summary>
        /// 将一个整数写入到字节数组中。
        /// </summary>
        /// <remarks>
        /// Writes an integer to the byte span.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组。 / The byte span to write to.</param>
        /// <param name="value">要写入的整数值。 / The integer value to write.</param>
        /// <param name="offset">写入操作的偏移量。 / The offset of the write operation.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteInt(this Span<byte> buffer, int value, ref int offset)
        {
            if (offset < 0 || offset + IntSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + IntSize, buffer.Length));
            }

            fixed (byte* ptr = buffer)
            {
                *(int*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += IntSize;
            }
        }

        /// <summary>
        /// 将一个短整数写入到字节数组中。
        /// </summary>
        /// <remarks>
        /// Writes a short integer to the byte span.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组。 / The byte span to write to.</param>
        /// <param name="value">要写入的短整数值。 / The short integer value to write.</param>
        /// <param name="offset">写入操作的偏移量。 / The offset of the write operation.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteShort(this Span<byte> buffer, short value, ref int offset)
        {
            if (offset < 0 || offset + ShortSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + ShortSize, buffer.Length));
            }

            fixed (byte* ptr = buffer)
            {
                *(short*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += ShortSize;
            }
        }

        /// <summary>
        /// 将一个长整数写入到字节数组中。
        /// </summary>
        /// <remarks>
        /// Writes a long integer to the byte span.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组。 / The byte span to write to.</param>
        /// <param name="value">要写入的长整数值。 / The long integer value to write.</param>
        /// <param name="offset">写入操作的偏移量。 / The offset of the write operation.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteLong(this Span<byte> buffer, long value, ref int offset)
        {
            if (offset < 0 || offset + LongSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + LongSize, buffer.Length));
            }

            fixed (byte* ptr = buffer)
            {
                *(long*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(value);
                offset += LongSize;
            }
        }

        /// <summary>
        /// 将一个浮点数写入到字节数组中。
        /// </summary>
        /// <remarks>
        /// Writes a floating-point number to the byte span.
        /// </remarks>
        /// <param name="buffer">要写入的字节数组。 / The byte span to write to.</param>
        /// <param name="value">要写入的浮点数值。 / The floating-point value to write.</param>
        /// <param name="offset">写入操作的偏移量。 / The offset of the write operation.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteFloat(this Span<byte> buffer, float value, ref int offset)
        {
            if (offset < 0 || offset + FloatSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + FloatSize, buffer.Length));
            }

            fixed (byte* ptr = buffer)
            {
                *(float*)(ptr + offset) = value;
                *(int*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(*(int*)(ptr + offset));
                offset += FloatSize;
            }
        }

        /// <summary>
        /// 将双精度浮点数写入缓冲区。
        /// </summary>
        /// <remarks>
        /// Writes a double-precision floating-point number to the buffer.
        /// </remarks>
        /// <param name="buffer">要写入的字节范围。 / The byte span to write to.</param>
        /// <param name="value">要写入的双精度浮点数值。 / The double-precision floating-point value to write.</param>
        /// <param name="offset">偏移量，指示从何处开始写入。 / The offset indicating where to start writing.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteDouble(this Span<byte> buffer, double value, ref int offset)
        {
            if (offset < 0 || offset + DoubleSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + DoubleSize, buffer.Length));
            }

            fixed (byte* ptr = buffer)
            {
                *(double*)(ptr + offset) = value;
                *(long*)(ptr + offset) = System.Net.IPAddress.HostToNetworkOrder(*(long*)(ptr + offset));
                offset += DoubleSize;
            }
        }

        /// <summary>
        /// 将字节写入缓冲区。
        /// </summary>
        /// <remarks>
        /// Writes a byte to the buffer.
        /// </remarks>
        /// <param name="buffer">要写入的字节范围。 / The byte span to write to.</param>
        /// <param name="value">要写入的字节值。 / The byte value to write.</param>
        /// <param name="offset">偏移量，指示从何处开始写入。 / The offset indicating where to start writing.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteByte(this Span<byte> buffer, byte value, ref int offset)
        {
            if (offset < 0 || offset + ByteSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + ByteSize, buffer.Length));
            }

            fixed (byte* ptr = buffer)
            {
                *(ptr + offset) = value;
                offset += ByteSize;
            }
        }

        /// <summary>
        /// 将字节数组写入缓冲区。
        /// </summary>
        /// <remarks>
        /// Writes a byte array, including its length, to the buffer.
        /// </remarks>
        /// <param name="buffer">要写入的字节范围。 / The byte span to write to.</param>
        /// <param name="value">要写入的字节数组。 / The byte array to write.</param>
        /// <param name="offset">偏移量，指示从何处开始写入。 / The offset indicating where to start writing.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteBytes(this Span<byte> buffer, byte[] value, ref int offset)
        {
            if (value == null)
            {
                buffer.WriteInt(0, ref offset);
                return;
            }

            if (offset < 0 || offset + value.Length + IntSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + value.Length + IntSize, buffer.Length));
            }

            buffer.WriteInt(value.Length, ref offset);
            //System.Array.Copy(value, 0, buffer, offset, value.Length);
            //offset += value.Length;
            fixed (byte* ptr = buffer, valPtr = value)
            {
                Buffer.MemoryCopy(valPtr, ptr + offset, value.Length, value.Length);
                offset += value.Length;
            }
        }

        /// <summary>
        /// 将字节数组写入缓冲区，不包括长度信息。
        /// </summary>
        /// <remarks>
        /// Writes a byte array to the buffer without length information.
        /// </remarks>
        /// <param name="buffer">要写入的字节范围。 / The byte span to write to.</param>
        /// <param name="value">要写入的字节数组。 / The byte array to write.</param>
        /// <param name="offset">偏移量，指示从缓冲区的哪个位置开始写入。 / The offset indicating where in the buffer to start writing.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteBytesWithoutLength(this Span<byte> buffer, byte[] value, ref int offset)
        {
            if (value == null)
            {
                buffer.WriteInt(0, ref offset);
                return;
            }

            if (offset < 0 || offset + value.Length + IntSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + value.Length + IntSize, buffer.Length));
            }

            fixed (byte* ptr = buffer, valPtr = value)
            {
                Buffer.MemoryCopy(valPtr, ptr + offset, value.Length, value.Length);
                offset += value.Length;
            }
        }

        /// <summary>
        /// 将有符号字节写入缓冲区。
        /// </summary>
        /// <remarks>
        /// Writes a signed byte to the buffer.
        /// </remarks>
        /// <param name="buffer">要写入的字节范围。 / The byte span to write to.</param>
        /// <param name="value">要写入的有符号字节值。 / The signed byte value to write.</param>
        /// <param name="offset">偏移量，指示从缓冲区的哪个位置开始写入。 / The offset indicating where in the buffer to start writing.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteSByte(this Span<byte> buffer, sbyte value, ref int offset)
        {
            if (offset < 0 || offset + SbyteSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + SbyteSize, buffer.Length));
            }

            fixed (byte* ptr = buffer)
            {
                *(sbyte*)(ptr + offset) = value;
                offset += SbyteSize;
            }
        }

        /// <summary>
        /// 将字符串写入缓冲区
        /// </summary>
        /// <remarks>
        /// Writes a string to the buffer.
        /// </remarks>
        /// <param name="buffer">要写入的缓冲区 / The buffer to write to.</param>
        /// <param name="value">要写入的字符串 / The string to write.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteString(this Span<byte> buffer, string value, ref int offset)
        {
            if (value == null)
            {
                value = string.Empty;
            }

            int len = System.Text.Encoding.UTF8.GetByteCount(value);
            if (len > short.MaxValue)
            {
                throw new ArgumentException($"string length exceed short.MaxValue {len}, {short.MaxValue}");
            }

            //预判已经超出长度了，直接计算长度就行了
            if (offset < 0 || offset + len + ShortSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + len + ShortSize, buffer.Length));
            }

            buffer.WriteShort((short)len, ref offset);
            var val = System.Text.Encoding.UTF8.GetBytes(value);
            fixed (byte* ptr = buffer, valPtr = val)
            {
                //System.Text.Encoding.UTF8.GetBytes(value, 0, value.Length, buffer, offset + ShortSize);
                //WriteShort((short)len, buffer, ref offset);
                //offset += len;
                Buffer.MemoryCopy(valPtr, ptr + offset, len, len);
                offset += len;
            }
        }

        /// <summary>
        /// 将布尔值写入缓冲区
        /// </summary>
        /// <remarks>
        /// Writes a boolean value to the buffer.
        /// </remarks>
        /// <param name="buffer">要写入的缓冲区 / The buffer to write to.</param>
        /// <param name="value">要写入的布尔值 / The boolean value to write.</param>
        /// <param name="offset">偏移量 / The offset.</param>
        [UnityEngine.Scripting.Preserve]
        public static unsafe void WriteBool(this Span<byte> buffer, bool value, ref int offset)
        {
            if (offset < 0 || offset + BoolSize > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), string.Format("buffer write out of index {0}, {1}", offset + BoolSize, buffer.Length));
            }

            fixed (byte* ptr = buffer)
            {
                *(bool*)(ptr + offset) = value;
                offset += BoolSize;
            }
        }

        #endregion


        #region ReadSpan

        /// <summary>
        /// 从字节数组中读取一个整数。
        /// </summary>
        /// <remarks>
        /// Reads an integer from the byte span.
        /// </remarks>
        /// <param name="buffer">包含整数的字节数组。 / The byte span containing the integer.</param>
        /// <param name="offset">偏移量，指示从何处开始读取整数。 / The offset indicating where to start reading.</param>
        /// <returns>从字节数组中读取的整数。 / The integer read from the byte span.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe int ReadInt(this Span<byte> buffer, ref int offset)
        {
            if (offset < 0 || offset + IntSize > buffer.Length)
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
        /// 从指定的字节数组中读取一个 16 位有符号整数，并将偏移量向前移动 2 个字节。
        /// </summary>
        /// <remarks>
        /// Reads a 16-bit signed integer from the byte span.
        /// </remarks>
        /// <param name="buffer">包含要读取的数据的字节数组。 / The byte span containing the data to read.</param>
        /// <param name="offset">要开始读取的位置。读取完成后，此参数将包含新的偏移量值。 / The position to start reading from; contains the new offset after reading.</param>
        /// <returns>一个 16 位有符号整数。 / A 16-bit signed integer.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe short ReadShort(this Span<byte> buffer, ref int offset)
        {
            if (offset < 0 || offset + ShortSize > buffer.Length)
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
        /// 从字节数组中读取一个64位整数。
        /// </summary>
        /// <remarks>
        /// Reads a 64-bit integer from the byte span.
        /// </remarks>
        /// <param name="buffer">包含数据的字节数组。 / The byte span containing the data.</param>
        /// <param name="offset">偏移量，指示从何处开始读取。 / The offset indicating where to start reading.</param>
        /// <returns>读取的64位整数。 / The 64-bit integer read.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe long ReadLong(this Span<byte> buffer, ref int offset)
        {
            if (offset < 0 || offset + LongSize > buffer.Length)
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
        /// 从字节数组中读取一个单精度浮点数。
        /// </summary>
        /// <remarks>
        /// Reads a single-precision floating-point number from the byte span.
        /// </remarks>
        /// <param name="buffer">包含数据的字节数组。 / The byte span containing the data.</param>
        /// <param name="offset">偏移量，指示从何处开始读取。 / The offset indicating where to start reading.</param>
        /// <returns>读取的单精度浮点数。 / The single-precision floating-point number read.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe float ReadFloat(this Span<byte> buffer, ref int offset)
        {
            if (offset < 0 || offset + FloatSize > buffer.Length)
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
        /// 从字节数组中读取一个双精度浮点数。
        /// </summary>
        /// <remarks>
        /// Reads a double-precision floating-point number from the byte span.
        /// </remarks>
        /// <param name="buffer">包含数据的 Span&lt;byte&gt; 对象。 / The Span&lt;byte&gt; containing the data.</param>
        /// <param name="offset">从字节数组开始读取的偏移量。 / The offset to start reading from.</param>
        /// <returns>从字节数组中读取的双精度浮点数。 / The double-precision floating-point number read from the byte span.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe double ReadDouble(this Span<byte> buffer, ref int offset)
        {
            if (offset < 0 || offset + DoubleSize > buffer.Length)
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
        /// Reads a byte from the byte span.
        /// </remarks>
        /// <param name="buffer">包含数据的 Span&lt;byte&gt; 对象。 / The Span&lt;byte&gt; containing the data.</param>
        /// <param name="offset">从字节数组开始读取的偏移量。 / The offset to start reading from.</param>
        /// <returns>从字节数组中读取的字节。 / The byte read from the byte span.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe byte ReadByte(this Span<byte> buffer, ref int offset)
        {
            if (offset < 0 || offset + ByteSize > buffer.Length)
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
        /// 从给定的缓冲区中读取字节数组。
        /// </summary>
        /// <remarks>
        /// Reads a byte array from the given buffer.
        /// </remarks>
        /// <param name="buffer">包含数据的 Span。 / The span containing the data.</param>
        /// <param name="offset">从缓冲区中读取数据的偏移量。 / The offset to read data from in the buffer.</param>
        /// <returns>从缓冲区中读取的字节数组。 / The byte array read from the buffer.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe byte[] ReadBytes(this Span<byte> buffer, ref int offset)
        {
            var len = ReadInt(buffer, ref offset);
            //数据不可信
            if (len <= 0 || offset < 0 || offset + len > buffer.Length)
            {
                return Array.Empty<byte>();
            }

            //var data = new byte[len];
            //System.Array.Copy(buffer, offset, data, 0, len);
            var data = buffer.Slice(offset, len).ToArray();
            offset += len;
            return data;
        }

        /// <summary>
        /// 从给定的缓冲区中读取有符号字节。
        /// </summary>
        /// <remarks>
        /// Reads a signed byte from the given buffer.
        /// </remarks>
        /// <param name="buffer">包含数据的 Span。 / The span containing the data.</param>
        /// <param name="offset">从缓冲区中读取数据的偏移量。 / The offset to read data from in the buffer.</param>
        /// <returns>从缓冲区中读取的有符号字节。 / The signed byte read from the buffer.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe sbyte ReadSByte(this Span<byte> buffer, ref int offset)
        {
            if (offset < 0 || offset + ByteSize > buffer.Length)
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
        /// 从字节范围中读取字符串。
        /// </summary>
        /// <remarks>
        /// Reads a string from the byte span.
        /// </remarks>
        /// <param name="buffer">包含字符串的字节范围。 / The byte span containing the string.</param>
        /// <param name="offset">偏移量。 / The offset.</param>
        /// <returns>从字节范围中读取的字符串。 / The string read from the byte span.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe string ReadString(this Span<byte> buffer, ref int offset)
        {
            var len = ReadShort(buffer, ref offset);
            //数据不可信
            if (len <= 0 || offset < 0 || offset + len > buffer.Length)
            {
                return string.Empty;
            }

            fixed (byte* ptr = buffer)
            {
                var value = System.Text.Encoding.UTF8.GetString(ptr + offset, len);
                offset += len;
                return value;
            }
        }

        /// <summary>
        /// 从字节范围中读取布尔值。
        /// </summary>
        /// <remarks>
        /// Reads a boolean value from the byte span.
        /// </remarks>
        /// <param name="buffer">包含布尔值的字节范围。 / The byte span containing the boolean value.</param>
        /// <param name="offset">偏移量。 / The offset.</param>
        /// <returns>从字节范围中读取的布尔值。 / The boolean value read from the byte span.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe bool ReadBool(this Span<byte> buffer, ref int offset)
        {
            if (offset < 0 || offset + BoolSize > buffer.Length)
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
        /// 将只读内存转换为数组段。
        /// </summary>
        /// <remarks>
        /// Converts read-only memory to an array segment.
        /// </remarks>
        /// <param name="memory">只读内存。 / The read-only memory.</param>
        /// <returns>数组段。 / The array segment.</returns>
        [UnityEngine.Scripting.Preserve]
        public static ArraySegment<byte> GetArray(this ReadOnlyMemory<byte> memory)
        {
            if (!MemoryMarshal.TryGetArray(memory, out var result))
            {
                throw new InvalidOperationException("Buffer backed by array was expected");
            }

            return result;
        }
    }
}