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

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// Marshal 相关的实用函数。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static class Marshal
        {
            private const int BlockSize = 1024 * 4;
            private static IntPtr s_CachedHGlobalPtr = IntPtr.Zero;
            private static int s_CachedHGlobalSize = 0;

            /// <summary>
            /// 获取缓存的从进程的非托管内存中分配的内存的大小。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public static int CachedHGlobalSize
            {
                get { return s_CachedHGlobalSize; }
            }

            /// <summary>
            /// 确保从进程的非托管内存中分配足够大小的内存并缓存。
            /// </summary>
            /// <param name="ensureSize">要确保从进程的非托管内存中分配内存的大小。</param>
            [UnityEngine.Scripting.Preserve]
            public static void EnsureCachedHGlobalSize(int ensureSize)
            {
                if (ensureSize < 0)
                {
                    throw new GameFrameworkException("Ensure size is invalid.");
                }

                if (s_CachedHGlobalPtr == IntPtr.Zero || s_CachedHGlobalSize < ensureSize)
                {
                    FreeCachedHGlobal();
                    int size = (ensureSize - 1 + BlockSize) / BlockSize * BlockSize;
                    s_CachedHGlobalPtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
                    s_CachedHGlobalSize = size;
                }
            }

            /// <summary>
            /// 释放缓存的从进程的非托管内存中分配的内存。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public static void FreeCachedHGlobal()
            {
                if (s_CachedHGlobalPtr != IntPtr.Zero)
                {
                    System.Runtime.InteropServices.Marshal.FreeHGlobal(s_CachedHGlobalPtr);
                    s_CachedHGlobalPtr = IntPtr.Zero;
                    s_CachedHGlobalSize = 0;
                }
            }

            /// <summary>
            /// 将数据从对象转换为二进制流。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="structure">要转换的对象。</param>
            /// <returns>存储转换结果的二进制流。</returns>
            [UnityEngine.Scripting.Preserve]
            public static byte[] StructureToBytes<T>(T structure)
            {
                return StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)));
            }

            /// <summary>
            /// 将数据从对象转换为二进制流。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="structure">要转换的对象。</param>
            /// <param name="structureSize">要转换的对象的大小。</param>
            /// <returns>存储转换结果的二进制流。</returns>
            [UnityEngine.Scripting.Preserve]
            internal static byte[] StructureToBytes<T>(T structure, int structureSize)
            {
                if (structureSize < 0)
                {
                    throw new GameFrameworkException("Structure size is invalid.");
                }

                EnsureCachedHGlobalSize(structureSize);
                System.Runtime.InteropServices.Marshal.StructureToPtr(structure, s_CachedHGlobalPtr, true);
                byte[] result = new byte[structureSize];
                System.Runtime.InteropServices.Marshal.Copy(s_CachedHGlobalPtr, result, 0, structureSize);
                return result;
            }

            /// <summary>
            /// 将数据从对象转换为二进制流。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="structure">要转换的对象。</param>
            /// <param name="result">存储转换结果的二进制流。</param>
            [UnityEngine.Scripting.Preserve]
            public static void StructureToBytes<T>(T structure, byte[] result)
            {
                StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), result, 0);
            }

            /// <summary>
            /// 将数据从对象转换为二进制流。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="structure">要转换的对象。</param>
            /// <param name="structureSize">要转换的对象的大小。</param>
            /// <param name="result">存储转换结果的二进制流。</param>
            [UnityEngine.Scripting.Preserve]
            internal static void StructureToBytes<T>(T structure, int structureSize, byte[] result)
            {
                StructureToBytes(structure, structureSize, result, 0);
            }

            /// <summary>
            /// 将数据从对象转换为二进制流。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="structure">要转换的对象。</param>
            /// <param name="result">存储转换结果的二进制流。</param>
            /// <param name="startIndex">写入存储转换结果的二进制流的起始位置。</param>
            [UnityEngine.Scripting.Preserve]
            public static void StructureToBytes<T>(T structure, byte[] result, int startIndex)
            {
                StructureToBytes(structure, System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), result, startIndex);
            }

            /// <summary>
            /// 将数据从对象转换为二进制流。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="structure">要转换的对象。</param>
            /// <param name="structureSize">要转换的对象的大小。</param>
            /// <param name="result">存储转换结果的二进制流。</param>
            /// <param name="startIndex">写入存储转换结果的二进制流的起始位置。</param>
            [UnityEngine.Scripting.Preserve]
            internal static void StructureToBytes<T>(T structure, int structureSize, byte[] result, int startIndex)
            {
                if (structureSize < 0)
                {
                    throw new GameFrameworkException("Structure size is invalid.");
                }

                if (result == null)
                {
                    throw new GameFrameworkException("Result is invalid.");
                }

                if (startIndex < 0)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                if (startIndex + structureSize > result.Length)
                {
                    throw new GameFrameworkException("Result length is not enough.");
                }

                EnsureCachedHGlobalSize(structureSize);
                System.Runtime.InteropServices.Marshal.StructureToPtr(structure, s_CachedHGlobalPtr, true);
                System.Runtime.InteropServices.Marshal.Copy(s_CachedHGlobalPtr, result, startIndex, structureSize);
            }

            /// <summary>
            /// 将数据从二进制流转换为对象。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="buffer">要转换的二进制流。</param>
            /// <returns>存储转换结果的对象。</returns>
            [UnityEngine.Scripting.Preserve]
            public static T BytesToStructure<T>(byte[] buffer)
            {
                return BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), buffer, 0);
            }

            /// <summary>
            /// 将数据从二进制流转换为对象。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="buffer">要转换的二进制流。</param>
            /// <param name="startIndex">读取要转换的二进制流的起始位置。</param>
            /// <returns>存储转换结果的对象。</returns>
            [UnityEngine.Scripting.Preserve]
            public static T BytesToStructure<T>(byte[] buffer, int startIndex)
            {
                return BytesToStructure<T>(System.Runtime.InteropServices.Marshal.SizeOf(typeof(T)), buffer, startIndex);
            }

            /// <summary>
            /// 将数据从二进制流转换为对象。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="structureSize">要转换的对象的大小。</param>
            /// <param name="buffer">要转换的二进制流。</param>
            /// <returns>存储转换结果的对象。</returns>
            [UnityEngine.Scripting.Preserve]
            internal static T BytesToStructure<T>(int structureSize, byte[] buffer)
            {
                return BytesToStructure<T>(structureSize, buffer, 0);
            }

            /// <summary>
            /// 将数据从二进制流转换为对象。
            /// </summary>
            /// <typeparam name="T">要转换的对象的类型。</typeparam>
            /// <param name="structureSize">要转换的对象的大小。</param>
            /// <param name="buffer">要转换的二进制流。</param>
            /// <param name="startIndex">读取要转换的二进制流的起始位置。</param>
            /// <returns>存储转换结果的对象。</returns>
            [UnityEngine.Scripting.Preserve]
            internal static T BytesToStructure<T>(int structureSize, byte[] buffer, int startIndex)
            {
                if (structureSize < 0)
                {
                    throw new GameFrameworkException("Structure size is invalid.");
                }

                if (buffer == null)
                {
                    throw new GameFrameworkException("Buffer is invalid.");
                }

                if (startIndex < 0)
                {
                    throw new GameFrameworkException("Start index is invalid.");
                }

                if (startIndex + structureSize > buffer.Length)
                {
                    throw new GameFrameworkException("Buffer length is not enough.");
                }

                EnsureCachedHGlobalSize(structureSize);
                System.Runtime.InteropServices.Marshal.Copy(buffer, startIndex, s_CachedHGlobalPtr, structureSize);
                return (T)System.Runtime.InteropServices.Marshal.PtrToStructure(s_CachedHGlobalPtr, typeof(T));
            }
        }
    }
}