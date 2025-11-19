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

using System;
using System.IO;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 校验相关的实用函数。
        /// </summary>
        [Preserve]
        public static partial class Verifier
        {
            private const int CachedBytesLength = 0x1000;
            private static readonly byte[] SCachedBytes = new byte[CachedBytesLength];
            private static readonly Crc32 SAlgorithm = new Crc32();
            private static readonly Crc64 SAlgorithm64 = new Crc64();

            /// <summary>
            /// 计算二进制流的CRC64
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            [Preserve]
            public static ulong GetCrc64(byte[] bytes)
            {
                SAlgorithm64.Reset();
                SAlgorithm64.Append(bytes);
                return SAlgorithm64.GetCurrentHashAsUInt64();
            }

            /// <summary>
            /// 计算流的CRC64
            /// </summary>
            /// <param name="stream"></param>
            /// <returns></returns>
            [Preserve]
            public static ulong GetCrc64(Stream stream)
            {
                SAlgorithm64.Reset();
                SAlgorithm64.Append(stream);
                return SAlgorithm64.GetCurrentHashAsUInt64();
            }

            /// <summary>
            /// 计算二进制流的 CRC32。
            /// </summary>
            /// <param name="bytes">指定的二进制流。</param>
            /// <returns>计算后的 CRC32。</returns>
            [Preserve]
            public static int GetCrc32(byte[] bytes)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                return GetCrc32(bytes, 0, bytes.Length);
            }

            /// <summary>
            /// 计算二进制流的 CRC32。
            /// </summary>
            /// <param name="bytes">指定的二进制流。</param>
            /// <param name="offset">二进制流的偏移。</param>
            /// <param name="length">二进制流的长度。</param>
            /// <returns>计算后的 CRC32。</returns>
            [Preserve]
            public static int GetCrc32(byte[] bytes, int offset, int length)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                if (offset < 0 || length < 0 || offset + length > bytes.Length)
                {
                    throw new GameFrameworkException("Offset or length is invalid.");
                }

                SAlgorithm.HashCore(bytes, offset, length);
                int result = (int)SAlgorithm.HashFinal();
                SAlgorithm.Initialize();
                return result;
            }

            /// <summary>
            /// 计算二进制流的 CRC32。
            /// </summary>
            /// <param name="stream">指定的二进制流。</param>
            /// <returns>计算后的 CRC32。</returns>
            [Preserve]
            public static int GetCrc32(Stream stream)
            {
                if (stream == null)
                {
                    throw new GameFrameworkException("Stream is invalid.");
                }

                while (true)
                {
                    int bytesRead = stream.Read(SCachedBytes, 0, CachedBytesLength);
                    if (bytesRead > 0)
                    {
                        SAlgorithm.HashCore(SCachedBytes, 0, bytesRead);
                    }
                    else
                    {
                        break;
                    }
                }

                int result = (int)SAlgorithm.HashFinal();
                SAlgorithm.Initialize();
                Array.Clear(SCachedBytes, 0, CachedBytesLength);
                return result;
            }

            /// <summary>
            /// 获取 CRC32 数值的二进制数组。
            /// </summary>
            /// <param name="crc32">CRC32 数值。</param>
            /// <returns>CRC32 数值的二进制数组。</returns>
            [Preserve]
            public static byte[] GetCrc32Bytes(int crc32)
            {
                return new byte[] { (byte)((crc32 >> 24) & 0xff), (byte)((crc32 >> 16) & 0xff), (byte)((crc32 >> 8) & 0xff), (byte)(crc32 & 0xff) };
            }

            /// <summary>
            /// 获取 CRC32 数值的二进制数组。
            /// </summary>
            /// <param name="crc32">CRC32 数值。</param>
            /// <param name="bytes">要存放结果的数组。</param>
            [Preserve]
            public static void GetCrc32Bytes(int crc32, byte[] bytes)
            {
                GetCrc32Bytes(crc32, bytes, 0);
            }

            /// <summary>
            /// 获取 CRC32 数值的二进制数组。
            /// </summary>
            /// <param name="crc32">CRC32 数值。</param>
            /// <param name="bytes">要存放结果的数组。</param>
            /// <param name="offset">CRC32 数值的二进制数组在结果数组内的起始位置。</param>
            [Preserve]
            public static void GetCrc32Bytes(int crc32, byte[] bytes, int offset)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Result is invalid.");
                }

                if (offset < 0 || offset + 4 > bytes.Length)
                {
                    throw new GameFrameworkException("Offset or length is invalid.");
                }

                bytes[offset] = (byte)((crc32 >> 24) & 0xff);
                bytes[offset + 1] = (byte)((crc32 >> 16) & 0xff);
                bytes[offset + 2] = (byte)((crc32 >> 8) & 0xff);
                bytes[offset + 3] = (byte)(crc32 & 0xff);
            }

            internal static int GetCrc32(Stream stream, byte[] code, int length)
            {
                if (stream == null)
                {
                    throw new GameFrameworkException("Stream is invalid.");
                }

                if (code == null)
                {
                    throw new GameFrameworkException("Code is invalid.");
                }

                int codeLength = code.Length;
                if (codeLength <= 0)
                {
                    throw new GameFrameworkException("Code length is invalid.");
                }

                int bytesLength = (int)stream.Length;
                if (length < 0 || length > bytesLength)
                {
                    length = bytesLength;
                }

                int codeIndex = 0;
                while (true)
                {
                    int bytesRead = stream.Read(SCachedBytes, 0, CachedBytesLength);
                    if (bytesRead > 0)
                    {
                        if (length > 0)
                        {
                            for (int i = 0; i < bytesRead && i < length; i++)
                            {
                                SCachedBytes[i] ^= code[codeIndex++];
                                codeIndex %= codeLength;
                            }

                            length -= bytesRead;
                        }

                        SAlgorithm.HashCore(SCachedBytes, 0, bytesRead);
                    }
                    else
                    {
                        break;
                    }
                }

                int result = (int)SAlgorithm.HashFinal();
                SAlgorithm.Initialize();
                Array.Clear(SCachedBytes, 0, CachedBytesLength);
                return result;
            }
        }
    }
}