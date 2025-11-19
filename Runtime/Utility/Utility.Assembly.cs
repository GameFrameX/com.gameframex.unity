// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
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
        public static class Assembly
        {
            private static readonly System.Reflection.Assembly[] s_Assemblies = null;
            private static readonly Dictionary<string, Type> s_CachedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);

            static Assembly()
            {
                s_Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            /// <summary>
            /// 获取已加载的程序集。
            /// </summary>
            /// <returns>已加载的程序集。</returns>
            [Preserve]
            public static System.Reflection.Assembly[] GetAssemblies()
            {
                return s_Assemblies;
            }

            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <returns>已加载的程序集中的所有类型。</returns>
            [Preserve]
            public static Type[] GetTypes()
            {
                List<Type> results = new List<Type>();
                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    results.AddRange(assembly.GetTypes());
                }

                return results.ToArray();
            }

            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <param name="results">已加载的程序集中的所有类型。</param>
            [Preserve]
            public static void GetTypes(List<Type> results)
            {
                if (results == null)
                {
                    throw new GameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    results.AddRange(assembly.GetTypes());
                }
            }

            /// <summary>
            /// 获取已加载的程序集中的指定类型。
            /// </summary>
            /// <param name="typeName">要获取的类型名。</param>
            /// <returns>已加载的程序集中的指定类型。</returns>
            [Preserve]
            public static Type GetType(string typeName)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new GameFrameworkException("Type name is invalid.");
                }

                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }

                type = Type.GetType(typeName);
                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);
                    return type;
                }

                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    type = Type.GetType(Text.Format("{0}, {1}", typeName, assembly.FullName));
                    if (type != null)
                    {
                        s_CachedTypes.Add(typeName, type);
                        return type;
                    }
                }

                return null;
            }

            /// <summary>
            /// 获取已加载的程序集中的指定类型的子类列表。
            /// </summary>
            /// <param name="type">指定类型</param>
            /// <returns></returns>
            [Preserve]
            public static List<string> GetRuntimeTypeNames(Type type)
            {
                var types = GetTypes();
                List<string> results = new List<string>();
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