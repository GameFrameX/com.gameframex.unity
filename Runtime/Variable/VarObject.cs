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
    /// System.Object 变量类。
    /// </summary>
    [Preserve]
    public sealed class VarObject : Variable<object>
    {
        /// <summary>
        /// 初始化 System.Object 变量类的新实例。
        /// </summary>
        [Preserve]
        public VarObject()
        {
        }
    }
}
