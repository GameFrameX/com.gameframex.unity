// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

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
                get { return m_Object.Name; }
            }

            /// <summary>
            /// 获取对象是否被加锁。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool Locked
            {
                get { return m_Object.Locked; }
                internal set { m_Object.Locked = value; }
            }

            /// <summary>
            /// 获取对象的优先级。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int Priority
            {
                get { return m_Object.Priority; }
                internal set { m_Object.Priority = value; }
            }

            /// <summary>
            /// 获取自定义释放检查标记。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool CustomCanReleaseFlag
            {
                get { return m_Object.CustomCanReleaseFlag; }
            }

            /// <summary>
            /// 获取对象上次使用时间。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public DateTime LastUseTime
            {
                get { return m_Object.LastUseTime; }
            }

            /// <summary>
            /// 获取对象是否正在使用。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool IsInUse
            {
                get { return m_SpawnCount > 0; }
            }

            /// <summary>
            /// 获取对象的获取计数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int SpawnCount
            {
                get { return m_SpawnCount; }
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