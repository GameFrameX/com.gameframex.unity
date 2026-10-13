using System.Collections.Generic;
using GameFrameX.Runtime;
using UnityEditor;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 组件类型 Inspector 抽象类，提供组件类型的下拉选择绘制。
    /// </summary>
    /// <remarks>
    /// Abstract Inspector for component types, providing a dropdown for selecting the component type.
    /// </remarks>
    public abstract class ComponentTypeComponentInspector : GameFrameworkInspector
    {
        protected SerializedProperty ComponentType = null;
        protected string[] ComponentTypeNames = null;
        protected int ComponentTypeNameIndex = 0;

        /// <summary>
        /// 绘制事件。
        /// </summary>
        /// <remarks>
        /// The draw event.
        /// </remarks>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                // 子类可通过不调用 RefreshComponentTypeNames 声明无类型列表（如 C9 后配置收敛到 Options 的组件），
                // 此时跳过下拉绘制，避免 null 数组传入 EditorGUILayout.Popup 导致 NRE。
                if (ComponentTypeNames != null)
                {
                    int componentTypeSelectedIndex = EditorGUILayout.Popup("ComponentType", ComponentTypeNameIndex, ComponentTypeNames);
                    if (componentTypeSelectedIndex != ComponentTypeNameIndex)
                    {
                        ComponentTypeNameIndex = componentTypeSelectedIndex;
                        ComponentType.stringValue = componentTypeSelectedIndex <= 0 ? null : ComponentTypeNames[componentTypeSelectedIndex];
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();

            Repaint();
        }

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }

        protected virtual void Enable()
        {
        }

        private void OnEnable()
        {
            ComponentType = serializedObject.FindProperty("componentType");
            Enable();
            RefreshTypeNames();
        }

        protected abstract void RefreshTypeNames();

        protected void RefreshComponentTypeNames(System.Type type)
        {
            List<string> managerTypeNames = new List<string>
            {
                NoneOptionName
            };

            managerTypeNames.AddRange(Utility.Assembly.GetRuntimeTypeNames(type));
            ComponentTypeNames = managerTypeNames.ToArray();
            ComponentTypeNameIndex = 0;
            if (!ComponentType.stringValue.IsNullOrEmpty())
            {
                ComponentTypeNameIndex = managerTypeNames.IndexOf(ComponentType.stringValue);
                if (ComponentTypeNameIndex <= 0)
                {
                    ComponentTypeNameIndex = 0;
                    ComponentType.stringValue = null;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}