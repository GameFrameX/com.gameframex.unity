//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using GameFrameX.Runtime;
using UnityEngine.Scripting;

namespace GameFrameX.ObjectPool
{
    /// <summary>
    /// 对象基类。
    /// </summary>
    [Preserve]
    public abstract class ObjectBase : IReference
    {
        private string m_Name;
        private object m_Target;
        private bool m_Locked;
        private int m_Priority;
        private DateTime m_LastUseTime;

        /// <summary>
        /// 初始化对象基类的新实例。
        /// </summary>
        [Preserve]
        public ObjectBase()
        {
            m_Name = null;
            m_Target = null;
            m_Locked = false;
            m_Priority = 0;
            m_LastUseTime = default(DateTime);
        }

        /// <summary>
        /// 获取对象名称。
        /// </summary>
        [Preserve]
        public virtual string Name
        {
            get { return m_Name; }
            protected set { m_Name = value; }
        }

        /// <summary>
        /// 获取对象。
        /// </summary>
        [Preserve]
        public object Target
        {
            get { return m_Target; }
        }

        /// <summary>
        /// 获取或设置对象是否被加锁。
        /// </summary>
        [Preserve]
        public bool Locked
        {
            get { return m_Locked; }
            set { m_Locked = value; }
        }

        /// <summary>
        /// 获取或设置对象的优先级。
        /// </summary>
        [Preserve]
        public int Priority
        {
            get { return m_Priority; }
            set { m_Priority = value; }
        }

        /// <summary>
        /// 获取自定义释放检查标记。
        /// </summary>
        [Preserve]
        public virtual bool CustomCanReleaseFlag
        {
            get { return true; }
        }

        /// <summary>
        /// 获取对象上次使用时间。
        /// </summary>
        [Preserve]
        public DateTime LastUseTime
        {
            get { return m_LastUseTime; }
            internal set { m_LastUseTime = value; }
        }

        /// <summary>
        /// 初始化对象基类。
        /// </summary>
        /// <param name="target">对象。</param>
        [Preserve]
        protected void Initialize(object target)
        {
            Initialize(null, target, false, 0);
        }

        /// <summary>
        /// 初始化对象基类。
        /// </summary>
        /// <param name="name">对象名称。</param>
        /// <param name="target">对象。</param>
        [Preserve]
        protected void Initialize(string name, object target)
        {
            Initialize(name, target, false, 0);
        }

        /// <summary>
        /// 初始化对象基类。
        /// </summary>
        /// <param name="name">对象名称。</param>
        /// <param name="target">对象。</param>
        /// <param name="locked">对象是否被加锁。</param>
        [Preserve]
        protected void Initialize(string name, object target, bool locked)
        {
            Initialize(name, target, locked, 0);
        }

        /// <summary>
        /// 初始化对象基类。
        /// </summary>
        /// <param name="name">对象名称。</param>
        /// <param name="target">对象。</param>
        /// <param name="priority">对象的优先级。</param>
        [Preserve]
        protected void Initialize(string name, object target, int priority)
        {
            Initialize(name, target, false, priority);
        }

        /// <summary>
        /// 初始化对象基类。
        /// </summary>
        /// <param name="name">对象名称。</param>
        /// <param name="target">对象。</param>
        /// <param name="locked">对象是否被加锁。</param>
        /// <param name="priority">对象的优先级。</param>
        [Preserve]
        protected void Initialize(string name, object target, bool locked, int priority)
        {
            if (target == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Target '{0}' is invalid.", name));
            }

            m_Name = name ?? string.Empty;
            m_Target = target;
            m_Locked = locked;
            m_Priority = priority;
            m_LastUseTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 清理对象基类。
        /// </summary>
        [Preserve]
        public virtual void Clear()
        {
            m_Name = null;
            m_Target = null;
            m_Locked = false;
            m_Priority = 0;
            m_LastUseTime = default(DateTime);
        }

        /// <summary>
        /// 获取对象时的事件。
        /// </summary>
        [Preserve]
        protected internal virtual void OnSpawn()
        {
        }

        /// <summary>
        /// 回收对象时的事件。
        /// </summary>
        [Preserve]
        protected internal virtual void OnUnspawn()
        {
        }

        /// <summary>
        /// 释放对象。
        /// </summary>
        /// <param name="isShutdown">是否是关闭对象池时触发。</param>
        [Preserve]
        protected internal abstract void Release(bool isShutdown);
    }
}