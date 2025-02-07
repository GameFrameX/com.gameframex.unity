//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 任务状态。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public enum TaskStatus : byte
    {
        /// <summary>
        /// 未开始。
        /// </summary>
        Todo = 0,

        /// <summary>
        /// 执行中。
        /// </summary>
        Doing,

        /// <summary>
        /// 完成。
        /// </summary>
        Done
    }
}
