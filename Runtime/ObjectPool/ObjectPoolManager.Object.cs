//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using GameFrameX.Runtime;

namespace GameFrameX.ObjectPool
{
    public sealed partial class ObjectPoolManager
    {
        /// <summary>
        /// 内部对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        private sealed class Object<T> : IReference where T : ObjectBase
        {
            private T m_Object;
            private int m_SpawnCount;

            /// <summary>
            /// 初始化内部对象的新实例。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public Object()
            {
                m_Object = null;
                m_SpawnCount = 0;
            }

            /// <summary>
            /// 获取对象名称。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public string Name
            {
                get
                {
                    return m_Object.Name;
                }
            }

            /// <summary>
            /// 获取对象是否被加锁。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool Locked
            {
                get
                {
                    return m_Object.Locked;
                }
                internal set
                {
                    m_Object.Locked = value;
                }
            }

            /// <summary>
            /// 获取对象的优先级。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int Priority
            {
                get
                {
                    return m_Object.Priority;
                }
                internal set
                {
                    m_Object.Priority = value;
                }
            }

            /// <summary>
            /// 获取自定义释放检查标记。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool CustomCanReleaseFlag
            {
                get
                {
                    return m_Object.CustomCanReleaseFlag;
                }
            }

            /// <summary>
            /// 获取对象上次使用时间。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public DateTime LastUseTime
            {
                get
                {
                    return m_Object.LastUseTime;
                }
            }

            /// <summary>
            /// 获取对象是否正在使用。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool IsInUse
            {
                get
                {
                    return m_SpawnCount > 0;
                }
            }

            /// <summary>
            /// 获取对象的获取计数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int SpawnCount
            {
                get
                {
                    return m_SpawnCount;
                }
            }

            /// <summary>
            /// 创建内部对象。
            /// </summary>
            /// <param name="obj">对象。</param>
            /// <param name="spawned">对象是否已被获取。</param>
            /// <returns>创建的内部对象。</returns>
            [UnityEngine.Scripting.Preserve]
            public static Object<T> Create(T obj, bool spawned)
            {
                if (obj == null)
                {
                    throw new GameFrameworkException("Object is invalid.");
                }

                Object<T> internalObject = ReferencePool.Acquire<Object<T>>();
                internalObject.m_Object = obj;
                internalObject.m_SpawnCount = spawned ? 1 : 0;
                if (spawned)
                {
                    obj.OnSpawn();
                }

                return internalObject;
            }

            /// <summary>
            /// 清理内部对象。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public void Clear()
            {
                m_Object = null;
                m_SpawnCount = 0;
            }

            /// <summary>
            /// 查看对象。
            /// </summary>
            /// <returns>对象。</returns>
            [UnityEngine.Scripting.Preserve]
            public T Peek()
            {
                return m_Object;
            }

            /// <summary>
            /// 获取对象。
            /// </summary>
            /// <returns>对象。</returns>
            [UnityEngine.Scripting.Preserve]
            public T Spawn()
            {
                m_SpawnCount++;
                m_Object.LastUseTime = DateTime.UtcNow;
                m_Object.OnSpawn();
                return m_Object;
            }

            /// <summary>
            /// 回收对象。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public void Unspawn()
            {
                m_Object.OnUnspawn();
                m_Object.LastUseTime = DateTime.UtcNow;
                m_SpawnCount--;
                if (m_SpawnCount < 0)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Object '{0}' spawn count is less than 0.", Name));
                }
            }

            /// <summary>
            /// 释放对象。
            /// </summary>
            /// <param name="isShutdown">是否是关闭对象池时触发。</param>
            [UnityEngine.Scripting.Preserve]
            public void Release(bool isShutdown)
            {
                m_Object.Release(isShutdown);
                ReferencePool.Release(m_Object);
            }
        }
    }
}
