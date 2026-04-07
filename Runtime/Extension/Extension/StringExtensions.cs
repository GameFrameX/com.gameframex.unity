using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 字符串扩展方法类
    /// </summary>
    /// <remarks>
    /// Provides extension methods for string manipulation including comparison, conversion, and validation.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class StringExtension
    {
        /// <summary>
        /// 快速比较两个字符串内容是否一致
        /// </summary>
        /// <remarks>
        /// Quickly compares two strings for equality without using string.Equals.
        /// Compares character by character from the end of the strings.
        /// </remarks>
        /// <param name="self">当前字符串 / The current string</param>
        /// <param name="target">对比的目标字符串 / The target string to compare with</param>
        /// <returns>如果两个字符串内容相同返回true，否则返回false / True if the strings are equal, otherwise false</returns>
        /// <exception cref="ArgumentNullException">当前对象为空 / Thrown when the current object is null</exception>
        [UnityEngine.Scripting.Preserve]
        public static bool EqualsFast(this string self, string target)
        {
            if (self == null)
            {
                return target == null;
            }

            if (target == null)
            {
                return false;
            }

            if (self.Length != target.Length)
            {
                return false;
            }

            int ap = self.Length - 1;
            int bp = target.Length - 1;

            while (ap >= 0 && bp >= 0 && self[ap] == target[bp])
            {
                ap--;
                bp--;
            }

            return (bp < 0);
        }

        /// <summary>
        /// 判断字符串是否以目标字符串结尾
        /// </summary>
        /// <remarks>
        /// Quickly determines whether the string ends with the specified target string.
        /// Compares character by character from the end of the strings.
        /// </remarks>
        /// <param name="self">当前字符串 / The current string</param>
        /// <param name="target">目标字符串 / The target string to check for</param>
        /// <returns>如果字符串以目标字符串结尾返回true，否则返回false / True if the string ends with the target, otherwise false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool EndsWithFast(this string self, string target)
        {
            if (self == null || target == null)
            {
                return false;
            }

            int ap = self.Length - 1;
            int bp = target.Length - 1;

            while (ap >= 0 && bp >= 0 && self[ap] == target[bp])
            {
                ap--;
                bp--;
            }

            return (bp < 0);
        }

        /// <summary>
        /// 判断字符串是否以目标字符串开始
        /// </summary>
        /// <remarks>
        /// Quickly determines whether the string starts with the specified target string.
        /// Compares character by character from the beginning of the strings.
        /// </remarks>
        /// <param name="self">当前字符串 / The current string</param>
        /// <param name="target">目标字符串 / The target string to check for</param>
        /// <returns>如果字符串以目标字符串开始返回true，否则返回false / True if the string starts with the target, otherwise false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool StartsWithFast(this string self, string target)
        {
            if (self == null || target == null)
            {
                return false;
            }

            int aLen = self.Length;
            int bLen = target.Length;

            int ap = 0;
            int bp = 0;

            while (ap < aLen && bp < bLen && self[ap] == target[bp])
            {
                ap++;
                bp++;
            }

            return (bp == bLen);
        }

        /// <summary>
        /// 字符串转字符数组
        /// </summary>
        /// <remarks>
        /// Converts the string to an enumerable collection of bytes using the default encoding.
        /// </remarks>
        /// <param name="self">当前字符串 / The current string</param>
        /// <returns>字节数组的可枚举集合 / An enumerable collection of bytes</returns>
        [UnityEngine.Scripting.Preserve]
        public static IEnumerable<byte> ToBytes(this string self)
        {
            byte[] byteArray = Encoding.Default.GetBytes(self);
            return byteArray;
        }

        /// <summary>
        /// 字符串转字符数组
        /// </summary>
        /// <remarks>
        /// Converts the string to a byte array using the default encoding.
        /// </remarks>
        /// <param name="self">当前字符串 / The current string</param>
        /// <returns>字节数组 / A byte array</returns>
        [UnityEngine.Scripting.Preserve]
        public static byte[] ToByteArray(this string self)
        {
            byte[] byteArray = Encoding.Default.GetBytes(self);
            return byteArray;
        }

        /// <summary>
        /// 字符串转UTF8字符数组
        /// </summary>
        /// <remarks>
        /// Converts the string to a UTF-8 encoded byte array.
        /// </remarks>
        /// <param name="self">当前字符串 / The current string</param>
        /// <returns>UTF-8编码的字节数组 / A UTF-8 encoded byte array</returns>
        [UnityEngine.Scripting.Preserve]
        public static byte[] ToUtf8(this string self)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(self);
            return byteArray;
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <remarks>
        /// Converts a hexadecimal string to a byte array.
        /// The hex string must have an even number of characters.
        /// </remarks>
        /// <param name="hexString">16进制字符串 / The hexadecimal string</param>
        /// <returns>转换后的字节数组 / The converted byte array</returns>
        /// <exception cref="ArgumentException">字符串字符数不是偶数引发异常 / Thrown when the string has an odd number of characters</exception>
        [UnityEngine.Scripting.Preserve]
        public static byte[] HexToBytes(this string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}",
                                                          hexString));
            }

            var hexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < hexAsBytes.Length; index++)
            {
                string byteValue = "";
                byteValue += hexString[index * 2];
                byteValue += hexString[index * 2 + 1];
                hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return hexAsBytes;
        }

        /// <summary>
        /// 指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified string is null, empty, or consists only of white-space characters.
        /// </remarks>
        /// <param name="self">要检查的字符串 / The string to check</param>
        /// <returns>如果字符串为null、空或仅由空白字符组成则返回true，否则返回false / True if the string is null, empty, or white-space only, otherwise false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsNullOrWhiteSpace(this string self)
        {
            return string.IsNullOrWhiteSpace(self);
        }

        /// <summary>
        /// 指定的字符串是 null 还是 Empty 字符串。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified string is null or an empty string.
        /// </remarks>
        /// <param name="self">要检查的字符串 / The string to check</param>
        /// <returns>如果字符串为null或空则返回true，否则返回false / True if the string is null or empty, otherwise false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        /// <summary>
        /// 指定的字符串[不]是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified string is NOT null, empty, or consisting only of white-space characters.
        /// This is the inverse of IsNullOrWhiteSpace.
        /// </remarks>
        /// <param name="self">要检查的字符串 / The string to check</param>
        /// <returns>如果字符串不为null、不为空且不全为空白字符则返回true，否则返回false / True if the string is not null, not empty, and not white-space only, otherwise false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsNotNullOrWhiteSpace(this string self)
        {
            return !self.IsNullOrWhiteSpace();
        }

        /// <summary>
        /// 指定的字符串[不]是 null 还是 Empty 字符串。
        /// </summary>
        /// <remarks>
        /// Determines whether the specified string is NOT null or an empty string.
        /// This is the inverse of IsNullOrEmpty.
        /// </remarks>
        /// <param name="self">要检查的字符串 / The string to check</param>
        /// <returns>如果字符串不为null且不为空则返回true，否则返回false / True if the string is not null and not empty, otherwise false</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsNotNullOrEmpty(this string self)
        {
            return !self.IsNullOrEmpty();
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <remarks>
        /// Formats the string with the specified arguments using string.Format.
        /// </remarks>
        /// <param name="text">格式字符串 / The format string</param>
        /// <param name="args">格式化参数 / The format arguments</param>
        /// <returns>格式化后的字符串 / The formatted string</returns>
        [UnityEngine.Scripting.Preserve]
        public static string Format(this string text, params object[] args)
        {
            return string.Format(text, args);
        }

        /// <summary>
        /// 将[\n、\t、\r、空格]替换为空,并返回
        /// </summary>
        /// <remarks>
        /// Removes all newline, tab, carriage return, and space characters from the string.
        /// </remarks>
        /// <param name="self">原始字符串 / The original string</param>
        /// <returns>去除空白字符后的字符串 / The string with whitespace characters removed</returns>
        [UnityEngine.Scripting.Preserve]
        public static string TrimEmpty(this string self)
        {
            self = self.Replace("\n", string.Empty).Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty);
            return self;
        }

        /// <summary>
        /// 将驼峰命名的字符串转换为下划线分隔的小写形式（蛇形命名）。
        /// </summary>
        /// <remarks>
        /// Converts a camelCase or PascalCase string to snake_case format.
        /// Preserves leading underscores.
        /// </remarks>
        /// <param name="input">要转换的字符串 / The string to convert</param>
        /// <returns>转换后的蛇形命名字符串。如果输入为null或空，则返回原字符串 / The converted snake_case string, or the original if input is null or empty</returns>
        [UnityEngine.Scripting.Preserve]
        public static string ConvertToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        /// <summary>
        /// 匹配中文正则表达式
        /// </summary>
        private static readonly Regex CnReg = new Regex(@"[\u4e00-\u9fa5]");

        /// <summary>
        /// 替换中文为空字符串
        /// </summary>
        /// <remarks>
        /// Removes all Chinese characters from the string.
        /// </remarks>
        /// <param name="self">原始字符串 / The original string</param>
        /// <returns>去除中文字符后的字符串 / The string with Chinese characters removed</returns>
        [UnityEngine.Scripting.Preserve]
        public static string TrimZhCn(this string self)
        {
            self = CnReg.Replace(self, string.Empty);
            return self;
        }

        /// <summary>
        /// 将字符串按指定分隔符分割并转换为整型数组
        /// </summary>
        /// <remarks>
        /// Splits the string by the specified separator and converts each part to an integer.
        /// </remarks>
        /// <param name="str">要分割的字符串 / The string to split</param>
        /// <param name="sep">分隔符，默认为'+' / The separator character, defaults to '+'</param>
        /// <returns>整型数组 / An array of integers</returns>
        [UnityEngine.Scripting.Preserve]
        public static int[] SplitToIntArray(this string str, char sep = '+')
        {
            if (string.IsNullOrEmpty(str))
                return Array.Empty<int>();

            var arr = str.Split(sep);
            int[] ret = new int[arr.Length];
            for (int i = 0; i < arr.Length; ++i)
            {
                if (int.TryParse(arr[i], out var t))
                    ret[i] = t;
            }

            return ret;
        }

        /// <summary>
        /// 将字符串按两级分隔符分割并转换为二维整型数组
        /// </summary>
        /// <remarks>
        /// Splits the string by the first separator, then splits each part by the second separator and converts to integer arrays.
        /// </remarks>
        /// <param name="str">要分割的字符串 / The string to split</param>
        /// <param name="sep1">第一级分隔符，默认为';' / The first level separator, defaults to ';'</param>
        /// <param name="sep2">第二级分隔符，默认为'+' / The second level separator, defaults to '+'</param>
        /// <returns>二维整型数组 / A two-dimensional array of integers</returns>
        [UnityEngine.Scripting.Preserve]
        public static int[][] SplitTo2IntArray(this string str, char sep1 = ';', char sep2 = '+')
        {
            if (string.IsNullOrEmpty(str))
                return Array.Empty<int[]>();

            var arr = str.Split(sep1);
            if (arr.Length <= 0)
                return Array.Empty<int[]>();

            int[][] ret = new int[arr.Length][];

            for (int i = 0; i < arr.Length; ++i)
                ret[i] = arr[i].SplitToIntArray(sep2);
            return ret;
        }

        /// <summary>
        /// 根据字符串创建目录,递归
        /// </summary>
        /// <remarks>
        /// Creates a directory at the specified path. If isFile is true, extracts the directory path from the file path.
        /// Creates all parent directories recursively if they don't exist.
        /// </remarks>
        /// <param name="path">目录路径或文件路径 / The directory path or file path</param>
        /// <param name="isFile">是否为文件路径 / Whether the path is a file path</param>
        [UnityEngine.Scripting.Preserve]
        public static void CreateAsDirectory(this string path, bool isFile = false)
        {
            if (isFile)
            {
                path = Path.GetDirectoryName(path);
            }

            if (!Directory.Exists(path))
            {
                CreateAsDirectory(path, true);
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 从指定字符串中的指定位置处开始读取一行。
        /// </summary>
        /// <remarks>
        /// Reads a line from the string starting at the specified position.
        /// Updates the position to point to the start of the next line after reading.
        /// Handles both \r\n and \n line endings.
        /// </remarks>
        /// <param name="rawString">指定的字符串 / The string to read from</param>
        /// <param name="position">从指定位置处开始读取一行，读取后将返回下一行开始的位置 / The starting position; updated to the next line's start position after reading</param>
        /// <returns>读取的一行字符串 / The read line string</returns>
        [UnityEngine.Scripting.Preserve]
        public static string ReadLine(this string rawString, ref int position)
        {
            if (position < 0)
            {
                return null;
            }

            int length = rawString.Length;
            int offset = position;
            while (offset < length)
            {
                char ch = rawString[offset];
                switch (ch)
                {
                    case '\r':
                    case '\n':
                        if (offset > position)
                        {
                            string line = rawString.Substring(position, offset - position);
                            position = offset + 1;
                            if ((ch == '\r') && (position < length) && (rawString[position] == '\n'))
                            {
                                position++;
                            }

                            return line;
                        }

                        offset++;
                        position++;
                        break;

                    default:
                        offset++;
                        break;
                }
            }

            if (offset > position)
            {
                string line = rawString.Substring(position, offset - position);
                position = offset;
                return line;
            }

            return null;
        }
    }
}