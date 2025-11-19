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
using System.Security.Cryptography;
using System.Text;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 加密解密相关的实用函数。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static partial class Encryption
        {
            internal const int QuickEncryptLength = 220;

            /// <summary>
            /// 将 bytes 使用 code 做异或运算的快速版本。
            /// </summary>
            /// <param name="bytes">原始二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            /// <returns>异或后的二进制流。</returns>
            [UnityEngine.Scripting.Preserve]
            public static byte[] GetQuickXorBytes(byte[] bytes, byte[] code)
            {
                return GetXorBytes(bytes, 0, QuickEncryptLength, code);
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算的快速版本。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
            /// </summary>
            /// <param name="bytes">原始及异或后的二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            [UnityEngine.Scripting.Preserve]
            public static void GetQuickSelfXorBytes(byte[] bytes, byte[] code)
            {
                GetSelfXorBytes(bytes, 0, QuickEncryptLength, code);
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算。
            /// </summary>
            /// <param name="bytes">原始二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            /// <returns>异或后的二进制流。</returns>
            [UnityEngine.Scripting.Preserve]
            public static byte[] GetXorBytes(byte[] bytes, byte[] code)
            {
                if (bytes == null)
                {
                    return null;
                }

                return GetXorBytes(bytes, 0, bytes.Length, code);
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
            /// </summary>
            /// <param name="bytes">原始及异或后的二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            [UnityEngine.Scripting.Preserve]
            public static void GetSelfXorBytes(byte[] bytes, byte[] code)
            {
                if (bytes == null)
                {
                    return;
                }

                GetSelfXorBytes(bytes, 0, bytes.Length, code);
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算。
            /// </summary>
            /// <param name="bytes">原始二进制流。</param>
            /// <param name="startIndex">异或计算的开始位置。</param>
            /// <param name="length">异或计算长度，若小于 0，则计算整个二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            /// <returns>异或后的二进制流。</returns>
            [UnityEngine.Scripting.Preserve]
            public static byte[] GetXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
            {
                if (bytes == null)
                {
                    return null;
                }

                int bytesLength = bytes.Length;
                byte[] results = new byte[bytesLength];
                Array.Copy(bytes, 0, results, 0, bytesLength);
                GetSelfXorBytes(results, startIndex, length, code);
                return results;
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
            /// </summary>
            /// <param name="bytes">原始及异或后的二进制流。</param>
            /// <param name="startIndex">异或计算的开始位置。</param>
            /// <param name="length">异或计算长度。</param>
            /// <param name="code">异或二进制流。</param>
            [UnityEngine.Scripting.Preserve]
            public static void GetSelfXorBytes(byte[] bytes, int startIndex, int length, byte[] code)
            {
                if (bytes == null)
                {
                    return;
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

                if (startIndex < 0 || length < 0 || startIndex + length > bytes.Length)
                {
                    throw new GameFrameworkException("Start index or length is invalid.");
                }

                int codeIndex = startIndex % codeLength;
                for (int i = startIndex; i < length; i++)
                {
                    bytes[i] ^= code[codeIndex++];
                    codeIndex %= codeLength;
                }
            }
        }
    }
}