﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 引用强制检查类型。
    /// </summary>
    [Preserve]
    public enum ReferenceStrictCheckType : byte
    {
        /// <summary>
        /// 总是启用。
        /// </summary>
        AlwaysEnable = 0,

        /// <summary>
        /// 仅在开发模式时启用。
        /// </summary>
        OnlyEnableWhenDevelopment,

        /// <summary>
        /// 仅在编辑器中启用。
        /// </summary>
        OnlyEnableInEditor,

        /// <summary>
        /// 总是禁用。
        /// </summary>
        AlwaysDisable,
    }
}
