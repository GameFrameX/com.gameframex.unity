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