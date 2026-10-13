// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

/* Licensed to the .NET Foundation under one or more agreements.
 * The .NET Foundation licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information. */

using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 为 <see cref="SequenceReader{byte}"/> 提供数值读取扩展方法。
    /// </summary>
    /// <remarks>
    /// Provides numeric read extension methods for <see cref="SequenceReader{byte}"/>.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static partial class SequenceReaderExtensions
    {
        /// <summary>
        /// 尝试从缓冲区中读取给定的类型。警告：对任意结构体使用此方法很危险，详见备注。
        /// </summary>
        /// <remarks>
        /// IMPORTANT: The read is a straight copy of bits. If a struct depends on specific state of its members to
        /// behave correctly this can lead to exceptions, etc. If reading endian specific integers, use the explicit
        /// overloads such as <see cref="TryReadBigEndian(ref SequenceReader{byte}, out short)"/>.
        /// </remarks>
        /// <returns>成功时为 true；失败（空间不足）时 <paramref name="value"/> 为默认值。 / True if successful. <paramref name="value"/> will be default if failed (due to lack of space).</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [UnityEngine.Scripting.Preserve]
        internal static unsafe bool TryRead<T>(ref this SequenceReader<byte> reader, out T value)
            where T : unmanaged
        {
            ReadOnlySpan<byte> span = reader.UnreadSpan;
            if (span.Length < sizeof(T))
            {
                return TryReadMultisegment(ref reader, out value);
            }

            value = Unsafe.ReadUnaligned<T>(ref MemoryMarshal.GetReference(span));
            reader.Advance(sizeof(T));
            return true;
        }

        private static unsafe bool TryReadMultisegment<T>(ref SequenceReader<byte> reader, out T value)
            where T : unmanaged
        {
            Debug.Assert(reader.UnreadSpan.Length < sizeof(T), "reader.UnreadSpan.Length < sizeof(T)");

            // Not enough data in the current segment, try to peek for the data we need.
            T buffer = default;
            Span<byte> tempSpan = new Span<byte>(&buffer, sizeof(T));

            if (!reader.TryCopyTo(tempSpan))
            {
                value = default;
                return false;
            }

            value = Unsafe.ReadUnaligned<T>(ref MemoryMarshal.GetReference(tempSpan));
            reader.Advance(sizeof(T));
            return true;
        }

        /// <summary>
        /// 从序列中的下一个位置读取 <see cref="sbyte"/>。
        /// </summary>
        /// <remarks>
        /// Reads an <see cref="sbyte"/> from the next position in the sequence.
        /// </remarks>
        /// <param name="reader">要读取的读取器。 / The reader to read from.</param>
        /// <param name="value">接收读取到的值。 / Receives the value read.</param>
        /// <returns>若序列中还有另一个字节则为 <see langword="true"/>；否则为 <see langword="false"/>。 / <see langword="true"/> if there was another byte in the sequence; <see langword="false"/> otherwise.</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool TryRead(ref this SequenceReader<byte> reader, out sbyte value)
        {
            if (TryRead(ref reader, out byte byteValue))
            {
                value = unchecked((sbyte)byteValue);
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// 以大端序读取 <see cref="Int16"/>。
        /// </summary>
        /// <remarks>
        /// Reads an <see cref="Int16"/> as big endian.
        /// </remarks>
        /// <returns>如果没有足够的 <see cref="Int16"/> 数据，则为 false。 / False if there wasn't enough data for an <see cref="Int16"/>.</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool TryReadBigEndian(ref this SequenceReader<byte> reader, out short value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return reader.TryRead(out value);
            }

            return TryReadReverseEndianness(ref reader, out value);
        }

        /// <summary>
        /// 以大端序读取 <see cref="UInt16"/>。
        /// </summary>
        /// <remarks>
        /// Reads an <see cref="UInt16"/> as big endian.
        /// </remarks>
        /// <returns>如果没有足够的 <see cref="UInt16"/> 数据，则为 false。 / False if there wasn't enough data for an <see cref="UInt16"/>.</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool TryReadBigEndian(ref this SequenceReader<byte> reader, out ushort value)
        {
            if (TryReadBigEndian(ref reader, out short shortValue))
            {
                value = unchecked((ushort)shortValue);
                return true;
            }

            value = default;
            return false;
        }

        private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out short value)
        {
            if (reader.TryRead(out value))
            {
                value = BinaryPrimitives.ReverseEndianness(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 以大端序读取 <see cref="Int32"/>。
        /// </summary>
        /// <remarks>
        /// Reads an <see cref="Int32"/> as big endian.
        /// </remarks>
        /// <returns>如果没有足够的 <see cref="Int32"/> 数据，则为 false。 / False if there wasn't enough data for an <see cref="Int32"/>.</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool TryReadBigEndian(ref this SequenceReader<byte> reader, out int value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return reader.TryRead(out value);
            }

            return TryReadReverseEndianness(ref reader, out value);
        }

        /// <summary>
        /// 以大端序读取 <see cref="UInt32"/>。
        /// </summary>
        /// <remarks>
        /// Reads an <see cref="UInt32"/> as big endian.
        /// </remarks>
        /// <returns>如果没有足够的 <see cref="UInt32"/> 数据，则为 false。 / False if there wasn't enough data for an <see cref="UInt32"/>.</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool TryReadBigEndian(ref this SequenceReader<byte> reader, out uint value)
        {
            if (TryReadBigEndian(ref reader, out int intValue))
            {
                value = unchecked((uint)intValue);
                return true;
            }

            value = default;
            return false;
        }

        private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out int value)
        {
            if (reader.TryRead(out value))
            {
                value = BinaryPrimitives.ReverseEndianness(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 以大端序读取 <see cref="Int64"/>。
        /// </summary>
        /// <remarks>
        /// Reads an <see cref="Int64"/> as big endian.
        /// </remarks>
        /// <returns>如果没有足够的 <see cref="Int64"/> 数据，则为 false。 / False if there wasn't enough data for an <see cref="Int64"/>.</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool TryReadBigEndian(ref this SequenceReader<byte> reader, out long value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return reader.TryRead(out value);
            }

            return TryReadReverseEndianness(ref reader, out value);
        }

        /// <summary>
        /// 以大端序读取 <see cref="UInt64"/>。
        /// </summary>
        /// <remarks>
        /// Reads an <see cref="UInt64"/> as big endian.
        /// </remarks>
        /// <returns>如果没有足够的 <see cref="UInt64"/> 数据，则为 false。 / False if there wasn't enough data for an <see cref="UInt64"/>.</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool TryReadBigEndian(ref this SequenceReader<byte> reader, out ulong value)
        {
            if (TryReadBigEndian(ref reader, out long longValue))
            {
                value = unchecked((ulong)longValue);
                return true;
            }

            value = default;
            return false;
        }

        private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out long value)
        {
            if (reader.TryRead(out value))
            {
                value = BinaryPrimitives.ReverseEndianness(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 以大端序读取 <see cref="Single"/>。
        /// </summary>
        /// <remarks>
        /// Reads a <see cref="Single"/> as big endian.
        /// </remarks>
        /// <returns>如果没有足够的 <see cref="Single"/> 数据，则为 false。 / False if there wasn't enough data for a <see cref="Single"/>.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe bool TryReadBigEndian(ref this SequenceReader<byte> reader, out float value)
        {
            if (TryReadBigEndian(ref reader, out int intValue))
            {
                value = *(float*)&intValue;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// 以大端序读取 <see cref="Double"/>。
        /// </summary>
        /// <remarks>
        /// Reads a <see cref="Double"/> as big endian.
        /// </remarks>
        /// <returns>如果没有足够的 <see cref="Double"/> 数据，则为 false。 / False if there wasn't enough data for a <see cref="Double"/>.</returns>
        [UnityEngine.Scripting.Preserve]
        public static unsafe bool TryReadBigEndian(ref this SequenceReader<byte> reader, out double value)
        {
            if (TryReadBigEndian(ref reader, out long longValue))
            {
                value = *(double*)&longValue;
                return true;
            }

            value = default;
            return false;
        }
    }
}