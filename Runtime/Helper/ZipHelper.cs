﻿using System;
using System.Buffers;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 压缩帮助类
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class ZipHelper
    {
        private static readonly Crc32 CRC = new Crc32();

        /// <summary> 
        /// 压缩文件夹  
        /// </summary> 
        /// <param name="folderToZip">要压缩的文件夹路径</param> 
        /// <param name="stream">压缩前的Stream,方法执行后变为压缩完成后的文件</param> 
        /// <param name="password">密码</param> 
        /// <returns>是否成功</returns> 
        [UnityEngine.Scripting.Preserve]
        public static bool CompressDirectoryToStream(string folderToZip, Stream stream, string password = null)
        {
            return CompressDirectoryToZipStream(folderToZip, stream, password) != null;
        }


        /// <summary> 
        /// 压缩文件夹  
        /// </summary> 
        /// <param name="folderToZip">要压缩的文件夹路径</param> 
        /// <param name="stream">压缩前的Stream,方法执行后变为压缩完成后的文件</param> 
        /// <param name="password">密码</param> 
        /// <returns>是否压缩成功返回ZipOutputStream，否则返回null</returns> 
        [UnityEngine.Scripting.Preserve]
        public static ZipOutputStream CompressDirectoryToZipStream(string folderToZip, Stream stream, string password = null)
        {
            if (!Directory.Exists(folderToZip))
            {
                return null;
            }

            ZipOutputStream zipStream = new ZipOutputStream(stream);
            zipStream.SetLevel(6);
            if (!string.IsNullOrEmpty(password))
            {
                zipStream.Password = password;
            }

            if (CompressDirectory(folderToZip, zipStream, ""))
            {
                zipStream.Finish();
                return zipStream;
            }

            GC.Collect(1);
            return null;
        }

        /// <summary> 
        /// 递归压缩文件夹的内部方法 
        /// </summary> 
        /// <param name="folderToZip">要压缩的文件夹路径</param> 
        /// <param name="zipStream">压缩输出流</param> 
        /// <param name="parentFolderName">此文件夹的上级文件夹</param> 
        /// <returns>是否成功</returns> 
        [UnityEngine.Scripting.Preserve]
        private static bool CompressDirectory(string folderToZip, ZipOutputStream zipStream, string parentFolderName)
        {
            //这段是创建空文件夹,注释掉可以去掉空文件夹(因为在写入文件的时候也会创建文件夹)
            if (parentFolderName.IsNotNullOrWhiteSpace())
            {
                var ent = new ZipEntry(parentFolderName + "/");
                zipStream.PutNextEntry(ent);
                zipStream.Flush();
            }

            var files = Directory.GetFiles(folderToZip);
            foreach (string file in files)
            {
                byte[] buffer = File.ReadAllBytes(file);
                var path = Path.GetFileName(file);
                if (parentFolderName.IsNotNullOrWhiteSpace())
                {
                    path = parentFolderName + Path.DirectorySeparatorChar + Path.GetFileName(file);
                }

                var ent = new ZipEntry(path)
                {
                    //ent.DateTime = File.GetLastWriteTime(file);//设置文件最后修改时间
                    DateTime = DateTime.Now,
                    Size = buffer.Length,
                };

                CRC.Reset();
                CRC.Update(buffer);

                ent.Crc = CRC.Value;
                zipStream.PutNextEntry(ent);
                zipStream.Write(buffer, 0, buffer.Length);
            }

            var folders = Directory.GetDirectories(folderToZip);
            foreach (var folder in folders)
            {
                var folderName = folder.Substring(folder.LastIndexOf('\\') + 1);

                if (parentFolderName.IsNotNullOrWhiteSpace())
                {
                    folderName = parentFolderName + "\\" + folder.Substring(folder.LastIndexOf('\\') + 1);
                }

                if (!CompressDirectory(folder, zipStream, folderName))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary> 
        /// 压缩文件夹  
        /// </summary> 
        /// <param name="folderToZip">要压缩的文件夹路径</param> 
        /// <param name="zipFile">压缩文件完整路径</param> 
        /// <param name="password">密码</param> 
        /// <returns>是否成功</returns> 
        [UnityEngine.Scripting.Preserve]
        public static bool CompressDirectory(string folderToZip, string zipFile, string password = null)
        {
            if (folderToZip.EndsWithFast(Path.DirectorySeparatorChar.ToString()) || folderToZip.EndsWithFast("/"))
            {
                folderToZip = folderToZip.Substring(0, folderToZip.Length - 1);
            }

            var zipStream = CompressDirectoryToZipStream(folderToZip, new FileStream(zipFile, FileMode.Create, FileAccess.Write, FileShare.Write), password);
            if (zipStream == null)
            {
                return false;
            }

            zipStream.Close();
            return true;
        }

        /// <summary> 
        /// 压缩文件 
        /// </summary> 
        /// <param name="fileToZip">要压缩的文件全名</param> 
        /// <param name="zipFile">压缩后的文件名</param> 
        /// <param name="password">密码</param> 
        /// <returns>是否成功</returns> 
        [UnityEngine.Scripting.Preserve]
        public static bool CompressFile(string fileToZip, string zipFile, string password = null)
        {
            if (!File.Exists(fileToZip))
            {
                return false;
            }

            using (var readStream = File.OpenRead(fileToZip))
            {
                byte[] buffer = new byte[readStream.Length];
                var read = readStream.Read(buffer, 0, buffer.Length);
                using (var writeStream = File.Create(zipFile))
                {
                    var entry = new ZipEntry(Path.GetFileName(fileToZip))
                    {
                        DateTime = DateTime.Now,
                        Size = readStream.Length
                    };
                    CRC.Reset();
                    CRC.Update(buffer);
                    entry.Crc = CRC.Value;
                    using (var zipStream = new ZipOutputStream(writeStream))
                    {
                        if (!string.IsNullOrEmpty(password))
                        {
                            zipStream.Password = password;
                        }

                        zipStream.PutNextEntry(entry);
                        zipStream.SetLevel(Deflater.BEST_COMPRESSION);
                        zipStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }

            GC.Collect(1);

            return true;
        }

        /// <summary> 
        /// 解压功能(解压压缩文件到指定目录) 
        /// </summary> 
        /// <param name="fileToUnZip">待解压的文件</param> 
        /// <param name="zipFolder">指定解压目标目录</param> 
        /// <param name="password">密码</param> 
        /// <returns>是否成功</returns> 
        [UnityEngine.Scripting.Preserve]
        public static bool DecompressFile(string fileToUnZip, string zipFolder, string password = null)
        {
            if (!System.IO.File.Exists(fileToUnZip))
            {
                return false;
            }

            if (!Directory.Exists(zipFolder))
            {
                Directory.CreateDirectory(zipFolder);
            }

            if (!zipFolder.EndsWith("\\"))
            {
                zipFolder += "\\";
            }

            using (var zipStream = new ZipInputStream(System.IO.File.OpenRead(fileToUnZip)))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    zipStream.Password = password;
                }

                ZipEntry zipEntry = null;
                while ((zipEntry = zipStream.GetNextEntry()) != null)
                {
                    if (zipEntry.IsDirectory)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(zipEntry.Name))
                    {
                        continue;
                    }

                    string fileName = zipFolder + zipEntry.Name.Replace('/', '\\');
                    var index = zipEntry.Name.LastIndexOf('/');
                    if (index != -1)
                    {
                        string path = zipFolder + zipEntry.Name.Substring(0, index).Replace('/', '\\');
                        System.IO.Directory.CreateDirectory(path);
                    }

                    var bytes = new byte[zipEntry.Size];
                    var read = zipStream.Read(bytes, 0, bytes.Length);
                    System.IO.File.WriteAllBytes(fileName, bytes);
                }
            }

            GC.Collect(1);
            return true;
        }

        /// <summary>
        /// 用于压缩和解压缩操作的缓冲区大小（以字节为单位）
        /// </summary>
        private const int BufferSize = 8192;

        /// <summary>
        /// 压缩数据。使用Deflate算法将原始字节数组压缩成更小的字节数组。
        /// </summary>
        /// <param name="content">要压缩的原始字节数组。不能为null。</param>
        /// <returns>压缩后的字节数组。如果输入为空数组，则直接返回该空数组。如果压缩过程中发生异常，则返回原始数组。</returns>
        /// <exception cref="ArgumentNullException">当输入参数content为null时抛出。</exception>
        [UnityEngine.Scripting.Preserve]
        public static byte[] Compress(byte[] content)
        {
            content.CheckNull(nameof(content));
            if (content.Length == 0)
            {
                return content;
            }

            var compressor = new Deflater();
            compressor.SetLevel(Deflater.BEST_COMPRESSION);

            compressor.SetInput(content);
            compressor.Finish();

            using (var compressorMemoryStream = new MemoryStream(content.Length))
            {
                var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
                try
                {
                    while (!compressor.IsFinished)
                    {
                        var count = compressor.Deflate(buffer);
                        compressorMemoryStream.Write(buffer, 0, count);
                    }

                    return compressorMemoryStream.ToArray();
                }
                catch (Exception e)
                {
                    Log.Fatal(e);
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                }

                return content;
            }
        }

        /// <summary>
        /// 解压数据。使用Inflate算法将压缩的字节数组还原成原始字节数组。
        /// </summary>
        /// <param name="content">要解压的压缩字节数组。不能为null。</param>
        /// <returns>解压后的原始字节数组。如果输入为空数组，则直接返回该空数组。如果解压过程中发生异常，则返回原始数组。</returns>
        /// <exception cref="ArgumentNullException">当输入参数content为null时抛出。</exception>
        /// <exception cref="InvalidDataException">当压缩数据格式无效或已损坏时抛出。</exception>
        [UnityEngine.Scripting.Preserve]
        public static byte[] Decompress(byte[] content)
        {
            content.CheckNull(nameof(content));
            if (content.Length == 0)
            {
                return content;
            }

            var decompressor = new Inflater();
            decompressor.SetInput(content, 0, content.Length);
            using (var decompressMemoryStream = new MemoryStream(content.Length))
            {
                var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
                try
                {
                    while (!decompressor.IsFinished)
                    {
                        var countLength = decompressor.Inflate(buffer);
                        decompressMemoryStream.Write(buffer, 0, countLength);
                    }

                    return decompressMemoryStream.ToArray();
                }
                catch (Exception e)
                {
                    Log.Fatal(e);
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer, true);
                }

                return content;
            }
        }
    }
}