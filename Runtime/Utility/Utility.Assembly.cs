// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 Apache License 2.0 许可证分发，
//  This project is licensed under the Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please see the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository: https://cnb.cool/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 程序集相关的实用函数。
        /// </summary>
        /// <remarks>
        /// Assembly related utility functions.
        /// </remarks>
        public static class Assembly
        {
            // C29：程序集可见性为活取（live fetch），不再使用静态构造器一次性快照；
            // C29: assembly visibility is a live fetch, no longer a one-shot static-constructor snapshot;
            // 迟加载（热更 / 运行期加载）程序集内的类型随加载随可见。
            // types inside late-loaded (hot-update / runtime loaded) assemblies become visible as soon as they load.
            private static readonly Dictionary<string, Type> CachedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);
            private static readonly object Lock = new object();

            /// <summary>
            /// 获取当前已加载的程序集（活取 AppDomain，C29 起权威）。
            /// </summary>
            /// <remarks>
            /// Gets the currently loaded assemblies (live fetch from the AppDomain, authoritative since C29).
            /// </remarks>
            /// <returns>已加载的程序集 / The loaded assemblies</returns>
            [Preserve]
            public static System.Reflection.Assembly[] GetAssemblies()
            {
                return AppDomain.CurrentDomain.GetAssemblies();
            }

            /// <summary>
            /// 获取当前已加载的程序集中的所有类型（活取，C29 起权威）。
            /// </summary>
            /// <remarks>
            /// Gets all types in the currently loaded assemblies (live fetch, authoritative since C29).
            /// </remarks>
            /// <returns>当前已加载的程序集中的所有类型 / All types in the currently loaded assemblies</returns>
            [Preserve]
            public static Type[] GetTypes()
            {
                var results = new List<Type>(256);
                foreach (var assembly in GetAssemblies())
                {
                    results.AddRange(assembly.GetTypes());
                }

                return results.ToArray();
            }

            /// <summary>
            /// 获取当前已加载的程序集中的所有类型（活取，C29 起权威）。
            /// </summary>
            /// <remarks>
            /// Gets all types in the currently loaded assemblies (live fetch, authoritative since C29).
            /// </remarks>
            /// <param name="results">当前已加载的程序集中的所有类型 / All types in the currently loaded assemblies</param>
            [Preserve]
            public static void GetTypes(List<Type> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (var assembly in GetAssemblies())
                {
                    results.AddRange(assembly.GetTypes());
                }
            }

            /// <summary>
            /// 获取当前已加载的程序集中的指定类型（活取，C29 起权威；仅缓存命中，miss 不缓存）。
            /// </summary>
            /// <remarks>
            /// Gets the specified type from the currently loaded assemblies (live fetch, authoritative since C29; hits are cached, misses are not).
            /// </remarks>
            /// <param name="typeName">要获取的类型名 / The type name to get</param>
            /// <returns>当前已加载的程序集中的指定类型 / The specified type from the loaded assemblies</returns>
            [Preserve]
            public static Type GetType(string typeName)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    return null;
                }

                lock (Lock)
                {
                    if (CachedTypes.TryGetValue(typeName, out var type))
                    {
                        return type;
                    }

                    type = Type.GetType(typeName);
                    if (type != null)
                    {
                        CachedTypes.Add(typeName, type);
                        return type;
                    }

                    foreach (System.Reflection.Assembly assembly in GetAssemblies())
                    {
                        type = Type.GetType(Text.Format("{0}, {1}", typeName, assembly.FullName));
                        if (type != null)
                        {
                            CachedTypes.Add(typeName, type);
                            return type;
                        }
                    }

                    return null;
                }
            }

            /// <summary>
            /// 获取当前已加载的程序集中的指定类型的子类列表（活取，C29 起权威）。
            /// </summary>
            /// <remarks>
            /// Gets the list of subclass names of the specified type from the currently loaded assemblies (live fetch, authoritative since C29).
            /// </remarks>
            /// <param name="type">指定类型 / The specified type</param>
            /// <returns>子类全名列表 / The list of subclass full names</returns>
            [Preserve]
            public static List<string> GetRuntimeTypeNames(Type type)
            {
                var types = GetTypes();
                var results = new List<string>(256);
                foreach (var t in types)
                {
                    if (t.IsAbstract || !t.IsClass)
                    {
                        continue;
                    }

                    if (t.IsSubclassOf(type) || t.IsImplWithInterface(type))
                    {
                        results.Add(t.FullName);
                    }
                }

                return results;
            }
        }
    }
}
