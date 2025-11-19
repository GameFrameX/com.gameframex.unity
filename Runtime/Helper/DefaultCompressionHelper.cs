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