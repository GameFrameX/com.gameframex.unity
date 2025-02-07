//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFrameX;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 引用池组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/ReferencePool")]
    [UnityEngine.Scripting.Preserve]
    public sealed class ReferencePoolComponent : GameFrameworkComponent
    {
        [SerializeField]
        private ReferenceStrictCheckType m_EnableStrictCheck = ReferenceStrictCheckType.AlwaysEnable;

        /// <summary>
        /// 获取或设置是否开启强制检查。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public bool EnableStrictCheck
        {
            get
            {
                return ReferencePool.EnableStrictCheck;
            }
            set
            {
                ReferencePool.EnableStrictCheck = value;
                if (value)
                {
                    Log.Info("Strict checking is enabled for the Reference Pool. It will drastically affect the performance.");
                }
            }
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        protected override void Awake()
        {
            IsAutoRegister = false;
            base.Awake();
        }

        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
            switch (m_EnableStrictCheck)
            {
                case ReferenceStrictCheckType.AlwaysEnable:
                    EnableStrictCheck = true;
                    break;

                case ReferenceStrictCheckType.OnlyEnableWhenDevelopment:
                    EnableStrictCheck = Debug.isDebugBuild;
                    break;

                case ReferenceStrictCheckType.OnlyEnableInEditor:
                    EnableStrictCheck = Application.isEditor;
                    break;

                default:
                    EnableStrictCheck = false;
                    break;
            }
        }
    }
}
