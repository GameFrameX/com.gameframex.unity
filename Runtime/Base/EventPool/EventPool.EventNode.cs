// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using UnityEngine.Scripting; // 确保引入命名空间

namespace GameFrameX.Runtime
{
    public sealed partial class EventPool<T> where T : BaseEventArgs
    {
        /// <summary>
        /// 事件结点。
        /// </summary>
        private sealed class EventNode : IReference
        {
            private object _sender = null;
            private T _eventArgs = null;

            /// <summary>
            /// 发送者
            /// </summary>
            [Preserve] // 添加 Preserve 标签
            public object Sender
            {
                get { return _sender; }
            }

            /// <summary>
            /// 事件参数
            /// </summary>
            [Preserve] // 添加 Preserve 标签
            public T EventArgs
            {
                get { return _eventArgs; }
            }

            /// <summary>
            /// 创建事件节点
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="eventArgs"></param>
            /// <returns></returns>
            [Preserve] // 添加 Preserve 标签
            public static EventNode Create(object sender, T eventArgs)
            {
                EventNode eventNodeNode = ReferencePool.Acquire<EventNode>();
                eventNodeNode._sender = sender;
                eventNodeNode._eventArgs = eventArgs;
                return eventNodeNode;
            }

            public void Clear()
            {
                _sender = null;
                _eventArgs = null;
            }
        }
    }
}