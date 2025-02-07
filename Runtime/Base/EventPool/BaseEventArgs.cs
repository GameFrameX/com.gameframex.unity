//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine.Scripting; // 确保引入命名空间

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 事件基类。
    /// </summary>
    public abstract class BaseEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 获取事件ID。
        /// </summary>
        [Preserve] // 添加 Preserve 标签
        public abstract string Id
        {
            get;
        }
    }
}
