// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System.Runtime.InteropServices;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 任务信息。
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    [UnityEngine.Scripting.Preserve]
    public struct TaskInfo
    {
        private readonly bool m_IsValid;
        private readonly int m_SerialId;
        private readonly string m_Tag;
        private readonly int m_Priority;
        private readonly object m_UserData;
        private readonly TaskStatus m_Status;
        private readonly string m_Description;

        /// <summary>
        /// 初始化任务信息的新实例。
        /// </summary>
        /// <param name="serialId">任务的序列编号。</param>
        /// <param name="tag">任务的标签。</param>
        /// <param name="priority">任务的优先级。</param>
        /// <param name="userData">任务的用户自定义数据。</param>
        /// <param name="status">任务状态。</param>
        /// <param name="description">任务描述。</param>
        [UnityEngine.Scripting.Preserve]
        public TaskInfo(int serialId, string tag, int priority, object userData, TaskStatus status, string description)
        {
            m_IsValid = true;
            m_SerialId = serialId;
            m_Tag = tag;
            m_Priority = priority;
            m_UserData = userData;
            m_Status = status;
            m_Description = description;
        }

        /// <summary>
        /// 获取任务信息是否有效。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool IsValid
        {
            get { return m_IsValid; }
        }

        /// <summary>
        /// 获取任务的序列编号。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int SerialId
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_SerialId;
            }
        }

        /// <summary>
        /// 获取任务的标签。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string Tag
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Tag;
            }
        }

        /// <summary>
        /// 获取任务的优先级。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int Priority
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Priority;
            }
        }

        /// <summary>
        /// 获取任务的用户自定义数据。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public object UserData
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_UserData;
            }
        }

        /// <summary>
        /// 获取任务状态。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public TaskStatus Status
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Status;
            }
        }

        /// <summary>
        /// 获取任务描述。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string Description
        {
            get
            {
                if (!m_IsValid)
                {
                    throw new GameFrameworkException("Data is invalid.");
                }

                return m_Description;
            }
        }
    }
}