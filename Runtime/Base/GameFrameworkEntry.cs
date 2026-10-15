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
//  CNB  仓库：https://cnb.cool/GameFrameX
//  CNB Repository:  https://cnb.cool/GameFrameX
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
    /// <remarks>
    /// Game framework entry point.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class GameFrameworkEntry
    {
        private static readonly GameFrameworkLinkedList<IGameFrameworkModule> s_GameFrameworkModules = new GameFrameworkLinkedList<IGameFrameworkModule>();

        private static readonly Dictionary<Type, Type> s_ModuleTypeMap = new Dictionary<Type, Type>();

        /// <summary>
        /// 获取模块会话版本号。每次 <see cref="Shutdown"/> 清空模块实例后自增，用于外部缓存（如 GameApp 静态访问器）
        /// 检测模块实例集已重建并重新解析，保证与 <see cref="GetModule{T}"/> 路径行为一致。
        /// </summary>
        /// <remarks>
        /// Gets the module session version. It increments after every <see cref="Shutdown"/> clears module instances,
        /// so external caches (such as GameApp static accessors) can detect that the module instance set has been rebuilt
        /// and re-resolve, keeping behavior consistent with the <see cref="GetModule{T}"/> path.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static int SessionVersion { get; private set; }

        /// <summary>
        /// 轮询所有可轮询游戏框架模块。
        /// </summary>
        /// <remarks>
        /// Polls all updatable game framework modules. Modules that do not implement <see cref="IUpdatableModule"/> are skipped.
        /// </remarks>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位 / Logical elapsed time in seconds</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位 / Real elapsed time in seconds</param>
        [UnityEngine.Scripting.Preserve]
        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var module in s_GameFrameworkModules)
            {
                if (module is IUpdatableModule updatableModule)
                {
                    updatableModule.Update(elapseSeconds, realElapseSeconds);
                }
            }
        }

        /// <summary>
        /// 通知所有低内存感知游戏框架模块释放可回收资源。
        /// </summary>
        /// <remarks>
        /// 低内存事件的唯一转发入口：<c>Application.lowMemory</c> → <see cref="BaseComponent"/> 钩子 → 本方法。
        /// 仅对实现 <see cref="ILowMemoryAwareModule"/> 的模块分发，未实现者被跳过。
        /// <para>
        /// English: The only entry point forwarding low-memory events: <c>Application.lowMemory</c> → the
        /// <see cref="BaseComponent"/> hook → this method. Dispatched only to modules implementing
        /// <see cref="ILowMemoryAwareModule"/>; others are skipped.
        /// </para>
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void NotifyLowMemory()
        {
            foreach (var module in s_GameFrameworkModules)
            {
                if (module is ILowMemoryAwareModule lowMemoryAwareModule)
                {
                    lowMemoryAwareModule.OnLowMemory();
                }
            }
        }

        /// <summary>
        /// 关闭并清理所有可关闭游戏框架模块。
        /// </summary>
        /// <remarks>
        /// Shuts down and cleans up all shutdownable game framework modules in reverse priority order. Modules that do not implement <see cref="IShutdownableModule"/> are skipped.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void Shutdown()
        {
            for (var current = s_GameFrameworkModules.Last; current != null; current = current.Previous)
            {
                if (current.Value is IShutdownableModule shutdownableModule)
                {
                    shutdownableModule.Shutdown();
                }
            }

            s_GameFrameworkModules.Clear();
            s_ModuleTypeMap.Clear();
            ReferencePool.ClearAll();
            Utility.Marshal.FreeCachedHGlobal();
            GameFrameworkLog.SetLogHelper(null);
            GameFrameXOptions.Reset();

            SessionVersion++;
        }

        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <remarks>
        /// Gets a game framework module. If the module does not exist, it will be created automatically.
        /// </remarks>
        /// <typeparam name="T">要获取的游戏框架模块类型 / Type of the game framework module to get</typeparam>
        /// <returns>要获取的游戏框架模块 / The game framework module to get</returns>
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
                throw new GameFrameworkException(Utility.Text.Format("You must get a GameFrameX module, but '{0}' is not.", interfaceType.FullName));
            }

            if (!s_ModuleTypeMap.TryGetValue(interfaceType, out Type moduleType))
            {
                string moduleName = Utility.Text.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
                moduleType = Type.GetType(moduleName);
                if (moduleType == null)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Can not find GameFrameX module type '{0}'.", moduleName));
                }
            }

            return GetModule(moduleType) as T;
        }

        /// <summary>
        /// 获取游戏框架模块。
        /// </summary>
        /// <remarks>
        /// Gets a game framework module. If the module does not exist, it will be created automatically.
        /// </remarks>
        /// <param name="moduleType">要获取的游戏框架模块类型 / Type of the game framework module to get</param>
        /// <returns>要获取的游戏框架模块 / The game framework module to get</returns>
        [UnityEngine.Scripting.Preserve]
        public static IGameFrameworkModule GetModule(Type moduleType)
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
        /// <remarks>
        /// Registers a framework module.
        /// </remarks>
        /// <param name="interfaceType">要注册的游戏框架模块接口类型 / Interface type of the game framework module to register</param>
        /// <param name="implType">要注册的游戏框架模块类型 / Implementation type of the game framework module to register</param>
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
        /// 启动期全量实例化类型映射中的模块（按注册映射去重，创建时按 <see cref="IPrioritizedModule.Priority"/> 排序）。
        /// </summary>
        /// <remarks>
        /// 安装即启用：唯一调用点为自动注入启动完成点（<see cref="GameFrameXRuntimeHost.EnsureStarted"/> 尾部），
        /// 全部已注册模块即完成创建并消费各自 Options；幂等（已存在实例不重复创建）。
        /// 标注 <see cref="GameFrameXLazyModuleAttribute"/> 的模块跳过（保留 <see cref="GetModule(System.Type)"/> 惰性兜底）。
        /// <para>
        /// English: Enabled upon install: called once at the auto-injection startup completion point (the tail of
        /// <see cref="GameFrameXRuntimeHost.EnsureStarted"/>), all registered modules are created and
        /// consume their Options. Idempotent (existing instances are not created again). Modules marked with
        /// <see cref="GameFrameXLazyModuleAttribute"/> are skipped (the <see cref="GetModule(System.Type)"/> lazy fallback is retained).
        /// </para>
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void InstantiateAllModules()
        {
            var implemented = new HashSet<Type>();
            foreach (var implType in s_ModuleTypeMap.Values)
            {
                if (!implemented.Add(implType))
                {
                    continue;
                }

                if (implType.IsDefined(typeof(GameFrameXLazyModuleAttribute), false))
                {
                    continue;
                }

                GetModule(implType);
            }
        }

        /// <summary>
        /// 创建游戏框架模块。
        /// </summary>
        /// <remarks>
        /// Creates a game framework module.
        /// </remarks>
        /// <param name="moduleType">要创建的游戏框架模块类型 / Type of the game framework module to create</param>
        /// <returns>要创建的游戏框架模块 / The created game framework module</returns>
        private static IGameFrameworkModule CreateModule(Type moduleType)
        {
            var module = (IGameFrameworkModule)Activator.CreateInstance(moduleType);
            if (module == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not create module '{0}'.", moduleType.FullName));
            }

            var priority = GetPriority(module);
            var current = s_GameFrameworkModules.First;
            while (current != null)
            {
                if (priority > GetPriority(current.Value))
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

        /// <summary>
        /// 获取游戏框架模块优先级。
        /// </summary>
        /// <remarks>
        /// Gets the priority of a game framework module. Modules that do not implement <see cref="IPrioritizedModule"/> are treated as 0.
        /// </remarks>
        /// <param name="module">要获取优先级的游戏框架模块 / The game framework module to get the priority of</param>
        /// <returns>游戏框架模块优先级 / The priority of the game framework module</returns>
        private static int GetPriority(IGameFrameworkModule module)
        {
            return module is IPrioritizedModule prioritizedModule ? prioritizedModule.Priority : 0;
        }

    }
}