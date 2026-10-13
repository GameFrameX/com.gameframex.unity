using UnityEditor;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 游戏框架 Inspector 抽象类。
    /// </summary>
    /// <remarks>
    /// Abstract class for game framework Inspectors.
    /// </remarks>
    public abstract class GameFrameworkInspector : UnityEditor.Editor
    {
        protected const string NoneOptionName = "<None>";
        private bool m_IsCompiling = false;

        /// <summary>
        /// 绘制事件。
        /// </summary>
        /// <remarks>
        /// The draw event.
        /// </remarks>
        public override void OnInspectorGUI()
        {
            if (m_IsCompiling && !EditorApplication.isCompiling)
            {
                m_IsCompiling = false;
                OnCompileComplete();
            }
            else if (!m_IsCompiling && EditorApplication.isCompiling)
            {
                m_IsCompiling = true;
                OnCompileStart();
            }
        }

        /// <summary>
        /// 编译开始事件。
        /// </summary>
        /// <remarks>
        /// The compile start event.
        /// </remarks>
        protected virtual void OnCompileStart()
        {
        }

        /// <summary>
        /// 编译完成事件。
        /// </summary>
        /// <remarks>
        /// The compile complete event.
        /// </remarks>
        protected virtual void OnCompileComplete()
        {
        }

        protected bool IsPrefabInHierarchy(UnityEngine.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

#if UNITY_2018_3_OR_NEWER
            return PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.Regular;
#else
            return PrefabUtility.GetPrefabType(obj) != PrefabType.Prefab;
#endif
        }
    }
}