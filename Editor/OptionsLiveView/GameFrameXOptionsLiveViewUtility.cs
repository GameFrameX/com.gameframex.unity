// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents and other related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and other international regulations.
//
//  使用本项目须严格遵守相应法律法规与开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
//
//  本项目采用 Apache License 2.0 许可证分发，
//  This project is licensed under the Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
//
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes or liabilities arising from secondary development based on this project
//  本组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository: https://gitee.com/GameFrameX
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository: https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GameFrameX.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.Editor
{
    /// <summary>
    /// C9 配置终态后保留组件的 Play 期活视图绘制工具。
    /// </summary>
    /// <remarks>
    /// 启动参数已收敛到各模块 Options（代码，经 <see cref="GameFrameXOptions.Configure{TOptions}"/> 注入），
    /// Inspector 编辑态不再提供参数序列化编辑：
    /// <list type="bullet">
    /// <item>编辑态（非 Play）：仅绘制 HelpBox 提示与 Options 类型名（<see cref="DrawLiveViewHeader"/>）。</item>
    /// <item>Play 态：按 Options 成员逐项绘制组件现状——启动期型（标 <see cref="GameFrameXStartupOnlyAttribute"/>）
    /// 只读并附 "(启动期)" 后缀；实时型可编辑，优先写回组件公开属性，否则写回私有字段。</item>
    /// </list>
    /// 成员到组件持有者的映射：Options 成员 <c>Xxx</c> → 私有字段 <c>m_Xxx</c>（忽略大小写）与公开属性 <c>Xxx</c>；
    /// 名称不一致时经 <paramref name="memberFieldAliases"/> 显式指定。引用型/集合型字段不逐项绘制，
    /// 仅配置对象（如 ImageCacheConfig）展开其公开值属性。
    /// </remarks>
    public static class GameFrameXOptionsLiveViewUtility
    {
        /// <summary>
        /// 启动期型只读字段的后缀。
        /// </summary>
        /// <remarks>
        /// The suffix appended to read-only startup-only fields.
        /// </remarks>
        private const string StartupOnlySuffix = " (启动期)";

        /// <summary>
        /// 活视图内单个 Options 成员的解析结果。
        /// </summary>
        /// <remarks>
        /// The resolved result of a single Options member in the live view.
        /// </remarks>
        private sealed class LiveField
        {
            public string MemberName;
            public FieldInfo Field;
            public PropertyInfo Property;
            public bool StartupOnly;
        }

        /// <summary>
        /// 组件类型 → 活视图成员缓存（含反射结果，避免每帧重复反射）。
        /// </summary>
        /// <remarks>
        /// Component type to live view member cache (including reflection results, to avoid repeated reflection every frame).
        /// </remarks>
        private static readonly Dictionary<Type, LiveField[]> s_LiveFieldCache = new Dictionary<Type, LiveField[]>();

        /// <summary>
        /// 配置对象类型 → 可编辑公开属性缓存。
        /// </summary>
        /// <remarks>
        /// Config object type to editable public property cache.
        /// </remarks>
        private static readonly Dictionary<Type, PropertyInfo[]> s_NestedPropertyCache = new Dictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// 绘制编辑态（非 Play）提示：启动参数经 GameFrameXOptions.Configure 配置，并列出模块 Options 类型名。
        /// </summary>
        /// <remarks>
        /// Draws the edit-mode (not playing) hint: startup parameters are configured via GameFrameXOptions.Configure, and lists the module Options type name.
        /// </remarks>
        /// <param name="optionsTypeName">模块 Options 类型名。 / The module Options type name.</param>
        public static void DrawLiveViewHeader(string optionsTypeName)
        {
            EditorGUILayout.HelpBox("启动参数经 GameFrameXOptions.Configure 配置（代码，场景加载前）。\nOptions：" + optionsTypeName, MessageType.Info);
        }

        /// <summary>
        /// 绘制只读字段（启动期型等不可运行期修改的值）。
        /// </summary>
        /// <remarks>
        /// Draws a read-only field (values that cannot be modified at runtime, such as startup-only ones).
        /// </remarks>
        /// <param name="name">字段显示名。 / The display name of the field.</param>
        /// <param name="value">当前值。 / The current value.</param>
        public static void DrawReadOnlyField(string name, object value)
        {
            EditorGUILayout.LabelField(name, FormatValue(value));
        }

        /// <summary>
        /// 绘制实时型可编辑字段：按值类型选择控件，变更时优先写公开属性，其次写私有字段。
        /// </summary>
        /// <remarks>
        /// Draws an editable real-time field: picks a control by value type, and on change writes back to the public property first, then the private field.
        /// </remarks>
        /// <param name="label">字段显示名。 / The display name of the field.</param>
        /// <param name="target">组件实例。 / The component instance.</param>
        /// <param name="field">组件私有字段；可为空（仅属性持有者，如 WebComponent.Timeout）。 / The private field of the component; may be null (property-only holder, e.g. WebComponent.Timeout).</param>
        /// <param name="property">组件公开属性；可为空（仅字段持有者）。优先用于读取与写回。 / The public property of the component; may be null (field-only holder). Preferred for reading and writing back.</param>
        /// <returns>值是否被修改。 / Whether the value was modified.</returns>
        public static bool TryDrawEditableField(string label, object target, FieldInfo field, PropertyInfo property)
        {
            if (target == null || (field == null && property == null))
            {
                return false;
            }

            Type valueType = property != null ? property.PropertyType : field.FieldType;
            object current = ReadValue(target, field, property);
            if (!IsSimpleType(valueType) || current == null)
            {
                DrawReadOnlyField(label, current);
                return false;
            }

            if (!TryDrawValueField(label, current, valueType, out object newValue) || Equals(current, newValue))
            {
                return false;
            }

            WriteValue(target, field, property, newValue);
            return true;
        }

        /// <summary>
        /// 绘制组件活视图主入口：编辑态绘制 HelpBox；Play 态按 Options 成员逐项绘制组件/模块现状。
        /// </summary>
        /// <remarks>
        /// Main entry for drawing the component live view: draws a HelpBox in edit mode; in Play mode draws the component/module state item by item based on Options members.
        /// </remarks>
        /// <param name="component">保留组件实例。 / The retained component instance.</param>
        /// <param name="optionsType">该模块 Options 类型。 / The Options type of the module.</param>
        /// <param name="memberFieldAliases">Options 成员名 → 组件私有字段名的显式别名（名称不一致时使用，如 AgentHelperCount → m_DownloadAgentHelperCount）。 / Explicit aliases from Options member name to component private field name (used when names differ, e.g. AgentHelperCount to m_DownloadAgentHelperCount).</param>
        public static void DrawLiveView(Component component, Type optionsType, Dictionary<string, string> memberFieldAliases = null)
        {
            if (component == null || optionsType == null)
            {
                return;
            }

            if (!EditorApplication.isPlaying)
            {
                DrawLiveViewHeader(optionsType.Name);
                return;
            }

            LiveField[] liveFields = GetLiveFields(component.GetType(), optionsType, memberFieldAliases);
            if (liveFields.Length <= 0)
            {
                return;
            }

            EditorGUILayout.LabelField("运行时参数（活视图）", EditorStyles.boldLabel);
            foreach (LiveField liveField in liveFields)
            {
                DrawLiveField(component, liveField);
            }
        }

        /// <summary>
        /// 绘制单个 Options 成员的活视图行。
        /// </summary>
        /// <remarks>
        /// Draws the live view row for a single Options member.
        /// </remarks>
        private static void DrawLiveField(Component component, LiveField liveField)
        {
            string label = ObjectNames.NicifyVariableName(liveField.MemberName);
            if (liveField.StartupOnly)
            {
                DrawReadOnlyField(label + StartupOnlySuffix, ReadValue(component, liveField.Field, liveField.Property));
                return;
            }

            Type valueType = liveField.Property != null ? liveField.Property.PropertyType : liveField.Field.FieldType;
            if (!IsSimpleType(valueType))
            {
                // 引用型实时成员：配置对象展开其公开值属性；集合/其他引用只读展示概要。
                object value = ReadValue(component, liveField.Field, liveField.Property);
                if (value is IEnumerable && !(value is string))
                {
                    DrawReadOnlyField(label, FormatValue(value));
                    return;
                }

                DrawNestedConfigObject(label, value);
                return;
            }

            if (liveField.Property != null && !liveField.Property.CanWrite && liveField.Field == null)
            {
                DrawReadOnlyField(label + " (只读)", ReadValue(component, null, liveField.Property));
                return;
            }

            TryDrawEditableField(label, component, liveField.Field, liveField.Property);
        }

        /// <summary>
        /// 展开绘制配置对象（如 ImageCacheConfig）的公开值属性，变更经属性写回（即模块 API）。
        /// </summary>
        /// <remarks>
        /// Expands and draws the public value properties of a config object (e.g. ImageCacheConfig); changes are written back through the property (i.e. the module API).
        /// </remarks>
        private static void DrawNestedConfigObject(string label, object value)
        {
            if (value == null)
            {
                DrawReadOnlyField(label, null);
                return;
            }

            foreach (PropertyInfo property in GetNestedProperties(value.GetType()))
            {
                object current = ReadValue(value, null, property);
                if (current == null || !IsSimpleType(property.PropertyType))
                {
                    DrawReadOnlyField(label + " / " + ObjectNames.NicifyVariableName(property.Name), current);
                    continue;
                }

                if (TryDrawValueField(label + " / " + ObjectNames.NicifyVariableName(property.Name), current, property.PropertyType, out object newValue) && !Equals(current, newValue))
                {
                    WriteValue(value, null, property, newValue);
                }
            }
        }

        /// <summary>
        /// 解析并缓存组件的活视图成员：Options 公共实例字段（按声明序）→ 组件私有字段/公开属性。
        /// </summary>
        /// <remarks>
        /// Resolves and caches the live view members of a component: public instance fields of Options (in declaration order) mapped to the component private field/public property.
        /// </remarks>
        private static LiveField[] GetLiveFields(Type componentType, Type optionsType, Dictionary<string, string> memberFieldAliases)
        {
            if (s_LiveFieldCache.TryGetValue(componentType, out LiveField[] cached))
            {
                return cached;
            }

            var liveFields = new List<LiveField>();
            foreach (FieldInfo memberField in optionsType.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                FieldInfo field = ResolveComponentField(componentType, memberField.Name, memberFieldAliases);
                PropertyInfo property = componentType.GetProperty(memberField.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (field == null && property == null)
                {
                    // 组件未持有该成员现状（如 UIOptions.InstancePriority），跳过。
                    continue;
                }

                if (property != null && field != null && property.PropertyType != field.FieldType)
                {
                    // 属性与字段类型不一致（如 ReferencePoolComponent.EnableStrictCheck 为 bool、字段为枚举）：以字段为准。
                    property = null;
                }

                liveFields.Add(new LiveField
                {
                    MemberName = memberField.Name,
                    Field = field,
                    Property = property,
                    StartupOnly = memberField.IsDefined(typeof(GameFrameXStartupOnlyAttribute), false),
                });
            }

            LiveField[] result = liveFields.ToArray();
            s_LiveFieldCache[componentType] = result;
            return result;
        }

        /// <summary>
        /// 解析 Options 成员对应的组件私有字段：显式别名 → m_成员名（精确）→ m_成员名（忽略大小写）→ 成员名（忽略大小写）。
        /// </summary>
        /// <remarks>
        /// Resolves the component private field for an Options member: explicit alias, then m_MemberName (exact), then m_MemberName (case-insensitive), then member name (case-insensitive).
        /// </remarks>
        private static FieldInfo ResolveComponentField(Type componentType, string memberName, Dictionary<string, string> memberFieldAliases)
        {
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            if (memberFieldAliases != null && memberFieldAliases.TryGetValue(memberName, out string alias))
            {
                return componentType.GetField(alias, bindingFlags);
            }

            return componentType.GetField("m_" + memberName, bindingFlags)
                   ?? componentType.GetField("m_" + memberName, bindingFlags | BindingFlags.IgnoreCase)
                   ?? componentType.GetField(memberName, bindingFlags | BindingFlags.IgnoreCase);
        }

        /// <summary>
        /// 读取当前值：可写属性优先（通常直读模块管理器实时值）；只读属性因无写回路径，与字段同时存在时读字段以保证展示与写回一致。
        /// </summary>
        /// <remarks>
        /// Reads the current value: a writable property takes priority (usually reading the module manager's real-time value); a read-only property has no write-back path, so when both exist the field is read to keep display and write-back consistent.
        /// </remarks>
        private static object ReadValue(object target, FieldInfo field, PropertyInfo property)
        {
            if (property != null && property.CanRead && (property.CanWrite || field == null))
            {
                try
                {
                    return property.GetValue(target, null);
                }
                catch (TargetInvocationException)
                {
                    // Play 早期模块未就绪（如管理器为 null）：回退字段现状。
                }
            }

            return field != null ? field.GetValue(target) : null;
        }

        /// <summary>
        /// 写回修改值：优先公开属性 setter（模块公开 API），否则私有字段。
        /// </summary>
        /// <remarks>
        /// Writes back the modified value: the public property setter first (the module public API), otherwise the private field.
        /// </remarks>
        private static void WriteValue(object target, FieldInfo field, PropertyInfo property, object value)
        {
            try
            {
                if (property != null && property.CanWrite)
                {
                    property.SetValue(target, value, null);
                    return;
                }

                if (field != null)
                {
                    field.SetValue(target, value);
                }
            }
            catch (TargetInvocationException exception)
            {
                Debug.LogWarning("[GameFrameX] Live view write-back failed: " + exception.InnerException?.Message);
            }
        }

        /// <summary>
        /// 按值类型绘制编辑控件；不支持的类型返回 false。
        /// </summary>
        /// <remarks>
        /// Draws an edit control based on the value type; returns false for unsupported types.
        /// </remarks>
        private static bool TryDrawValueField(string label, object current, Type valueType, out object newValue)
        {
            newValue = null;
            if (valueType.IsEnum)
            {
                newValue = EditorGUILayout.EnumPopup(label, (Enum)current);
                return true;
            }

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Single:
                    newValue = EditorGUILayout.DelayedFloatField(label, (float)current);
                    return true;
                case TypeCode.Double:
                    newValue = EditorGUILayout.DelayedDoubleField(label, (double)current);
                    return true;
                case TypeCode.Int32:
                    newValue = EditorGUILayout.DelayedIntField(label, (int)current);
                    return true;
                case TypeCode.Int64:
                    newValue = EditorGUILayout.LongField(label, (long)current);
                    return true;
                case TypeCode.Boolean:
                    newValue = EditorGUILayout.Toggle(label, (bool)current);
                    return true;
                case TypeCode.String:
                    newValue = EditorGUILayout.DelayedTextField(label, (string)current);
                    return true;
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                    newValue = Convert.ChangeType(EditorGUILayout.DelayedIntField(label, Convert.ToInt32(current)), valueType);
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取并缓存配置对象的公开值属性（仅值类型/字符串/枚举，按声明序）。
        /// </summary>
        /// <remarks>
        /// Gets and caches the public value properties of a config object (only value types/strings/enums, in declaration order).
        /// </remarks>
        private static PropertyInfo[] GetNestedProperties(Type configType)
        {
            if (s_NestedPropertyCache.TryGetValue(configType, out PropertyInfo[] cached))
            {
                return cached;
            }

            var properties = new List<PropertyInfo>();
            foreach (PropertyInfo property in configType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead && IsSimpleType(property.PropertyType))
                {
                    properties.Add(property);
                }
            }

            PropertyInfo[] result = properties.ToArray();
            s_NestedPropertyCache[configType] = result;
            return result;
        }

        /// <summary>
        /// 是否为可逐项编辑的简单类型（基元/枚举/字符串）。
        /// </summary>
        /// <remarks>
        /// Whether the type is a simple type editable item by item (primitive/enum/string).
        /// </remarks>
        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive || type.IsEnum || type == typeof(string);
        }

        /// <summary>
        /// 格式化只读展示值：工厂委托显示其产物类型名，其余显示 ToString。
        /// </summary>
        /// <remarks>
        /// Formats a read-only display value: factory delegates show their product type name, others show ToString.
        /// </remarks>
        private static string FormatValue(object value)
        {
            if (value == null)
            {
                return "<null>";
            }

            if (value is Delegate delegateValue)
            {
                Type[] genericArguments = delegateValue.GetType().GetGenericArguments();
                return genericArguments.Length > 0 ? genericArguments[genericArguments.Length - 1].Name : delegateValue.ToString();
            }

            if (value is IEnumerable enumerable && !(value is string))
            {
                int count = 0;
                foreach (object _ in enumerable)
                {
                    count++;
                }

                return "<集合, Count=" + count + ">";
            }

            return value.ToString();
        }
    }
}
