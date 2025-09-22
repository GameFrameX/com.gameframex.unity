// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.IO;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        public static partial class Compression
        {
            /// <summary>
            /// 压缩解压缩辅助器接口。
            /// </summary>
            public interface ICompressionHelper
            {
                /// <summary>
                /// 压缩数据。
                /// </summary>
                /// <param name="bytes">要压缩的数据的二进制流。</param>
                /// <param name="offset">要压缩的数据的二进制流的偏移。</param>
                /// <param name="length">要压缩的数据的二进制流的长度。</param>
                /// <param name="compressedStream">压缩后的数据的二进制流。</param>
                /// <returns>是否压缩数据成功。</returns>
                bool Compress(byte[] bytes, int offset, int length, Stream compressedStream);

                /// <summary>
                /// 压缩数据。
                /// </summary>
                /// <param name="stream">要压缩的数据的二进制流。</param>
                /// <param name="compressedStream">压缩后的数据的二进制流。</param>
                /// <returns>是否压缩数据成功。</returns>
                bool Compress(Stream stream, Stream compressedStream);

                /// <summary>
                /// 解压缩数据。
                /// </summary>
                /// <param name="bytes">要解压缩的数据的二进制流。</param>
                /// <param name="offset">要解压缩的数据的二进制流的偏移。</param>
                /// <param name="length">要解压缩的数据的二进制流的长度。</param>
                /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
                /// <returns>是否解压缩数据成功。</returns>
                bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream);

                /// <summary>
                /// 解压缩数据。
                /// </summary>
                /// <param name="stream">要解压缩的数据的二进制流。</param>
                /// <param name="decompressedStream">解压缩后的数据的二进制流。</param>
                /// <returns>是否解压缩数据成功。</returns>
                bool Decompress(Stream stream, Stream decompressedStream);
            }
        }
    }
}