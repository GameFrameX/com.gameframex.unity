// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

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