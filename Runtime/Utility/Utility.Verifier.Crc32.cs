//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        public static partial class Verifier
        {
            /// <summary>
            /// CRC32 算法。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            private sealed class Crc32
            {
                private const int TableLength = 256;
                private const uint DefaultPolynomial = 0xedb88320;
                private const uint DefaultSeed = 0xffffffff;

                private readonly uint m_Seed;
                private readonly uint[] m_Table;
                private uint m_Hash;

                /// <summary>
                /// 初始化 CRC32 类的新实例。
                /// </summary>
                [UnityEngine.Scripting.Preserve]
                public Crc32()
                    : this(DefaultPolynomial, DefaultSeed)
                {
                }

                /// <summary>
                /// 使用指定的多项式和种子初始化 CRC32 类的新实例。
                /// </summary>
                /// <param name="polynomial">用于计算 CRC 的多项式。</param>
                /// <param name="seed">用于初始化哈希值的种子。</param>
                [UnityEngine.Scripting.Preserve]
                public Crc32(uint polynomial, uint seed)
                {
                    m_Seed = seed;
                    m_Table = InitializeTable(polynomial);
                    m_Hash = seed;
                }

                /// <summary>
                /// 初始化 CRC32 计算。
                /// </summary>
                [UnityEngine.Scripting.Preserve]
                public void Initialize()
                {
                    m_Hash = m_Seed;
                }

                /// <summary>
                /// 计算给定字节数组的哈希值。
                /// </summary>
                /// <param name="bytes">要计算哈希的字节数组。</param>
                /// <param name="offset">字节数组的起始偏移量。</param>
                /// <param name="length">要计算的字节数。</param>
                [UnityEngine.Scripting.Preserve]
                public void HashCore(byte[] bytes, int offset, int length)
                {
                    m_Hash = CalculateHash(m_Table, m_Hash, bytes, offset, length);
                }

                /// <summary>
                /// 返回计算的 CRC32 哈希值。
                /// </summary>
                /// <returns>计算的 CRC32 哈希值。</returns>
                [UnityEngine.Scripting.Preserve]
                public uint HashFinal()
                {
                    return ~m_Hash;
                }

                private static uint CalculateHash(uint[] table, uint value, byte[] bytes, int offset, int length)
                {
                    int last = offset + length;
                    for (int i = offset; i < last; i++)
                    {
                        unchecked
                        {
                            value = (value >> 8) ^ table[bytes[i] ^ value & 0xff];
                        }
                    }

                    return value;
                }

                private static uint[] InitializeTable(uint polynomial)
                {
                    uint[] table = new uint[TableLength];
                    for (int i = 0; i < TableLength; i++)
                    {
                        uint entry = (uint)i;
                        for (int j = 0; j < 8; j++)
                        {
                            if ((entry & 1) == 1)
                            {
                                entry = (entry >> 1) ^ polynomial;
                            }
                            else
                            {
                                entry >>= 1;
                            }
                        }

                        table[i] = entry;
                    }

                    return table;
                }
            }
        }
    }
}
