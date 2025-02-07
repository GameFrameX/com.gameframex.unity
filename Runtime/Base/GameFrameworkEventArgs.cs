//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架中包含事件数据的类的基类。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public abstract class GameFrameworkEventArgs : EventArgs, IReference
    {
        /// <summary>
        /// 初始化游戏框架中包含事件数据的类的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        protected GameFrameworkEventArgs()
        {
        }

        /// <summary>
        /// 清理引用。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public abstract void Clear();
    }
}
