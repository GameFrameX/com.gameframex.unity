// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.IO;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 压缩解压缩相关的实用函数。
        /// </summary>
        [Preserve]
        public static partial class Compression
        {
            private static ICompressionHelper s_CompressionHelper = null;

            /// <summary>
            /// 设置压缩解压缩辅助器。
            /// </summary>
            /// <param name="compressionHelper">要设置的压缩解压缩辅助器。</param>
            [Preserve]
            public static void SetCompressionHelper(ICompressionHelper compressionHelper)
            {
                s_CompressionHelper = compressionHelper;
            }

            /// <summary>
            /// 压缩数据。
            /// </summary>
            /// <param name="bytes">要压缩的数据的二进制流。</param>
            /// <returns>压缩后的数据的二进制流。</returns>
            [Preserve]
            public static byte[] Compress(byte[] bytes)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                return Compress(bytes, 0, bytes.Length);
            }

            /// <summary>
            /// 压缩数据。
            /// </summary>
            /// <param name="bytes">要压缩的数据的二进制流。</param>
            /// <param name="compressedStream">压缩后的数据的二进制流。</param>
            /// <returns>是否压缩数据成功。</returns>
            [Preserve]
            public static bool Compress(byte[] bytes, Stream compressedStream)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                return Compress(bytes, 0, bytes.Length, compressedStream);
            }

            /// <summary>
            /// 压缩数据。
            /// </summary>
            /// <param name="bytes">要压缩的数据的二进制流。</param>
            /// <param name="offset">要压缩的数据的二进制流的偏移。</param>
            /// <param name="length">要压缩的数据的二进制流的长度。</param>
            /// <returns>压缩后的数据的二进制流。</returns>
            [Preserve]
            public static byte[] Compress(byte[] bytes, int offset, int length)
            {
                using (MemoryStream compressedStream = new MemoryStream())
                {
                    if (Compress(bytes, offset, length, compressedStream))
                    {
                        return compressedStream.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            /// <summary>
            /// 压缩数据。
            /// </summary>
            /// <param name="bytes">要压缩的数据的二进制流。</param>
            /// <param name="offset">要压缩的数据的二进制流的偏移。</param>
            /// <param name="length">要压缩的数据的二进制流的长度。</param>
            /// <param name="compressedStream">压缩后的数据的二进制流。</param>
            /// <returns>是否压缩数据成功。</returns>
            [Preserve]
            public static bool Compress(byte[] bytes, int offset, int length, Stream compressedStream)
            {
                if (s_CompressionHelper == null)
                {
                    throw new GameFrameworkException("Compressed helper is invalid.");
                }

                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                if (offset < 0 || length < 0 || offset + length > bytes.Length)
                {
                    throw new GameFrameworkException("Offset or length is invalid.");
                }

                if (compressedStream == null)
                {
                    throw new GameFrameworkException("Compressed stream is invalid.");
                }

                try
                {
                    return s_CompressionHelper.Compress(bytes, offset, length, compressedStream);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not compress with exception '{0}'.", exception), exception);
                }
            }

            /// <summary>
            /// 压缩数据。
            /// </summary>
            /// <param name="stream">要压缩的数据的二进制流。</param>
            /// <returns>压缩后的数据的二进制流。</returns>
            [Preserve]
            public static byte[] Compress(Stream stream)
            {
                using (MemoryStream compressedStream = new MemoryStream())
                {
                    if (Compress(stream, compressedStream))
                    {
                        return compressedStream.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            /// <summary>
            /// 压缩数据。
            /// </summary>
            /// <param name="stream">要压缩的数据的二进制流。</param>
            /// <param name="compressedStream">压缩后的数据的二进制流。</param>
            /// <returns>是否压缩数据成功。</returns>
            [Preserve]
            public static bool Compress(Stream stream, Stream compressedStream)
            {
                if (s_CompressionHelper == null)
                {
                    throw new GameFrameworkException("Compressed helper is invalid.");
                }

                if (stream == null)
                {
                    throw new GameFrameworkException("Stream is invalid.");
                }

                if (compressedStream == null)
                {
                    throw new GameFrameworkException("Compressed stream is invalid.");
                }

                try
                {
                    return s_CompressionHelper.Compress(stream, compressedStream);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not compress with exception '{0}'.", exception), exception);
                }
            }

            /// <summary>
            /// 解压缩数据。
            /// </summary>
            /// <param name="bytes">要解压缩的数据的二进制流。</param>
            /// <returns>解压缩后的数据的二进制流。</returns>
            [Preserve]
            public static byte[] Decompress(byte[] bytes)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                return Decompress(bytes, 0, bytes.Length);
            }

            /// <summary>
            /// 解压缩数据。
            /// </summary>
            /// <param name="bytes">要解压缩的数据的二进制流。</param>
            /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
            /// <returns>是否解压缩数据成功。</returns>
            [Preserve]
            public static bool Decompress(byte[] bytes, Stream decompressedStream)
            {
                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                return Decompress(bytes, 0, bytes.Length, decompressedStream);
            }

            /// <summary>
            /// 解压缩数据。
            /// </summary>
            /// <param name="bytes">要解压缩的数据的二进制流。</param>
            /// <param name="offset">要解压缩的数据的二进制流的偏移。</param>
            /// <param name="length">要解压缩的数据的二进制流的长度。</param>
            /// <returns>解压缩后的数据的二进制流。</returns>
            [Preserve]
            public static byte[] Decompress(byte[] bytes, int offset, int length)
            {
                using (MemoryStream decompressedStream = new MemoryStream())
                {
                    if (Decompress(bytes, offset, length, decompressedStream))
                    {
                        return decompressedStream.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            /// <summary>
            /// 解压缩数据。
            /// </summary>
            /// <param name="bytes">要解压缩的数据的二进制流。</param>
            /// <param name="offset">要解压缩的数据的二进制流的偏移。</param>
            /// <param name="length">要解压缩的数据的二进制流的长度。</param>
            /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
            /// <returns>是否解压缩数据成功。</returns>
            [Preserve]
            public static bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream)
            {
                if (s_CompressionHelper == null)
                {
                    throw new GameFrameworkException("Compressed helper is invalid.");
                }

                if (bytes == null)
                {
                    throw new GameFrameworkException("Bytes is invalid.");
                }

                if (offset < 0 || length < 0 || offset + length > bytes.Length)
                {
                    throw new GameFrameworkException("Offset or length is invalid.");
                }

                if (decompressedStream == null)
                {
                    throw new GameFrameworkException("Decompressed stream is invalid.");
                }

                try
                {
                    return s_CompressionHelper.Decompress(bytes, offset, length, decompressedStream);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not decompress with exception '{0}'.", exception), exception);
                }
            }

            /// <summary>
            /// 解压缩数据。
            /// </summary>
            /// <param name="stream">要解压缩的数据的二进制流。</param>
            /// <returns>是否解压缩数据成功。</returns>
            [Preserve]
            public static byte[] Decompress(Stream stream)
            {
                using (MemoryStream decompressedStream = new MemoryStream())
                {
                    if (Decompress(stream, decompressedStream))
                    {
                        return decompressedStream.ToArray();
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            /// <summary>
            /// 解压缩数据。
            /// </summary>
            /// <param name="stream">要解压缩的数据的二进制流。</param>
            /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
            /// <returns>是否解压缩数据成功。</returns>
            [Preserve]
            public static bool Decompress(Stream stream, Stream decompressedStream)
            {
                if (s_CompressionHelper == null)
                {
                    throw new GameFrameworkException("Compressed helper is invalid.");
                }

                if (stream == null)
                {
                    throw new GameFrameworkException("Stream is invalid.");
                }

                if (decompressedStream == null)
                {
                    throw new GameFrameworkException("Decompressed stream is invalid.");
                }

                try
                {
                    return s_CompressionHelper.Decompress(stream, decompressedStream);
                }
                catch (Exception exception)
                {
                    if (exception is GameFrameworkException)
                    {
                        throw;
                    }

                    throw new GameFrameworkException(Text.Format("Can not decompress with exception '{0}'.", exception), exception);
                }
            }
        }
    }
}