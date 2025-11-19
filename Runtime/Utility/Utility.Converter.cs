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
using System.Text;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 类型转换相关的实用函数。
        /// </summary>
        [Preserve]
        public static class Converter
        {
            private const float InchesToCentimeters = 2.54f; // 1 inch = 2.54 cm
            private const float CentimetersToInches = 1f / InchesToCentimeters; // 1 cm = 0.3937 inches

            /// <summary>
            /// 获取数据在此计算机结构中存储时的字节顺序。
            /// </summary>
            [Preserve]
            public static bool IsLittleEndian
            {
                get { return BitConverter.IsLittleEndian; }
            }

            /// <summary>
            /// 获取或设置屏幕每英寸点数。
            /// </summary>
            [Preserve]
            public static float ScreenDpi { get; set; }

            /// <summary>
            /// 将像素转换为厘米。
            /// </summary>
            /// <param name="pixels">像素。</param>
            /// <returns>厘米。</returns>
            [Preserve]
            public static float GetCentimetersFromPixels(float pixels)
            {
                if (ScreenDpi <= 0)
                {
                    throw new GameFrameworkException("You must set screen DPI first.");
                }

                return InchesToCentimeters * pixels / ScreenDpi;
            }

            /// <summary>
            /// 将厘米转换为像素。
            /// </summary>
            /// <param name="centimeters">厘米。</param>
            /// <returns>像素。</returns>
            [Preserve]
            public static float GetPixelsFromCentimeters(float centimeters)
            {
                if (ScreenDpi <= 0)
                {
                    throw new GameFrameworkException("You must set screen DPI first.");
                }

                return CentimetersToInches * centimeters * ScreenDpi;
            }

            /// <summary>
            /// 将像素转换为英寸。
            /// </summary>
            /// <param name="pixels">像素。</param>
            /// <returns>英寸。</returns>
            [Preserve]
            public static float GetInchesFromPixels(float pixels)
            {
                if (ScreenDpi <= 0)
                {
                    throw new GameFrameworkException("You must set screen DPI first.");
                }

                return pixels / ScreenDpi;
            }

            /// <summary>
            /// 将英寸转换为像素。
            /// </summary>
            /// <param name="inches">英寸。</param>
            /// <returns>像素。</returns>
            [Preserve]
            public static float GetPixelsFromInches(float inches)
            {
                if (ScreenDpi <= 0)
                {
                    throw new GameFrameworkException("You must set screen DPI first.");
                }

                return inches * ScreenDpi;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的布尔值。
            /// </summary>
            /// <param name="value">要转换的布尔值。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(bool value)
            {
                byte[] buffer = new byte[1];
                GetBytes(value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的布尔值。
            /// </summary>
            /// <param name="value">要转换的布尔值。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static void GetBytes(bool value, byte[] buffer)
            {
                GetBytes(value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的布尔值。
            /// </summary>
            /// <param name="value">要转换的布尔值。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static void GetBytes(bool value, byte[] buffer, int startIndex)
            {
                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0 || startIndex + 1 > buffer.Length)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                buffer[startIndex] = value ? (byte)1 : (byte)0;
            }

            /// <summary>
            /// 返回由字节数组中首字节转换来的布尔值。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>如果 value 中的首字节非零，则为 true，否则为 false。</returns>
            [Preserve]
            public static bool GetBoolean(byte[] value)
            {
                return BitConverter.ToBoolean(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的一个字节转换来的布尔值。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>如果 value 中指定位置的字节非零，则为 true，否则为 false。</returns>
            [Preserve]
            public static bool GetBoolean(byte[] value, int startIndex)
            {
                return BitConverter.ToBoolean(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 Unicode 字符值。
            /// </summary>
            /// <param name="value">要转换的字符。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(char value)
            {
                byte[] buffer = new byte[2];
                GetBytes((short)value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 Unicode 字符值。
            /// </summary>
            /// <param name="value">要转换的字符。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static void GetBytes(char value, byte[] buffer)
            {
                GetBytes((short)value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 Unicode 字符值。
            /// </summary>
            /// <param name="value">要转换的字符。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static void GetBytes(char value, byte[] buffer, int startIndex)
            {
                GetBytes((short)value, buffer, startIndex);
            }

            /// <summary>
            /// 返回由字节数组中前两个字节转换来的 Unicode 字符。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由两个字节构成的字符。</returns>
            [Preserve]
            public static char GetChar(byte[] value)
            {
                return BitConverter.ToChar(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的两个字节转换来的 Unicode 字符。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由两个字节构成的字符。</returns>
            [Preserve]
            public static char GetChar(byte[] value, int startIndex)
            {
                return BitConverter.ToChar(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 16 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(short value)
            {
                byte[] buffer = new byte[2];
                GetBytes(value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 16 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static void GetBytes(short value, byte[] buffer)
            {
                GetBytes(value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 16 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static unsafe void GetBytes(short value, byte[] buffer, int startIndex)
            {
                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0 || startIndex + 2 > buffer.Length)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                fixed (byte* valueRef = buffer)
                {
                    *(short*)(valueRef + startIndex) = value;
                }
            }

            /// <summary>
            /// 返回由字节数组中前两个字节转换来的 16 位有符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由两个字节构成的 16 位有符号整数。</returns>
            [Preserve]
            public static short GetInt16(byte[] value)
            {
                return BitConverter.ToInt16(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的两个字节转换来的 16 位有符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由两个字节构成的 16 位有符号整数。</returns>
            [Preserve]
            public static short GetInt16(byte[] value, int startIndex)
            {
                return BitConverter.ToInt16(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 16 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(ushort value)
            {
                byte[] buffer = new byte[2];
                GetBytes((short)value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 16 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static void GetBytes(ushort value, byte[] buffer)
            {
                GetBytes((short)value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 16 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static void GetBytes(ushort value, byte[] buffer, int startIndex)
            {
                GetBytes((short)value, buffer, startIndex);
            }

            /// <summary>
            /// 返回由字节数组中前两个字节转换来的 16 位无符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由两个字节构成的 16 位无符号整数。</returns>
            [Preserve]
            public static ushort GetUInt16(byte[] value)
            {
                return BitConverter.ToUInt16(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的两个字节转换来的 16 位无符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由两个字节构成的 16 位无符号整数。</returns>
            [Preserve]
            public static ushort GetUInt16(byte[] value, int startIndex)
            {
                return BitConverter.ToUInt16(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 32 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(int value)
            {
                byte[] buffer = new byte[4];
                GetBytes(value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 32 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static void GetBytes(int value, byte[] buffer)
            {
                GetBytes(value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 32 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static unsafe void GetBytes(int value, byte[] buffer, int startIndex)
            {
                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0 || startIndex + 4 > buffer.Length)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                fixed (byte* valueRef = buffer)
                {
                    *(int*)(valueRef + startIndex) = value;
                }
            }

            /// <summary>
            /// 返回由字节数组中前四个字节转换来的 32 位有符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由四个字节构成的 32 位有符号整数。</returns>
            [Preserve]
            public static int GetInt32(byte[] value)
            {
                return BitConverter.ToInt32(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的四个字节转换来的 32 位有符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由四个字节构成的 32 位有符号整数。</returns>
            [Preserve]
            public static int GetInt32(byte[] value, int startIndex)
            {
                return BitConverter.ToInt32(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 32 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(uint value)
            {
                byte[] buffer = new byte[4];
                GetBytes((int)value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 32 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static void GetBytes(uint value, byte[] buffer)
            {
                GetBytes((int)value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 32 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static void GetBytes(uint value, byte[] buffer, int startIndex)
            {
                GetBytes((int)value, buffer, startIndex);
            }

            /// <summary>
            /// 返回由字节数组中前四个字节转换来的 32 位无符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由四个字节构成的 32 位无符号整数。</returns>
            [Preserve]
            public static uint GetUInt32(byte[] value)
            {
                return BitConverter.ToUInt32(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的四个字节转换来的 32 位无符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由四个字节构成的 32 位无符号整数。</returns>
            [Preserve]
            public static uint GetUInt32(byte[] value, int startIndex)
            {
                return BitConverter.ToUInt32(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 64 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(long value)
            {
                byte[] buffer = new byte[8];
                GetBytes(value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 64 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static void GetBytes(long value, byte[] buffer)
            {
                GetBytes(value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 64 位有符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static unsafe void GetBytes(long value, byte[] buffer, int startIndex)
            {
                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0 || startIndex + 8 > buffer.Length)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                fixed (byte* valueRef = buffer)
                {
                    *(long*)(valueRef + startIndex) = value;
                }
            }

            /// <summary>
            /// 返回由字节数组中前八个字节转换来的 64 位有符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由八个字节构成的 64 位有符号整数。</returns>
            [Preserve]
            public static long GetInt64(byte[] value)
            {
                return BitConverter.ToInt64(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的八个字节转换来的 64 位有符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由八个字节构成的 64 位有符号整数。</returns>
            [Preserve]
            public static long GetInt64(byte[] value, int startIndex)
            {
                return BitConverter.ToInt64(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 64 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(ulong value)
            {
                byte[] buffer = new byte[8];
                GetBytes((long)value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 64 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static void GetBytes(ulong value, byte[] buffer)
            {
                GetBytes((long)value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的 64 位无符号整数值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static void GetBytes(ulong value, byte[] buffer, int startIndex)
            {
                GetBytes((long)value, buffer, startIndex);
            }

            /// <summary>
            /// 返回由字节数组中前八个字节转换来的 64 位无符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由八个字节构成的 64 位无符号整数。</returns>
            [Preserve]
            public static ulong GetUInt64(byte[] value)
            {
                return BitConverter.ToUInt64(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的八个字节转换来的 64 位无符号整数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由八个字节构成的 64 位无符号整数。</returns>
            [Preserve]
            public static ulong GetUInt64(byte[] value, int startIndex)
            {
                return BitConverter.ToUInt64(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的单精度浮点值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static unsafe byte[] GetBytes(float value)
            {
                byte[] buffer = new byte[4];
                GetBytes(*(int*)&value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的单精度浮点值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static unsafe void GetBytes(float value, byte[] buffer)
            {
                GetBytes(*(int*)&value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的单精度浮点值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static unsafe void GetBytes(float value, byte[] buffer, int startIndex)
            {
                GetBytes(*(int*)&value, buffer, startIndex);
            }

            /// <summary>
            /// 返回由字节数组中前四个字节转换来的单精度浮点数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由四个字节构成的单精度浮点数。</returns>
            [Preserve]
            public static float GetSingle(byte[] value)
            {
                return BitConverter.ToSingle(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的四个字节转换来的单精度浮点数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由四个字节构成的单精度浮点数。</returns>
            [Preserve]
            public static float GetSingle(byte[] value, int startIndex)
            {
                return BitConverter.ToSingle(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的双精度浮点值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static unsafe byte[] GetBytes(double value)
            {
                byte[] buffer = new byte[8];
                GetBytes(*(long*)&value, buffer, 0);
                return buffer;
            }

            /// <summary>
            /// 以字节数组的形式获取指定的双精度浮点值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            [Preserve]
            public static unsafe void GetBytes(double value, byte[] buffer)
            {
                GetBytes(*(long*)&value, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定的双精度浮点值。
            /// </summary>
            /// <param name="value">要转换的数字。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            [Preserve]
            public static unsafe void GetBytes(double value, byte[] buffer, int startIndex)
            {
                GetBytes(*(long*)&value, buffer, startIndex);
            }

            /// <summary>
            /// 返回由字节数组中前八个字节转换来的双精度浮点数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>由八个字节构成的双精度浮点数。</returns>
            [Preserve]
            public static double GetDouble(byte[] value)
            {
                return BitConverter.ToDouble(value, 0);
            }

            /// <summary>
            /// 返回由字节数组中指定位置的八个字节转换来的双精度浮点数。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <returns>由八个字节构成的双精度浮点数。</returns>
            [Preserve]
            public static double GetDouble(byte[] value, int startIndex)
            {
                return BitConverter.ToDouble(value, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取 UTF-8 编码的字符串。
            /// </summary>
            /// <param name="value">要转换的字符串。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(string value)
            {
                return GetBytes(value, Encoding.UTF8);
            }

            /// <summary>
            /// 以字节数组的形式获取 UTF-8 编码的字符串。
            /// </summary>
            /// <param name="value">要转换的字符串。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <returns>buffer 内实际填充了多少字节。</returns>
            [Preserve]
            public static int GetBytes(string value, byte[] buffer)
            {
                return GetBytes(value, Encoding.UTF8, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取 UTF-8 编码的字符串。
            /// </summary>
            /// <param name="value">要转换的字符串。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            /// <returns>buffer 内实际填充了多少字节。</returns>
            [Preserve]
            public static int GetBytes(string value, byte[] buffer, int startIndex)
            {
                return GetBytes(value, Encoding.UTF8, buffer, startIndex);
            }

            /// <summary>
            /// 以字节数组的形式获取指定编码的字符串。
            /// </summary>
            /// <param name="value">要转换的字符串。</param>
            /// <param name="encoding">要使用的编码。</param>
            /// <returns>用于存放结果的字节数组。</returns>
            [Preserve]
            public static byte[] GetBytes(string value, Encoding encoding)
            {
                if (value == null)
                {
                    throw new GameFrameworkException("Value is invalid.");
                }

                if (encoding == null)
                {
                    throw new GameFrameworkException("Encoding is invalid.");
                }

                return encoding.GetBytes(value);
            }

            /// <summary>
            /// 以字节数组的形式获取指定编码的字符串。
            /// </summary>
            /// <param name="value">要转换的字符串。</param>
            /// <param name="encoding">要使用的编码。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <returns>buffer 内实际填充了多少字节。</returns>
            [Preserve]
            public static int GetBytes(string value, Encoding encoding, byte[] buffer)
            {
                return GetBytes(value, encoding, buffer, 0);
            }

            /// <summary>
            /// 以字节数组的形式获取指定编码的字符串。
            /// </summary>
            /// <param name="value">要转换的字符串。</param>
            /// <param name="encoding">要使用的编码。</param>
            /// <param name="buffer">用于存放结果的字节数组。</param>
            /// <param name="startIndex">buffer 内的起始位置。</param>
            /// <returns>buffer 内实际填充了多少字节。</returns>
            [Preserve]
            public static int GetBytes(string value, Encoding encoding, byte[] buffer, int startIndex)
            {
                if (value == null)
                {
                    throw new GameFrameworkException("Value is invalid.");
                }

                if (encoding == null)
                {
                    throw new GameFrameworkException("Encoding is invalid.");
                }

                return encoding.GetBytes(value, 0, value.Length, buffer, startIndex);
            }

            /// <summary>
            /// 返回由字节数组使用 UTF-8 编码转换成的字符串。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <returns>转换后的字符串。</returns>
            [Preserve]
            public static string GetString(byte[] value)
            {
                return GetString(value, Encoding.UTF8);
            }

            /// <summary>
            /// 返回由字节数组使用指定编码转换成的字符串。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="encoding">要使用的编码。</param>
            /// <returns>转换后的字符串。</returns>
            [Preserve]
            public static string GetString(byte[] value, Encoding encoding)
            {
                if (value == null)
                {
                    throw new GameFrameworkException("Value is invalid.");
                }

                if (encoding == null)
                {
                    throw new GameFrameworkException("Encoding is invalid.");
                }

                return encoding.GetString(value);
            }

            /// <summary>
            /// 返回由字节数组使用 UTF-8 编码转换成的字符串。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <param name="length">长度。</param>
            /// <returns>转换后的字符串。</returns>
            [Preserve]
            public static string GetString(byte[] value, int startIndex, int length)
            {
                return GetString(value, startIndex, length, Encoding.UTF8);
            }

            /// <summary>
            /// 返回由字节数组使用指定编码转换成的字符串。
            /// </summary>
            /// <param name="value">字节数组。</param>
            /// <param name="startIndex">value 内的起始位置。</param>
            /// <param name="length">长度。</param>
            /// <param name="encoding">要使用的编码。</param>
            /// <returns>转换后的字符串。</returns>
            [Preserve]
            public static string GetString(byte[] value, int startIndex, int length, Encoding encoding)
            {
                if (value == null)
                {
                    throw new GameFrameworkException("Value is invalid.");
                }

                if (encoding == null)
                {
                    throw new GameFrameworkException("Encoding is invalid.");
                }

                return encoding.GetString(value, startIndex, length);
            }
        }
    }
}