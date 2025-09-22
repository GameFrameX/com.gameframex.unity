// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using GameFrameX;
using ICSharpCode.SharpZipLib.GZip;
using System;
using System.IO;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 默认压缩解压缩辅助器。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public class DefaultCompressionHelper : Utility.Compression.ICompressionHelper
    {
        private const int CachedBytesLength = 0x1000;
        private readonly byte[] m_CachedBytes = new byte[CachedBytesLength];

        /// <summary>
        /// 压缩数据。
        /// </summary>
        /// <param name="bytes">要压缩的数据的二进制流。</param>
        /// <param name="offset">要压缩的数据的二进制流的偏移。</param>
        /// <param name="length">要压缩的数据的二进制流的长度。</param>
        /// <param name="compressedStream">压缩后的数据的二进制流。</param>
        /// <returns>是否压缩数据成功。</returns>
        [UnityEngine.Scripting.Preserve]
        public bool Compress(byte[] bytes, int offset, int length, Stream compressedStream)
        {
            if (bytes == null)
            {
                return false;
            }

            if (offset < 0 || length < 0 || offset + length > bytes.Length)
            {
                return false;
            }

            if (compressedStream == null)
            {
                return false;
            }

            try
            {
                GZipOutputStream gZipOutputStream = new GZipOutputStream(compressedStream);
                gZipOutputStream.Write(bytes, offset, length);
                gZipOutputStream.Finish();
                ProcessHeader(compressedStream);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 压缩数据。
        /// </summary>
        /// <param name="stream">要压缩的数据的二进制流。</param>
        /// <param name="compressedStream">压缩后的数据的二进制流。</param>
        /// <returns>是否压缩数据成功。</returns>
        [UnityEngine.Scripting.Preserve]
        public bool Compress(Stream stream, Stream compressedStream)
        {
            if (stream == null)
            {
                return false;
            }

            if (compressedStream == null)
            {
                return false;
            }

            try
            {
                GZipOutputStream gZipOutputStream = new GZipOutputStream(compressedStream);
                int bytesRead = 0;
                while ((bytesRead = stream.Read(m_CachedBytes, 0, CachedBytesLength)) > 0)
                {
                    gZipOutputStream.Write(m_CachedBytes, 0, bytesRead);
                }

                gZipOutputStream.Finish();
                ProcessHeader(compressedStream);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Array.Clear(m_CachedBytes, 0, CachedBytesLength);
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
        [UnityEngine.Scripting.Preserve]
        public bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream)
        {
            if (bytes == null)
            {
                return false;
            }

            if (offset < 0 || length < 0 || offset + length > bytes.Length)
            {
                return false;
            }

            if (decompressedStream == null)
            {
                return false;
            }

            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream(bytes, offset, length, false);
                using (GZipInputStream gZipInputStream = new GZipInputStream(memoryStream))
                {
                    int bytesRead = 0;
                    while ((bytesRead = gZipInputStream.Read(m_CachedBytes, 0, CachedBytesLength)) > 0)
                    {
                        decompressedStream.Write(m_CachedBytes, 0, bytesRead);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                    memoryStream = null;
                }

                Array.Clear(m_CachedBytes, 0, CachedBytesLength);
            }
        }

        /// <summary>
        /// 解压缩数据。
        /// </summary>
        /// <param name="stream">要解压缩的数据的二进制流。</param>
        /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
        /// <returns>是否解压缩数据成功。</returns>
        [UnityEngine.Scripting.Preserve]
        public bool Decompress(Stream stream, Stream decompressedStream)
        {
            if (stream == null)
            {
                return false;
            }

            if (decompressedStream == null)
            {
                return false;
            }

            try
            {
                GZipInputStream gZipInputStream = new GZipInputStream(stream);
                int bytesRead = 0;
                while ((bytesRead = gZipInputStream.Read(m_CachedBytes, 0, CachedBytesLength)) > 0)
                {
                    decompressedStream.Write(m_CachedBytes, 0, bytesRead);
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Array.Clear(m_CachedBytes, 0, CachedBytesLength);
            }
        }

        private static void ProcessHeader(Stream compressedStream)
        {
            if (compressedStream.Length >= 8L)
            {
                long current = compressedStream.Position;
                compressedStream.Position = 4L;
                compressedStream.WriteByte(25);
                compressedStream.WriteByte(134);
                compressedStream.WriteByte(2);
                compressedStream.WriteByte(32);
                compressedStream.Position = current;
            }
        }
    }
}