// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Runtime.Serialization;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架异常类。
    /// </summary>
    [Serializable]
    [UnityEngine.Scripting.Preserve]
    public class GameFrameworkException : Exception
    {
        /// <summary>
        /// 初始化游戏框架异常类的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public GameFrameworkException()
            : base()
        {
        }


        /// <summary>
        /// 使用指定错误消息初始化游戏框架异常类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        [UnityEngine.Scripting.Preserve]
        public GameFrameworkException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化游戏框架异常类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常。如果 innerException 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。</param>
        [UnityEngine.Scripting.Preserve]
        public GameFrameworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 用序列化数据初始化游戏框架异常类的新实例。
        /// </summary>
        /// <param name="info">存有有关所引发异常的序列化的对象数据。</param>
        /// <param name="context">包含有关源或目标的上下文信息。</param>
        [UnityEngine.Scripting.Preserve]
        protected GameFrameworkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}