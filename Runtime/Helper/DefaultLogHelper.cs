// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

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