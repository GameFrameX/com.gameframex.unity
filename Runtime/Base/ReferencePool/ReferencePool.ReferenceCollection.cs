// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Collections.Generic;

namespace GameFrameX.Runtime
{
    public static partial class ReferencePool
    {
        /// <summary>
        /// 引用集合
        /// </summary>
        private sealed class ReferenceCollection
        {
            private readonly Queue<IReference> _references;
            private readonly Type _referenceType;
            private int _usingReferenceCount;
            private int _acquireReferenceCount;
            private int _releaseReferenceCount;
            private int _addReferenceCount;
            private int _removeReferenceCount;

            public ReferenceCollection(Type referenceType)
            {
                _references = new Queue<IReference>();
                _referenceType = referenceType;
                _usingReferenceCount = 0;
                _acquireReferenceCount = 0;
                _releaseReferenceCount = 0;
                _addReferenceCount = 0;
                _removeReferenceCount = 0;
            }

            /// <summary>
            /// 引用类型
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public Type ReferenceType
            {
                get { return _referenceType; }
            }

            /// <summary>
            /// 未使用的引用计数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int UnusedReferenceCount
            {
                get { return _references.Count; }
            }

            /// <summary>
            /// 正在使用的引用计数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int UsingReferenceCount
            {
                get { return _usingReferenceCount; }
            }

            /// <summary>
            /// 获取引用的次数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int AcquireReferenceCount
            {
                get { return _acquireReferenceCount; }
            }

            /// <summary>
            /// 归还引用的次数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int ReleaseReferenceCount
            {
                get { return _releaseReferenceCount; }
            }

            /// <summary>
            /// 添加引用的次数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int AddReferenceCount
            {
                get { return _addReferenceCount; }
            }

            /// <summary>
            /// 移除引用的次数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int RemoveReferenceCount
            {
                get { return _removeReferenceCount; }
            }

            /// <summary>
            /// 从引用池获取引用。
            /// </summary>
            /// <typeparam name="T">引用类型。</typeparam>
            /// <returns>引用。</returns>
            [UnityEngine.Scripting.Preserve]
            public T Acquire<T>() where T : class, IReference, new()
            {
                if (typeof(T) != _referenceType)
                {
                    throw new GameFrameworkException("Type is invalid.");
                }

                _usingReferenceCount++;
                _acquireReferenceCount++;
                lock (_references)
                {
                    if (_references.Count > 0)
                    {
                        return (T)_references.Dequeue();
                    }
                }

                _addReferenceCount++;
                return new T();
            }

            /// <summary>
            /// 从引用池获取引用。
            /// </summary>
            /// <returns>引用。</returns>
            [UnityEngine.Scripting.Preserve]
            public IReference Acquire()
            {
                _usingReferenceCount++;
                _acquireReferenceCount++;
                lock (_references)
                {
                    if (_references.Count > 0)
                    {
                        return _references.Dequeue();
                    }
                }

                _addReferenceCount++;
                return (IReference)Activator.CreateInstance(_referenceType);
            }

            /// <summary>
            /// 释放一个引用对象。
            /// </summary>
            /// <param name="reference">要释放的引用对象。</param>
            [UnityEngine.Scripting.Preserve]
            public void Release(IReference reference)
            {
                reference.Clear();
                lock (_references)
                {
                    if (m_EnableStrictCheck && _references.Contains(reference))
                    {
                        GameFrameworkLog.Error("Reference has been released!=>{0}", reference.GetType().FullName);
                        return;
                    }

                    _references.Enqueue(reference);
                }

                _releaseReferenceCount++;
                _usingReferenceCount--;
            }

            /// <summary>
            /// 添加指定类型的引用对象到引用池中。
            /// </summary>
            /// <typeparam name="T">要添加的引用对象类型。</typeparam>
            /// <param name="count">要添加的引用对象数量。</param>
            /// <exception cref="GameFrameworkException">类型无效。</exception>
            [UnityEngine.Scripting.Preserve]
            public void Add<T>(int count) where T : class, IReference, new()
            {
                if (typeof(T) != _referenceType)
                {
                    throw new GameFrameworkException("Type is invalid.");
                }

                lock (_references)
                {
                    _addReferenceCount += count;
                    while (count-- > 0)
                    {
                        _references.Enqueue(new T());
                    }
                }
            }

            /// <summary>
            /// 向引用池中添加指定数量的引用。
            /// </summary>
            /// <param name="count">要添加的引用数量。</param>
            [UnityEngine.Scripting.Preserve]
            public void Add(int count)
            {
                lock (_references)
                {
                    _addReferenceCount += count;
                    while (count-- > 0)
                    {
                        _references.Enqueue((IReference)Activator.CreateInstance(_referenceType));
                    }
                }
            }

            /// <summary>
            /// 从引用池中移除指定数量的引用。
            /// </summary>
            /// <param name="count">要移除的引用数量。</param>
            [UnityEngine.Scripting.Preserve]
            public void Remove(int count)
            {
                lock (_references)
                {
                    if (count > _references.Count)
                    {
                        count = _references.Count;
                    }

                    _removeReferenceCount += count;
                    while (count-- > 0)
                    {
                        _references.Dequeue();
                    }
                }
            }

            /// <summary>
            /// 从引用池中移除所有的引用。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public void RemoveAll()
            {
                lock (_references)
                {
                    _removeReferenceCount += _references.Count;
                    _references.Clear();
                }
            }
        }
    }
}