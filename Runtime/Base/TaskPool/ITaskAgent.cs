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
    /// 任务代理接口。
    /// </summary>
    /// <typeparam name="T">任务类型。 / The type of the task.</typeparam>
    /// <remarks>
    /// Interface for task agents that process tasks in the task pool. Each agent handles one task at a time.
    /// </remarks>
    public interface ITaskAgent<T> where T : TaskBase
    {
        /// <summary>
        /// 获取任务。
        /// </summary>
        /// <remarks>
        /// The current task being processed by this agent. Returns null if no task is being processed.
        /// </remarks>
        T Task { get; }

        /// <summary>
        /// 初始化任务代理。
        /// </summary>
        /// <remarks>
        /// Called when the agent is added to the task pool. Use this to set up any necessary resources.
        /// </remarks>
        void Initialize();

        /// <summary>
        /// 任务代理轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。 / The logical elapsed time in seconds.</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。 / The real elapsed time in seconds.</param>
        /// <remarks>
        /// Called every frame to update the agent's current task processing.
        /// </remarks>
        void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 关闭并清理任务代理。
        /// </summary>
        /// <remarks>
        /// Called when the agent is removed from the task pool. Use this to release any resources held by the agent.
        /// </remarks>
        void Shutdown();

        /// <summary>
        /// 开始处理任务。
        /// </summary>
        /// <param name="task">要处理的任务。 / The task to process.</param>
        /// <returns>开始处理任务的状态。 / The status indicating the result of starting the task.</returns>
        /// <remarks>
        /// Called when the agent is assigned a new task. Returns a status indicating whether the task can be processed, completed immediately, or needs to wait.
        /// </remarks>
        StartTaskStatus Start(T task);

        /// <summary>
        /// 停止正在处理的任务并重置任务代理。
        /// </summary>
        /// <remarks>
        /// Called when the current task is completed or cancelled. The agent should release the current task and prepare for a new one.
        /// </remarks>
        void Reset();
    }
}
