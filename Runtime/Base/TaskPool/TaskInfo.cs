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

using System.Runtime.InteropServices;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 任务信息。
    /// </summary>
    /// <remarks>
    /// A structure that contains information about a task, including its serial ID, tag, priority, user data, status, and description.
    /// </remarks>
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
        /// <param name="serialId">任务的序列编号。 / The serial ID of the task.</param>
        /// <param name="tag">任务的标签。 / The tag of the task.</param>
        /// <param name="priority">任务的优先级。 / The priority of the task.</param>
        /// <param name="userData">任务的用户自定义数据。 / The user data associated with the task.</param>
        /// <param name="status">任务状态。 / The status of the task.</param>
        /// <param name="description">任务描述。 / The description of the task.</param>
        /// <remarks>
        /// Creates a new TaskInfo instance with the specified parameters.
        /// </remarks>
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
        /// <remarks>
        /// Indicates whether the TaskInfo contains valid data. Returns false if the task was not found.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public bool IsValid
        {
            get { return m_IsValid; }
        }

        /// <summary>
        /// 获取任务的序列编号。
        /// </summary>
        /// <remarks>
        /// The unique serial ID of the task. Throws an exception if the data is invalid.
        /// </remarks>
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
        /// <remarks>
        /// The tag of the task used for grouping and filtering. Throws an exception if the data is invalid.
        /// </remarks>
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
        /// <remarks>
        /// The priority of the task. Higher values indicate higher priority. Throws an exception if the data is invalid.
        /// </remarks>
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
        /// <remarks>
        /// Custom user data associated with the task. Throws an exception if the data is invalid.
        /// </remarks>
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
        /// <remarks>
        /// The current status of the task (Todo, Doing, or Done). Throws an exception if the data is invalid.
        /// </remarks>
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
        /// <remarks>
        /// The description of the task. Throws an exception if the data is invalid.
        /// </remarks>
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