// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

/* Licensed to the .NET Foundation under one or more agreements.
 * The .NET Foundation licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information. */

using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting; // Added for Preserve attribute

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 提供对只读序列的逐项读取功能的结构。
    /// </summary>
    /// <remarks>
    /// Provides a struct for reading items one by one from a read-only sequence.
    /// </remarks>
    [Preserve] // Added Preserve attribute
    public ref partial struct SequenceReader<T> where T : unmanaged, IEquatable<T>
    {
        /// <summary>
        /// 指示是否使用 <see cref="sequence"/>（而非 <see cref="memory"/>）的值。
        /// </summary>
        /// <remarks>
        /// A value indicating whether we're using <see cref="sequence"/> (as opposed to <see cref="memory"/>.
        /// </remarks>
        private bool usingSequence;

        /// <summary>
        /// 未使用 <see cref="memory"/> 时整个序列的后备数据。
        /// </summary>
        /// <remarks>
        /// Backing for the entire sequence when we're not using <see cref="memory"/>.
        /// </remarks>
        private ReadOnlySequence<T> sequence;

        /// <summary>
        /// <see cref="CurrentSpan"/> 起始处的位置。
        /// </summary>
        /// <remarks>
        /// The position at the start of the <see cref="CurrentSpan"/>.
        /// </remarks>
        private SequencePosition currentPosition;

        /// <summary>
        /// <see cref="CurrentSpan"/> 结束处的位置。
        /// </summary>
        /// <remarks>
        /// The position at the end of the <see cref="CurrentSpan"/>.
        /// </remarks>
        private SequencePosition nextPosition;

        /// <summary>
        /// 未使用 <see cref="sequence"/> 时整个序列的后备数据。
        /// </summary>
        /// <remarks>
        /// Backing for the entire sequence when we're not using <see cref="sequence"/>.
        /// </remarks>
        private ReadOnlyMemory<T> memory;

        /// <summary>
        /// 指示是否还有剩余未读数据的值。
        /// </summary>
        /// <remarks>
        /// A value indicating whether there is unread data remaining.
        /// </remarks>
        private bool moreData;

        /// <summary>
        /// 序列中元素的总数。
        /// </summary>
        /// <remarks>
        /// The total number of elements in the sequence.
        /// </remarks>
        private long length;

        /// <summary>
        /// 初始化一个基于给定 <see cref="ReadOnlySequence{T}"/> 的新 <see cref="SequenceReader{T}"/> 实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="SequenceReader{T}"/> struct
        /// over the given <see cref="ReadOnlySequence{T}"/>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Preserve] // Added Preserve attribute
        public SequenceReader(in ReadOnlySequence<T> sequence)
        {
            this.usingSequence = true;
            this.CurrentSpanIndex = 0;
            this.Consumed = 0;
            this.sequence = sequence;
            this.memory = default;
            this.currentPosition = sequence.Start;
            this.length = -1;

            ReadOnlySpan<T> first = sequence.First.Span;
            this.nextPosition = sequence.GetPosition(first.Length);
            this.CurrentSpan = first;
            this.moreData = first.Length > 0;

            if (!this.moreData && !sequence.IsSingleSegment)
            {
                this.moreData = true;
                this.GetNextSpan();
            }
        }

        /// <summary>
        /// 初始化一个基于给定 <see cref="ReadOnlyMemory{T}"/> 的新 <see cref="SequenceReader{T}"/> 实例。
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="SequenceReader{T}"/> struct
        /// over the given <see cref="ReadOnlyMemory{T}"/>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Preserve] // Added Preserve attribute
        public SequenceReader(ReadOnlyMemory<T> memory)
        {
            this.usingSequence = false;
            this.CurrentSpanIndex = 0;
            this.Consumed = 0;
            this.memory = memory;
            this.CurrentSpan = memory.Span;
            this.length = memory.Length;
            this.moreData = memory.Length > 0;

            this.currentPosition = default;
            this.nextPosition = default;
            this.sequence = default;
        }

        /// <summary>
        /// 获取一个指示 <see cref="Sequence"/> 中是否没有更多数据的值。
        /// </summary>
        /// <remarks>
        /// Gets a value indicating whether there is no more data in the <see cref="Sequence"/>.
        /// </remarks>
        public bool End => !this.moreData;

        /// <summary>
        /// 获取读取器底层的 <see cref="ReadOnlySequence{T}"/>。
        /// </summary>
        /// <remarks>
        /// Gets the underlying <see cref="ReadOnlySequence{T}"/> for the reader.
        /// </remarks>
        [Preserve] // Added Preserve attribute
        public ReadOnlySequence<T> Sequence
        {
            get
            {
                if (this.sequence.IsEmpty && !this.memory.IsEmpty)
                {
                    // We're in memory mode (instead of sequence mode).
                    // Lazily fill in the sequence data.
                    this.sequence = new ReadOnlySequence<T>(this.memory);
                    this.currentPosition = this.sequence.Start;
                    this.nextPosition = this.sequence.End;
                }

                return this.sequence;
            }
        }

        /// <summary>
        /// 获取 <see cref="Sequence"/> 中的当前位置。
        /// </summary>
        /// <remarks>
        /// Gets the current position in the <see cref="Sequence"/>.
        /// </remarks>
        [Preserve] // Added Preserve attribute
        public SequencePosition Position
            => this.Sequence.GetPosition(this.CurrentSpanIndex, this.currentPosition);

        /// <summary>
        /// 以范围（span）的形式获取 <see cref="Sequence"/> 中的当前段。
        /// </summary>
        /// <remarks>
        /// Gets the current segment in the <see cref="Sequence"/> as a span.
        /// </remarks>
        [Preserve] // Added Preserve attribute
        public ReadOnlySpan<T> CurrentSpan { get; private set; }

        /// <summary>
        /// 获取 <see cref="CurrentSpan"/> 中的索引。
        /// </summary>
        /// <remarks>
        /// Gets the index in the <see cref="CurrentSpan"/>.
        /// </remarks>
        [Preserve] // Added Preserve attribute
        public int CurrentSpanIndex { get; private set; }

        /// <summary>
        /// 获取 <see cref="CurrentSpan"/> 中未读取的部分。
        /// </summary>
        /// <remarks>
        /// Gets the unread portion of the <see cref="CurrentSpan"/>.
        /// </remarks>
        [Preserve] // Added Preserve attribute
        public ReadOnlySpan<T> UnreadSpan
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.CurrentSpan.Slice(this.CurrentSpanIndex);
        }

        /// <summary>
        /// 获取读取器已处理的 <typeparamref name="T"/> 总数。
        /// </summary>
        /// <remarks>
        /// Gets the total number of <typeparamref name="T"/>'s processed by the reader.
        /// </remarks>
        [Preserve] // Added Preserve attribute
        public long Consumed { get; private set; }

        /// <summary>
        /// 获取读取器的 <see cref="Sequence"/> 中剩余的 <typeparamref name="T"/> 数量。
        /// </summary>
        /// <remarks>
        /// Gets remaining <typeparamref name="T"/>'s in the reader's <see cref="Sequence"/>.
        /// </remarks>
        [Preserve] // Added Preserve attribute
        public long Remaining => this.Length - this.Consumed;

        /// <summary>
        /// 获取读取器的 <see cref="Sequence"/> 中 <typeparamref name="T"/> 的数量。
        /// </summary>
        /// <remarks>
        /// Gets count of <typeparamref name="T"/> in the reader's <see cref="Sequence"/>.
        /// </remarks>
        [Preserve] // Added Preserve attribute
        public long Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.length < 0)
                {
                    // Cache the length
                    this.length = this.Sequence.Length;
                }

                return this.length;
            }
        }

        /// <summary>
        /// 查看下一个值，但不推进读取器。
        /// </summary>
        /// <remarks>
        /// Peeks at the next value without advancing the reader.
        /// </remarks>
        /// <param name="value">下一个值；若已到达末尾则为默认值。 / The next value or default if at the end.</param>
        /// <returns>如果已到达读取器末尾，则为 false。 / False if at the end of the reader.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Preserve] // Added Preserve attribute
        public bool TryPeek(out T value)
        {
            if (this.moreData)
            {
                value = this.CurrentSpan[this.CurrentSpanIndex];
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        /// <summary>
        /// 读取下一个值并推进读取器。
        /// </summary>
        /// <remarks>
        /// Read the next value and advance the reader.
        /// </remarks>
        /// <param name="value">下一个值；若已到达末尾则为默认值。 / The next value or default if at the end.</param>
        /// <returns>如果已到达读取器末尾，则为 false。 / False if at the end of the reader.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Preserve] // Added Preserve attribute
        public bool TryRead(out T value)
        {
            if (this.End)
            {
                value = default;
                return false;
            }

            value = this.CurrentSpan[this.CurrentSpanIndex];
            this.CurrentSpanIndex++;
            this.Consumed++;

            if (this.CurrentSpanIndex >= this.CurrentSpan.Length)
            {
                if (this.usingSequence)
                {
                    this.GetNextSpan();
                }
                else
                {
                    this.moreData = false;
                }
            }

            return true;
        }

        /// <summary>
        /// 将读取器回退指定的项数。
        /// </summary>
        /// <remarks>
        /// Move the reader back the specified number of items.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Preserve] // Added Preserve attribute
        public void Rewind(long count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            this.Consumed -= count;

            if (this.CurrentSpanIndex >= count)
            {
                this.CurrentSpanIndex -= (int)count;
                this.moreData = true;
            }
            else if (this.usingSequence)
            {
                // Current segment doesn't have enough data, scan backward through segments
                this.RetreatToPreviousSpan(this.Consumed);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Rewind went past the start of the memory.");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void RetreatToPreviousSpan(long consumed)
        {
            Debug.Assert(this.usingSequence, "usingSequence");
            this.ResetReader();
            this.Advance(consumed);
        }

        private void ResetReader()
        {
            Debug.Assert(this.usingSequence, "usingSequence");
            this.CurrentSpanIndex = 0;
            this.Consumed = 0;
            this.currentPosition = this.Sequence.Start;
            this.nextPosition = this.currentPosition;

            if (this.Sequence.TryGet(ref this.nextPosition, out ReadOnlyMemory<T> memory, advance: true))
            {
                this.moreData = true;

                if (memory.Length == 0)
                {
                    this.CurrentSpan = default;

                    // No data in the first span, move to one with data
                    this.GetNextSpan();
                }
                else
                {
                    this.CurrentSpan = memory.Span;
                }
            }
            else
            {
                // No data in any spans and at end of sequence
                this.moreData = false;
                this.CurrentSpan = default;
            }
        }

        /// <summary>
        /// 获取下一个包含可用数据的段（如果有）。
        /// </summary>
        /// <remarks>
        /// Get the next segment with available data, if any.
        /// </remarks>
        private void GetNextSpan()
        {
            Debug.Assert(this.usingSequence, "usingSequence");
            if (!this.Sequence.IsSingleSegment)
            {
                SequencePosition previousNextPosition = this.nextPosition;
                while (this.Sequence.TryGet(ref this.nextPosition, out ReadOnlyMemory<T> memory, advance: true))
                {
                    this.currentPosition = previousNextPosition;
                    if (memory.Length > 0)
                    {
                        this.CurrentSpan = memory.Span;
                        this.CurrentSpanIndex = 0;
                        return;
                    }
                    else
                    {
                        this.CurrentSpan = default;
                        this.CurrentSpanIndex = 0;
                        previousNextPosition = this.nextPosition;
                    }
                }
            }

            this.moreData = false;
        }

        /// <summary>
        /// 将读取器向前移动指定的项数。
        /// </summary>
        /// <remarks>
        /// Move the reader ahead the specified number of items.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Preserve] // Added Preserve attribute
        public void Advance(long count)
        {
            const long TooBigOrNegative = unchecked((long)0xFFFFFFFF80000000);
            if ((count & TooBigOrNegative) == 0 && this.CurrentSpan.Length - this.CurrentSpanIndex > (int)count)
            {
                this.CurrentSpanIndex += (int)count;
                this.Consumed += count;
            }
            else if (this.usingSequence)
            {
                // Can't satisfy from the current span
                this.AdvanceToNextSpan(count);
            }
            else if (this.CurrentSpan.Length - this.CurrentSpanIndex == (int)count)
            {
                this.CurrentSpanIndex += (int)count;
                this.Consumed += count;
                this.moreData = false;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
        }

        /// <summary>
        /// 无检查的辅助方法，在已知数量有效时避免不必要的检查。
        /// </summary>
        /// <remarks>
        /// Unchecked helper to avoid unnecessary checks where you know count is valid.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AdvanceCurrentSpan(long count)
        {
            Debug.Assert(count >= 0, "count >= 0");

            this.Consumed += count;
            this.CurrentSpanIndex += (int)count;
            if (this.usingSequence && this.CurrentSpanIndex >= this.CurrentSpan.Length)
            {
                this.GetNextSpan();
            }
        }

        /// <summary>
        /// 仅在已知于当前范围内以有效数量推进且无需获取下一段时才调用的辅助方法。
        /// </summary>
        /// <remarks>
        /// Only call this helper if you know that you are advancing in the current span
        /// with valid count and there is no need to fetch the next one.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AdvanceWithinSpan(long count)
        {
            Debug.Assert(count >= 0, "count >= 0");

            this.Consumed += count;
            this.CurrentSpanIndex += (int)count;

            Debug.Assert(this.CurrentSpanIndex < this.CurrentSpan.Length, "this.CurrentSpanIndex < this.CurrentSpan.Length");
        }

        /// <summary>
        /// 如果序列中剩余元素足够，则将读取器向前移动指定的项数。
        /// </summary>
        /// <remarks>
        /// Move the reader ahead the specified number of items
        /// if there are enough elements remaining in the sequence.
        /// </remarks>
        /// <returns>如果有足够的元素可以推进，则为 true；否则为 false。 / True if there were enough elements to advance; otherwise false.</returns>
        internal bool TryAdvance(long count)
        {
            if (this.Remaining < count)
            {
                return false;
            }

            this.Advance(count);
            return true;
        }

        private void AdvanceToNextSpan(long count)
        {
            Debug.Assert(this.usingSequence, "usingSequence");
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            this.Consumed += count;
            while (this.moreData)
            {
                int remaining = this.CurrentSpan.Length - this.CurrentSpanIndex;

                if (remaining > count)
                {
                    this.CurrentSpanIndex += (int)count;
                    count = 0;
                    break;
                }

                // As there may not be any further segments we need to
                // push the current index to the end of the span.
                this.CurrentSpanIndex += remaining;
                count -= remaining;
                Debug.Assert(count >= 0, "count >= 0");

                this.GetNextSpan();

                if (count == 0)
                {
                    break;
                }
            }

            if (count != 0)
            {
                // Not enough data left- adjust for where we actually ended and throw
                this.Consumed -= count;
                throw new ArgumentOutOfRangeException(nameof(count));
            }
        }

        /// <summary>
        /// 将数据从当前 <see cref="Position"/> 复制到给定的 <paramref name="destination"/> 范围。
        /// </summary>
        /// <remarks>
        /// Copies data from the current <see cref="Position"/> to the given <paramref name="destination"/> span.
        /// </remarks>
        /// <param name="destination">要复制到的目标范围。 / Destination to copy to.</param>
        /// <returns>如果有足够的数据可复制到目标范围，则为 true。 / True if there is enough data to copy to the <paramref name="destination"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Preserve] // Added Preserve attribute
        public bool TryCopyTo(Span<T> destination)
        {
            ReadOnlySpan<T> firstSpan = this.UnreadSpan;
            if (firstSpan.Length >= destination.Length)
            {
                firstSpan.Slice(0, destination.Length).CopyTo(destination);
                return true;
            }

            return this.TryCopyMultisegment(destination);
        }

        internal bool TryCopyMultisegment(Span<T> destination)
        {
            if (this.Remaining < destination.Length)
            {
                return false;
            }

            ReadOnlySpan<T> firstSpan = this.UnreadSpan;
            Debug.Assert(firstSpan.Length < destination.Length, "firstSpan.Length < destination.Length");
            firstSpan.CopyTo(destination);
            int copied = firstSpan.Length;

            SequencePosition next = this.nextPosition;
            while (this.Sequence.TryGet(ref next, out ReadOnlyMemory<T> nextSegment, true))
            {
                if (nextSegment.Length > 0)
                {
                    ReadOnlySpan<T> nextSpan = nextSegment.Span;
                    int toCopy = Math.Min(nextSpan.Length, destination.Length - copied);
                    nextSpan.Slice(0, toCopy).CopyTo(destination.Slice(copied));
                    copied += toCopy;
                    if (copied >= destination.Length)
                    {
                        break;
                    }
                }
            }

            return true;
        }
    }
}