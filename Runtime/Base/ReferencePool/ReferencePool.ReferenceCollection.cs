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