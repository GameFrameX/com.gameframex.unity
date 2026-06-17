using System;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 提供热更新 AOT 元数据保留相关配置项的设置提供者接口。
    /// </summary>
    /// <remarks>
    /// Settings provider interface that exposes configuration items related to hot-update AOT metadata preservation.
    /// </remarks>
    public interface IAotPreserveSettingsProvider
    {
        /// <summary>
        /// 获取需要在 link.xml 中保留的类型集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of types to preserve in link.xml.
        /// </remarks>
        /// <value>需要保留的类型数组 / Array of types to preserve</value>
        Type[] PreserveTypes { get; }

        /// <summary>
        /// 获取需要通过包装类引用的类型集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of types referenced via wrapper classes.
        /// </remarks>
        /// <value>需要包装引用的类型数组 / Array of types to reference via wrappers</value>
        Type[] WrapTypes { get; }

        /// <summary>
        /// 获取需要保留的成员名称集合（格式：TypeName.MemberName）。
        /// </summary>
        /// <remarks>
        /// Gets the collection of member names to preserve (format: TypeName.MemberName).
        /// </remarks>
        /// <value>需要保留的成员名称数组 / Array of member names to preserve</value>
        string[] PreserveMembers { get; }

        /// <summary>
        /// 获取需要保留的泛型类型集合。
        /// </summary>
        /// <remarks>
        /// Gets the collection of generic types to preserve.
        /// </remarks>
        /// <value>需要保留的泛型类型数组 / Array of generic types to preserve</value>
        Type[] PreserveGenericTypes { get; }

        /// <summary>
        /// 获取类型白名单（强制保留的类型全名）。
        /// </summary>
        /// <remarks>
        /// Gets the type whitelist (full type names forcibly preserved).
        /// </remarks>
        /// <value>类型白名单全名数组 / Array of whitelisted type full names</value>
        string[] TypeWhiteList { get; }

        /// <summary>
        /// 获取类型黑名单（强制剔除的类型全名）。
        /// </summary>
        /// <remarks>
        /// Gets the type blacklist (full type names forcibly excluded).
        /// </remarks>
        /// <value>类型黑名单全名数组 / Array of blacklisted type full names</value>
        string[] TypeBlackList { get; }

        /// <summary>
        /// 获取成员白名单（强制保留的成员名称）。
        /// </summary>
        /// <remarks>
        /// Gets the member whitelist (member names forcibly preserved).
        /// </remarks>
        /// <value>成员白名单名称数组 / Array of whitelisted member names</value>
        string[] MemberWhiteList { get; }

        /// <summary>
        /// 获取成员黑名单（强制剔除的成员名称）。
        /// </summary>
        /// <remarks>
        /// Gets the member blacklist (member names forcibly excluded).
        /// </remarks>
        /// <value>成员黑名单名称数组 / Array of blacklisted member names</value>
        string[] MemberBlackList { get; }
    }
}
