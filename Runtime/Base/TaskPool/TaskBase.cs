// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 任务基类。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public abstract class TaskBase : IReference
    {
        /// <summary>
        /// 任务默认优先级。
        /// </summary>
        public const int DefaultPriority = 0;

        private int m_SerialId;
        private string m_Tag;
        private int m_Priority;
        private object m_UserData;

        private bool m_Done;

        /// <summary>
        /// 初始化任务基类的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public TaskBase()
        {
            m_SerialId = 0;
            m_Tag = null;
            m_Priority = DefaultPriority;
            m_Done = false;
            m_UserData = null;
        }

        /// <summary>
        /// 获取任务的序列编号。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int SerialId
        {
            get { return m_SerialId; }
        }

        /// <summary>
        /// 获取任务的标签。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public string Tag
        {
            get { return m_Tag; }
        }

        /// <summary>
        /// 获取任务的优先级。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int Priority
        {
            get { return m_Priority; }
        }

        /// <summary>
        /// 获取任务的用户自定义数据。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public object UserData
        {
            get { return m_UserData; }
        }

        /// <summary>
        /// 获取或设置任务是否完成。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool Done
        {
            get { return m_Done; }
            set { m_Done = value; }
        }

        /// <summary>
        /// 获取任务描述。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public virtual string Description
        {
            get { return null; }
        }

        /// <summary>
        /// 初始化任务基类。
        /// </summary>
        /// <param name="serialId">任务的序列编号。</param>
        /// <param name="tag">任务的标签。</param>
        /// <param name="priority">任务的优先级。</param>
        /// <param name="userData">任务的用户自定义数据。</param>
        [UnityEngine.Scripting.Preserve]
        public void Initialize(int serialId, string tag, int priority, object userData)
        {
            m_SerialId = serialId;
            m_Tag = tag;
            m_Priority = priority;
            m_UserData = userData;
            m_Done = false;
        }

        /// <summary>
        /// 清理任务基类。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public virtual void Clear()
        {
            m_SerialId = 0;
            m_Tag = null;
            m_Priority = DefaultPriority;
            m_UserData = null;
            m_Done = false;
        }
    }
}