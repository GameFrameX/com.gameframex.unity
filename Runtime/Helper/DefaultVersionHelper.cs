﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 默认版本号辅助器。
    /// </summary>
    public class DefaultVersionHelper : Version.IVersionHelper
    {
        /// <summary>
        /// 获取游戏版本号。
        /// </summary>
        public string GameVersion
        {
            get { return Application.version; }
        }
    }
}