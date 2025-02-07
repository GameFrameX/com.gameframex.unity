//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 默认游戏框架日志辅助器。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public class DefaultLogHelper : GameFrameworkLog.ILogHelper
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        [UnityEngine.Scripting.Preserve] // Added Preserve attribute
        public void Log(GameFrameworkLogLevel level, object message)
        {
            var time = $"[Unity]:[{DateTime.Now:HH:mm:ss.fff}]:";

            switch (level)
            {
                case GameFrameworkLogLevel.Debug:
                    Debug.Log($"{time}{message}");
                    break;

                case GameFrameworkLogLevel.Info:
                    Debug.Log($"{time}{message}");
                    break;

                case GameFrameworkLogLevel.Warning:
                    Debug.LogWarning($"{time}{message}");
                    break;

                case GameFrameworkLogLevel.Error:
                    Debug.LogError($"{time}{message}");
                    break;

                case GameFrameworkLogLevel.Fatal:
                    Debug.LogError($"{time}{message}");
                    break;
                default:
                    throw new GameFrameworkException($"{time}{message}");
            }
        }
    }
}