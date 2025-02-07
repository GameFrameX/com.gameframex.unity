//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

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