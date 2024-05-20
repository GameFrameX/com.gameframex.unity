﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace GameFrameX
{
    /// <summary>
    /// 游戏框架序列化器基类。
    /// </summary>
    /// <typeparam name="T">要序列化的数据类型。</typeparam>
    public abstract class GameFrameworkSerializer<T>
    {
        private readonly Dictionary<byte, SerializeCallback> _serializeCallbacks;
        private readonly Dictionary<byte, DeserializeCallback> _deserializeCallbacks;
        private readonly Dictionary<byte, TryGetValueCallback> _tryGetValueCallbacks;
        private byte _latestSerializeCallbackVersion;

        /// <summary>
        /// 初始化游戏框架序列化器基类的新实例。
        /// </summary>
        public GameFrameworkSerializer()
        {
            _serializeCallbacks = new Dictionary<byte, SerializeCallback>();
            _deserializeCallbacks = new Dictionary<byte, DeserializeCallback>();
            _tryGetValueCallbacks = new Dictionary<byte, TryGetValueCallback>();
            _latestSerializeCallbackVersion = 0;
        }

        /// <summary>
        /// 序列化回调函数。
        /// </summary>
        /// <param name="stream">目标流。</param>
        /// <param name="data">要序列化的数据。</param>
        /// <returns>是否序列化数据成功。</returns>
        public delegate bool SerializeCallback(Stream stream, T data);

        /// <summary>
        /// 反序列化回调函数。
        /// </summary>
        /// <param name="stream">指定流。</param>
        /// <returns>反序列化的数据。</returns>
        public delegate T DeserializeCallback(Stream stream);

        /// <summary>
        /// 尝试从指定流获取指定键的值回调函数。
        /// </summary>
        /// <param name="stream">指定流。</param>
        /// <param name="key">指定键。</param>
        /// <param name="value">指定键的值。</param>
        /// <returns>是否从指定流获取指定键的值成功。</returns>
        public delegate bool TryGetValueCallback(Stream stream, string key, out object value);

        /// <summary>
        /// 注册序列化回调函数。
        /// </summary>
        /// <param name="version">序列化回调函数的版本。</param>
        /// <param name="callback">序列化回调函数。</param>
        public void RegisterSerializeCallback(byte version, SerializeCallback callback)
        {
            if (callback == null)
            {
                throw new GameFrameworkException("Serialize callback is invalid.");
            }

            _serializeCallbacks[version] = callback;
            if (version > _latestSerializeCallbackVersion)
            {
                _latestSerializeCallbackVersion = version;
            }
        }

        /// <summary>
        /// 注册反序列化回调函数。
        /// </summary>
        /// <param name="version">反序列化回调函数的版本。</param>
        /// <param name="callback">反序列化回调函数。</param>
        public void RegisterDeserializeCallback(byte version, DeserializeCallback callback)
        {
            if (callback == null)
            {
                throw new GameFrameworkException("Deserialize callback is invalid.");
            }

            _deserializeCallbacks[version] = callback;
        }

        /// <summary>
        /// 注册尝试从指定流获取指定键的值回调函数。
        /// </summary>
        /// <param name="version">尝试从指定流获取指定键的值回调函数的版本。</param>
        /// <param name="callback">尝试从指定流获取指定键的值回调函数。</param>
        public void RegisterTryGetValueCallback(byte version, TryGetValueCallback callback)
        {
            if (callback == null)
            {
                throw new GameFrameworkException("Try get value callback is invalid.");
            }

            _tryGetValueCallbacks[version] = callback;
        }

        /// <summary>
        /// 序列化数据到目标流中。
        /// </summary>
        /// <param name="stream">目标流。</param>
        /// <param name="data">要序列化的数据。</param>
        /// <returns>是否序列化数据成功。</returns>
        public bool Serialize(Stream stream, T data)
        {
            if (_serializeCallbacks.Count <= 0)
            {
                throw new GameFrameworkException("No serialize callback registered.");
            }

            return Serialize(stream, data, _latestSerializeCallbackVersion);
        }

        /// <summary>
        /// 序列化数据到目标流中。
        /// </summary>
        /// <param name="stream">目标流。</param>
        /// <param name="data">要序列化的数据。</param>
        /// <param name="version">序列化回调函数的版本。</param>
        /// <returns>是否序列化数据成功。</returns>
        public bool Serialize(Stream stream, T data, byte version)
        {
            byte[] header = GetHeader();
            stream.WriteByte(header[0]);
            stream.WriteByte(header[1]);
            stream.WriteByte(header[2]);
            stream.WriteByte(version);
            SerializeCallback callback = null;
            if (!_serializeCallbacks.TryGetValue(version, out callback))
            {
                throw new GameFrameworkException(Utility.Text.Format("Serialize callback '{0}' is not exist.", version));
            }

            return callback(stream, data);
        }

        /// <summary>
        /// 从指定流反序列化数据。
        /// </summary>
        /// <param name="stream">指定流。</param>
        /// <returns>反序列化的数据。</returns>
        public T Deserialize(Stream stream)
        {
            byte[] header = GetHeader();
            byte header0 = (byte)stream.ReadByte();
            byte header1 = (byte)stream.ReadByte();
            byte header2 = (byte)stream.ReadByte();
            if (header0 != header[0] || header1 != header[1] || header2 != header[2])
            {
                throw new GameFrameworkException(Utility.Text.Format("Header is invalid, need '{0}{1}{2}', current '{3}{4}{5}'.", (char)header[0], (char)header[1], (char)header[2], (char)header0, (char)header1, (char)header2));
            }

            byte version = (byte)stream.ReadByte();
            DeserializeCallback callback = null;
            if (!_deserializeCallbacks.TryGetValue(version, out callback))
            {
                throw new GameFrameworkException(Utility.Text.Format("Deserialize callback '{0}' is not exist.", version));
            }

            return callback(stream);
        }

        /// <summary>
        /// 尝试从指定流获取指定键的值。
        /// </summary>
        /// <param name="stream">指定流。</param>
        /// <param name="key">指定键。</param>
        /// <param name="value">指定键的值。</param>
        /// <returns>是否从指定流获取指定键的值成功。</returns>
        public bool TryGetValue(Stream stream, string key, out object value)
        {
            value = null;
            byte[] header = GetHeader();
            byte header0 = (byte)stream.ReadByte();
            byte header1 = (byte)stream.ReadByte();
            byte header2 = (byte)stream.ReadByte();
            if (header0 != header[0] || header1 != header[1] || header2 != header[2])
            {
                return false;
            }

            byte version = (byte)stream.ReadByte();
            if (!_tryGetValueCallbacks.TryGetValue(version, out var callback))
            {
                return false;
            }

            return callback(stream, key, out value);
        }

        /// <summary>
        /// 获取数据头标识。
        /// </summary>
        /// <returns>数据头标识。</returns>
        protected abstract byte[] GetHeader();
    }
}
