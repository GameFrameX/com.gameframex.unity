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

using GameFrameX;
using System;
using System.IO;
using GameFrameX.Runtime;
using UnityEngine.Scripting; // Added this line for the Preserve attribute

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 对 BinaryReader 和 BinaryWriter 的扩展方法。
    /// </summary>
    [Preserve]
    public static class BinaryExtension
    {
        private static readonly byte[] s_CachedBytes = new byte[byte.MaxValue + 1];

        /// <summary>
        /// 从二进制流读取编码后的 32 位有符号整数。
        /// </summary>
        /// <param name="binaryReader">要读取的二进制流。</param>
        /// <returns>读取的 32 位有符号整数。</returns>
        [Preserve]
        public static int Read7BitEncodedInt32(this BinaryReader binaryReader)
        {
            int value = 0;
            int shift = 0;
            byte b;
            do
            {
                if (shift >= 35)
                {
                    throw new GameFrameworkException("7 bit encoded int is invalid.");
                }

                b = binaryReader.ReadByte();
                value |= (b & 0x7f) << shift;
                shift += 7;
            } while ((b & 0x80) != 0);

            return value;
        }

        /// <summary>
        /// 向二进制流写入编码后的 32 位有符号整数。
        /// </summary>
        /// <param name="binaryWriter">要写入的二进制流。</param>
        /// <param name="value">要写入的 32 位有符号整数。</param>
        [Preserve]
        public static void Write7BitEncodedInt32(this BinaryWriter binaryWriter, int value)
        {
            uint num = (uint)value;
            while (num >= 0x80)
            {
                binaryWriter.Write((byte)(num | 0x80));
                num >>= 7;
            }

            binaryWriter.Write((byte)num);
        }

        /// <summary>
        /// 从二进制流读取编码后的 32 位无符号整数。
        /// </summary>
        /// <param name="binaryReader">要读取的二进制流。</param>
        /// <returns>读取的 32 位无符号整数。</returns>
        [Preserve]
        public static uint Read7BitEncodedUInt32(this BinaryReader binaryReader)
        {
            return (uint)Read7BitEncodedInt32(binaryReader);
        }

        /// <summary>
        /// 向二进制流写入编码后的 32 位无符号整数。
        /// </summary>
        /// <param name="binaryWriter">要写入的二进制流。</param>
        /// <param name="value">要写入的 32 位无符号整数。</param>
        [Preserve]
        public static void Write7BitEncodedUInt32(this BinaryWriter binaryWriter, uint value)
        {
            Write7BitEncodedInt32(binaryWriter, (int)value);
        }

        /// <summary>
        /// 从二进制流读取编码后的 64 位有符号整数。
        /// </summary>
        /// <param name="binaryReader">要读取的二进制流。</param>
        /// <returns>读取的 64 位有符号整数。</returns>
        [Preserve]
        public static long Read7BitEncodedInt64(this BinaryReader binaryReader)
        {
            long value = 0L;
            int shift = 0;
            byte b;
            do
            {
                if (shift >= 70)
                {
                    throw new GameFrameworkException("7 bit encoded int is invalid.");
                }

                b = binaryReader.ReadByte();
                value |= (b & 0x7fL) << shift;
                shift += 7;
            } while ((b & 0x80) != 0);

            return value;
        }

        /// <summary>
        /// 向二进制流写入编码后的 64 位有符号整数。
        /// </summary>
        /// <param name="binaryWriter">要写入的二进制流。</param>
        /// <param name="value">要写入的 64 位有符号整数。</param>
        [Preserve]
        public static void Write7BitEncodedInt64(this BinaryWriter binaryWriter, long value)
        {
            ulong num = (ulong)value;
            while (num >= 0x80)
            {
                binaryWriter.Write((byte)(num | 0x80));
                num >>= 7;
            }

            binaryWriter.Write((byte)num);
        }

        /// <summary>
        /// 从二进制流读取编码后的 64 位无符号整数。
        /// </summary>
        /// <param name="binaryReader">要读取的二进制流。</param>
        /// <returns>读取的 64 位无符号整数。</returns>
        [Preserve]
        public static ulong Read7BitEncodedUInt64(this BinaryReader binaryReader)
        {
            return (ulong)Read7BitEncodedInt64(binaryReader);
        }

        /// <summary>
        /// 向二进制流写入编码后的 64 位无符号整数。
        /// </summary>
        /// <param name="binaryWriter">要写入的二进制流。</param>
        /// <param name="value">要写入的 64 位无符号整数。</param>
        [Preserve]
        public static void Write7BitEncodedUInt64(this BinaryWriter binaryWriter, ulong value)
        {
            Write7BitEncodedInt64(binaryWriter, (long)value);
        }

        /// <summary>
        /// 从二进制流读取加密字符串。
        /// </summary>
        /// <param name="binaryReader">要读取的二进制流。</param>
        /// <param name="encryptBytes">密钥数组。</param>
        /// <returns>读取的字符串。</returns>
        [Preserve]
        public static string ReadEncryptedString(this BinaryReader binaryReader, byte[] encryptBytes)
        {
            byte length = binaryReader.ReadByte();
            if (length <= 0)
            {
                return null;
            }

            if (length > byte.MaxValue)
            {
                throw new GameFrameworkException("String is too long.");
            }

            for (byte i = 0; i < length; i++)
            {
                s_CachedBytes[i] = binaryReader.ReadByte();
            }

            Utility.Encryption.GetSelfXorBytes(s_CachedBytes, 0, length, encryptBytes);
            string value = Utility.Converter.GetString(s_CachedBytes, 0, length);
            Array.Clear(s_CachedBytes, 0, length);
            return value;
        }

        /// <summary>
        /// 向二进制流写入加密字符串。
        /// </summary>
        /// <param name="binaryWriter">要写入的二进制流。</param>
        /// <param name="value">要写入的字符串。</param>
        /// <param name="encryptBytes">密钥数组。</param>
        [Preserve]
        public static void WriteEncryptedString(this BinaryWriter binaryWriter, string value, byte[] encryptBytes)
        {
            if (string.IsNullOrEmpty(value))
            {
                binaryWriter.Write((byte)0);
                return;
            }

            int length = Utility.Converter.GetBytes(value, s_CachedBytes);
            if (length > byte.MaxValue)
            {
                throw new GameFrameworkException(Utility.Text.Format("String '{0}' is too long.", value));
            }

            Utility.Encryption.GetSelfXorBytes(s_CachedBytes, encryptBytes);
            binaryWriter.Write((byte)length);
            binaryWriter.Write(s_CachedBytes, 0, length);
        }
    }
}