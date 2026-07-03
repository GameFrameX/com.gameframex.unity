using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 描述组件在自动运行时中的覆盖配置。
    /// </summary>
    [Preserve]
    public sealed class GameFrameXComponentRuntimeConfig
    {
        private readonly Dictionary<string, object> m_Values = new Dictionary<string, object>(StringComparer.Ordinal);
        private readonly List<Type> m_Dependencies = new List<Type>();

        /// <summary>
        /// 初始化组件运行时配置。
        /// </summary>
        /// <param name="componentType">组件类型。</param>
        public GameFrameXComponentRuntimeConfig(Type componentType)
        {
            ComponentType = componentType;
        }

        /// <summary>
        /// 获取组件类型。
        /// </summary>
        public Type ComponentType { get; private set; }

        /// <summary>
        /// 获取或设置是否启用组件。为空时使用自动发现结果。
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// 获取或设置组件挂载顺序。为空时使用 Attribute 或默认顺序。
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// 获取或设置 Manager 实现类型。
        /// </summary>
        public Type ManagerImplementationType { get; set; }

        /// <summary>
        /// 获取或设置 Manager 实现类型名称。
        /// </summary>
        public string ManagerImplementationTypeName { get; set; }

        /// <summary>
        /// 获取依赖组件类型列表。
        /// </summary>
        public IList<Type> Dependencies
        {
            get { return m_Dependencies; }
        }

        /// <summary>
        /// 设置 Inspector 字段或公开属性的运行时覆盖值。
        /// </summary>
        /// <param name="name">字段名或属性名。</param>
        /// <param name="value">覆盖值。</param>
        public void SetValue(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new GameFrameworkException("Runtime config value name is invalid.");
            }

            m_Values[name] = value;
        }

        /// <summary>
        /// 尝试获取运行时覆盖值。
        /// </summary>
        /// <param name="name">字段名或属性名。</param>
        /// <param name="value">覆盖值。</param>
        /// <returns>是否存在覆盖值。</returns>
        public bool TryGetValue(string name, out object value)
        {
            return m_Values.TryGetValue(name, out value);
        }

        /// <summary>
        /// 获取所有运行时覆盖值。
        /// </summary>
        /// <returns>运行时覆盖值字典。</returns>
        public IReadOnlyDictionary<string, object> GetValues()
        {
            return m_Values;
        }
    }
}
