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

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架入口。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class GameFrameworkEntry
    {
        private static readonly GameFrameworkLinkedList<GameFrameworkModule> s_GameFrameworkModules = new GameFrameworkLinkedList<GameFrameworkModule>();

        private static readonly Dictionary<Type, Type> s_ModuleTypeMap = new Dictionary<Type, Type>();

        /// <summary>
        /// 所有游戏框架模块轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var module in s_GameFrameworkModules)
            {
                module.Update(elapseSeconds, realElapseSeconds);
            }
        }

        /// <summary>
        /// 关闭并清理所有游戏框架模块。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static void Shutdown()
        {
            for (var current = s_GameFrameworkModules.Last; current != null; current = current.Previous)
            {
                current.Value.Shutdown();
            }

            s_GameFrameworkModules.Clear();
            ReferencePool.ClearAll();
            Utility.Marshal.FreeCachedHGlobal();
            GameFrameworkLog.SetLogHelper(null);
        }

        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <typeparam name="T">要获取的游戏框架模块类型。</typeparam>
        /// <returns>要获取的游戏框架模块。</returns>
        /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
        [UnityEngine.Scripting.Preserve]
        public static T GetModule<T>() where T : class
        {
            var interfaceType = typeof(T);

            if (!interfaceType.IsInterface)
            {
                throw new GameFrameworkException(Utility.Text.Format("You must get module by interface, but '{0}' is not.", interfaceType.FullName));
            }

            if (!interfaceType.FullName.StartsWith("GameFrameX.", StringComparison.Ordinal))
            {
                throw new GameFrameworkException(Utility.Text.Format("You must get a Game Framework module, but '{0}' is not.", interfaceType.FullName));
            }

            if (!s_ModuleTypeMap.TryGetValue(interfaceType, out Type moduleType))
            {
                string moduleName = Utility.Text.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
                moduleType = s_ModuleTypeMap.TryGetValue(interfaceType, out moduleType) ? moduleType : Type.GetType(moduleName);
                if (moduleType == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not find Game Framework module type '{0}'.", moduleName));
                }
            }

            return GetModule(moduleType) as T;
        }

        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <param name="moduleType">要获取的游戏框架模块类型。</param>
        /// <returns>要获取的游戏框架模块。</returns>
        /// <remarks>如果要获取的游戏框架模块不存在，则自动创建该游戏框架模块。</remarks>
        [UnityEngine.Scripting.Preserve]
        public static GameFrameworkModule GetModule(Type moduleType)
        {
            foreach (var module in s_GameFrameworkModules)
            {
                if (module.GetType() == moduleType)
                {
                    return module;
                }
            }

            return CreateModule(moduleType);
        }

        /// <summary>
        /// 注册框架模块。
        /// </summary>
        /// <param name="interfaceType"> 要注册的游戏框架模块接口类型。</param>
        /// <param name="implType"> 要注册的游戏框架模块类型。</param>
        /// <returns>要创建的游戏框架模块。</returns>
        [UnityEngine.Scripting.Preserve]
        public static void RegisterModule(Type interfaceType, Type implType)
        {
            if (!interfaceType.IsInterface)
            {
                throw new GameFrameworkException(Utility.Text.Format("You must register module by interface, but '{0}' is not.", interfaceType.FullName));
            }

            if (!implType.IsClass || implType.IsInterface || implType.IsAbstract)
            {
                throw new GameFrameworkException(Utility.Text.Format("You must register module by Class and not Interface and Abstract, but '{0}' is not.", implType.FullName));
            }

            if (!s_ModuleTypeMap.TryGetValue(interfaceType, out _))
            {
                s_ModuleTypeMap[interfaceType] = implType;
            }
        }

        /// <summary>
        /// 创建游戏框架模块。
        /// </summary>
        /// <param name="moduleType">要创建的游戏框架模块类型。</param>
        /// <returns>要创建的游戏框架模块。</returns>
        private static GameFrameworkModule CreateModule(Type moduleType)
        {
            var module = (GameFrameworkModule)Activator.CreateInstance(moduleType);
            if (module == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not create module '{0}'.", moduleType.FullName));
            }

            var current = s_GameFrameworkModules.First;
            while (current != null)
            {
                if (module.Priority > current.Value.Priority)
                {
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                s_GameFrameworkModules.AddBefore(current, module);
            }
            else
            {
                s_GameFrameworkModules.AddLast(module);
            }

            return module;
        }
    }
}