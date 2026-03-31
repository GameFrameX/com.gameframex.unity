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
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 事件池。
    /// </summary>
    /// <typeparam name="T">事件类型。 / The type of event.</typeparam>
    /// <remarks>
    /// Event pool that manages event subscription, unsubscription, and dispatching.
    /// Supports thread-safe event firing and immediate event processing modes.
    /// </remarks>
    public sealed partial class EventPool<T> where T : BaseEventArgs
    {
        private readonly object _lock = new object();
        private readonly GameFrameworkMultiDictionary<string, EventHandler<T>> _eventHandlers;
        private readonly ConcurrentQueue<EventNode> _events;
        private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> _cachedNodes;
        private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> _tempNodes;
        private readonly EventPoolMode _eventPoolMode;
        private EventHandler<T> _defaultHandler;

        /// <summary>
        /// 初始化事件池的新实例。
        /// </summary>
        /// <param name="mode">事件池模式。 / The event pool mode.</param>
        /// <remarks>
        /// Initializes a new instance of the EventPool class with the specified mode.
        /// </remarks>
        [Preserve]
        public EventPool(EventPoolMode mode)
        {
            _eventHandlers = new GameFrameworkMultiDictionary<string, EventHandler<T>>();
            _events = new ConcurrentQueue<EventNode>();
            _cachedNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
            _tempNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
            _eventPoolMode = mode;
            _defaultHandler = null;
        }

        /// <summary>
        /// 获取事件处理函数的数量。
        /// </summary>
        /// <remarks>
        /// Gets the total count of registered event handlers.
        /// </remarks>
        [Preserve]
        public int EventHandlerCount
        {
            get
            {
                lock (_lock)
                {
                    return _eventHandlers.Count;
                }
            }
        }

        /// <summary>
        /// 获取事件数量。
        /// </summary>
        /// <remarks>
        /// Gets the count of queued events waiting to be processed.
        /// </remarks>
        [Preserve]
        public int EventCount
        {
            get { return _events.Count; }
        }

        /// <summary>
        /// 事件池轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。 / The logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。 / The real elapsed time in seconds.</param>
        /// <remarks>
        /// Polls and processes all queued events. This method should be called every frame.
        /// </remarks>
        [Preserve]
        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            while (_events.TryDequeue(out var eventNodeNode))
            {
                HandleEvent(eventNodeNode.Sender, eventNodeNode.EventArgs);
                ReferencePool.Release(eventNodeNode);
            }
        }

        /// <summary>
        /// 关闭并清理事件池。
        /// </summary>
        /// <remarks>
        /// Shuts down the event pool and releases all resources including event handlers and queued events.
        /// </remarks>
        public void Shutdown()
        {
            Clear();
            lock (_lock)
            {
                _eventHandlers.Clear();
            }

            _cachedNodes.Clear();
            _tempNodes.Clear();
            _defaultHandler = null;
        }

        /// <summary>
        /// 清理事件。
        /// </summary>
        /// <remarks>
        /// Clears all queued events without removing event handlers.
        /// </remarks>
        public void Clear()
        {
            while (_events.TryDequeue(out var node))
            {
                ReferencePool.Release(node);
            }
        }

        /// <summary>
        /// 获取事件处理函数的数量。
        /// </summary>
        /// <param name="id">事件类型编号。 / The event type identifier.</param>
        /// <returns>事件处理函数的数量。 / The count of event handlers for the specified event.</returns>
        /// <remarks>
        /// Gets the number of handlers registered for a specific event type.
        /// </remarks>
        public int Count(string id)
        {
            lock (_lock)
            {
                if (_eventHandlers.TryGetValue(id, out var listRange))
                {
                    return listRange.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// 检查是否存在事件处理函数。
        /// </summary>
        /// <param name="id">事件类型编号。 / The event type identifier.</param>
        /// <param name="handler">要检查的事件处理函数。 / The event handler to check.</param>
        /// <returns>是否存在事件处理函数。 / Whether the event handler exists.</returns>
        /// <remarks>
        /// Checks if a specific handler is registered for the given event type.
        /// </remarks>
        public bool Check(string id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new GameFrameworkException("Event handler is invalid.");
            }

            lock (_lock)
            {
                return _eventHandlers.Contains(id, handler);
            }
        }

        /// <summary>
        /// 订阅事件处理函数。
        /// </summary>
        /// <param name="id">事件类型编号。 / The event type identifier.</param>
        /// <param name="handler">要订阅的事件处理函数。 / The event handler to subscribe.</param>
        /// <remarks>
        /// Subscribes an event handler to the specified event type.
        /// Behavior depends on the EventPoolMode settings for allowing multiple or duplicate handlers.
        /// </remarks>
        public void Subscribe(string id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new GameFrameworkException("Event handler is invalid.");
            }

            lock (_lock)
            {
                if (!_eventHandlers.Contains(id))
                {
                    _eventHandlers.Add(id, handler);
                }
                else if ((_eventPoolMode & EventPoolMode.AllowMultiHandler) != EventPoolMode.AllowMultiHandler)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Event '{0}' not allow multi handler.", id));
                }
                else if ((_eventPoolMode & EventPoolMode.AllowDuplicateHandler) != EventPoolMode.AllowDuplicateHandler && Check(id, handler))
                {
                    throw new GameFrameworkException(Utility.Text.Format("Event '{0}' not allow duplicate handler.", id));
                }
                else
                {
                    _eventHandlers.Add(id, handler);
                }
            }
        }

        /// <summary>
        /// 取消订阅事件处理函数。
        /// </summary>
        /// <param name="id">事件类型编号。 / The event type identifier.</param>
        /// <param name="handler">要取消订阅的事件处理函数。 / The event handler to unsubscribe.</param>
        /// <remarks>
        /// Unsubscribes an event handler from the specified event type.
        /// Handles cache node updates to ensure proper iteration during event processing.
        /// </remarks>
        public void Unsubscribe(string id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                throw new GameFrameworkException("Event handler is invalid.");
            }

            lock (_lock)
            {
                if (_cachedNodes.Count > 0)
                {
                    foreach (var cachedNode in _cachedNodes)
                    {
                        if (cachedNode.Value != null && cachedNode.Value.Value == handler)
                        {
                            _tempNodes.Add(cachedNode.Key, cachedNode.Value.Next);
                        }
                    }

                    if (_tempNodes.Count > 0)
                    {
                        foreach (KeyValuePair<object, LinkedListNode<EventHandler<T>>> cachedNode in _tempNodes)
                        {
                            _cachedNodes[cachedNode.Key] = cachedNode.Value;
                        }

                        _tempNodes.Clear();
                    }
                }

                if (!_eventHandlers.Remove(id, handler))
                {
                    throw new GameFrameworkException(Utility.Text.Format("Event '{0}' not exists specified handler.", id));
                }
            }
        }

        /// <summary>
        /// 设置默认事件处理函数。
        /// </summary>
        /// <param name="handler">要设置的默认事件处理函数。 / The default event handler to set.</param>
        /// <remarks>
        /// Sets a default handler that will be called when an event has no registered handlers.
        /// </remarks>
        public void SetDefaultHandler(EventHandler<T> handler)
        {
            _defaultHandler = handler;
        }

        /// <summary>
        /// 抛出事件，这个操作是线程安全的，即使不在主线程中抛出，也可保证在主线程中回调事件处理函数，但事件会在抛出后的下一帧分发。
        /// </summary>
        /// <param name="sender">事件源。 / The event source.</param>
        /// <param name="e">事件参数。 / The event arguments.</param>
        /// <remarks>
        /// Fires an event in a thread-safe manner. The event will be queued and processed in the next frame's Update call.
        /// This ensures that event handlers are always invoked on the main thread.
        /// </remarks>
        [Preserve]
        public void Fire(object sender, T e)
        {
            if (e == null)
            {
                throw new GameFrameworkException("Event is invalid.");
            }

            var eventNodeNode = EventNode.Create(sender, e);
            _events.Enqueue(eventNodeNode);
        }

        /// <summary>
        /// 抛出事件立即模式，这个操作不是线程安全的，事件会立刻分发。
        /// </summary>
        /// <param name="sender">事件源。 / The event source.</param>
        /// <param name="e">事件参数。 / The event arguments.</param>
        /// <remarks>
        /// Fires an event immediately in synchronous mode. This is not thread-safe and events are dispatched immediately.
        /// Use with caution when not on the main thread.
        /// </remarks>
        public void FireNow(object sender, T e)
        {
            if (e == null)
            {
                throw new GameFrameworkException("Event is invalid.");
            }

            lock (_lock)
            {
                HandleEvent(sender, e);
            }
        }

        /// <summary>
        /// 处理事件结点。
        /// </summary>
        /// <param name="sender">事件源。 / The event source.</param>
        /// <param name="e">事件参数。 / The event arguments.</param>
        /// <remarks>
        /// Internal method that processes and dispatches an event to all registered handlers.
        /// Handles cache nodes to support handler unsubscription during event processing.
        /// </remarks>
        private void HandleEvent(object sender, T e)
        {
            lock (_lock)
            {
                bool noHandlerException = false;
                if (_eventHandlers.TryGetValue(e.Id, out var range))
                {
                    LinkedListNode<EventHandler<T>> current = range.First;
                    while (current != null && current != range.Terminal)
                    {
                        _cachedNodes[e] = current.Next != range.Terminal ? current.Next : null;
                        try
                        {
                            current.Value?.Invoke(sender, e);
                        }
                        catch (Exception exception)
                        {
                            Log.Fatal(exception);
                        }

                        current = _cachedNodes[e];
                    }

                    _cachedNodes.Remove(e);
                }
                else if (_defaultHandler != null)
                {
                    try
                    {
                        _defaultHandler(sender, e);
                    }
                    catch (Exception exception)
                    {
                        Log.Fatal(exception);
                    }
                }
                else if ((_eventPoolMode & EventPoolMode.AllowNoHandler) == 0)
                {
                    noHandlerException = true;
                }

                ReferencePool.Release(e);

                if (noHandlerException)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Event '{0}' not allow no handler.", e.Id));
                }
            }
        }
    }
}