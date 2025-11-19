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