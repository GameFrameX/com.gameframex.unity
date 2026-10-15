using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 管理自动运行时覆盖 Provider。
    /// </summary>
    /// <remarks>
    /// Manages auto runtime override providers.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class GameFrameXRuntimeOverrideProvider
    {
        private static readonly List<IGameFrameXRuntimeOverrideProvider> Providers = new List<IGameFrameXRuntimeOverrideProvider>(32);

        /// <summary>
        /// 注册运行时覆盖 Provider。
        /// </summary>
        /// <remarks>
        /// Registers a runtime override provider.
        /// </remarks>
        /// <param name="provider">运行时覆盖 Provider。 / The runtime override provider.</param>
        [UnityEngine.Scripting.Preserve]
        public static void RegisterProvider(IGameFrameXRuntimeOverrideProvider provider)
        {
            if (provider == null)
            {
                throw new GameFrameworkException("Runtime override provider is invalid.");
            }

            if (!Providers.Contains(provider))
            {
                Providers.Add(provider);
            }
        }

        /// <summary>
        /// 注销运行时覆盖 Provider。
        /// </summary>
        /// <remarks>
        /// Unregisters a runtime override provider.
        /// </remarks>
        /// <param name="provider">运行时覆盖 Provider。 / The runtime override provider.</param>
        [UnityEngine.Scripting.Preserve]
        public static void UnregisterProvider(IGameFrameXRuntimeOverrideProvider provider)
        {
            if (provider == null)
            {
                return;
            }

            Providers.Remove(provider);
        }

        /// <summary>
        /// 清理已注册的运行时覆盖 Provider。
        /// </summary>
        /// <remarks>
        /// Clears the registered runtime override providers.
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void ClearProviders()
        {
            Providers.Clear();
        }

        /// <summary>
        /// 收集所有运行时覆盖：手动注册实例优先，再合并扫描发现的 Provider 实现（免手动注册）。
        /// </summary>
        /// <remarks>
        /// Collects all runtime overrides: manually registered providers first, then providers discovered by scanning
        /// (no manual registration required).
        /// </remarks>
        /// <returns>运行时覆盖上下文。 / The runtime override context.</returns>
        internal static GameFrameXRuntimeOverrideContext CollectOverrides()
        {
            return CollectOverrides(GameFrameXRuntimeScanner.Scan());
        }

        /// <summary>
        /// 收集所有运行时覆盖：手动注册实例优先，再合并扫描结果中发现的 Provider 实现。
        /// </summary>
        /// <param name="scanResult">运行时扫描结果。 / The runtime scan result.</param>
        /// <remarks>
        /// 引用即注册（C28）：实现 <see cref="IGameFrameXRuntimeOverrideProvider"/> 的普通类由扫描静默发现、无参实例化后自动生效；
        /// 手动注册的同类型实例优先，扫描不重复实例化同类型（防止覆盖被收集两次）。缺公共无参构造 → <c>[GameFrameX]</c> Warning 后跳过（响亮失败，不静默丢）。
        /// English: Reference-means-register: plain classes implementing the provider contract are discovered by scanning and applied
        /// automatically; types already registered manually are not instantiated twice, and types without a public parameterless
        /// constructor are skipped with a loud <c>[GameFrameX]</c> warning.
        /// </remarks>
        /// <returns>运行时覆盖上下文。 / The runtime override context.</returns>
        internal static GameFrameXRuntimeOverrideContext CollectOverrides(GameFrameXRuntimeScanResult scanResult)
        {
            var context = new GameFrameXRuntimeOverrideContext();
            foreach (var provider in Providers)
            {
                provider.CollectOverrides(context);
            }

            if (scanResult == null)
            {
                return context;
            }

            foreach (var providerType in scanResult.OverrideProviderTypes)
            {
                if (providerType == null || HasRegisteredInstanceOfType(providerType))
                {
                    continue;
                }

                IGameFrameXRuntimeOverrideProvider discovered;
                try
                {
                    discovered = (IGameFrameXRuntimeOverrideProvider)Activator.CreateInstance(providerType);
                }
                catch (Exception exception)
                {
                    Debug.LogWarning("[GameFrameX] Runtime override provider '" + providerType.FullName + "' 缺少公共无参构造或实例化失败，已跳过扫描发现：" + exception.Message);
                    continue;
                }

                discovered.CollectOverrides(context);
            }

            return context;
        }

        private static bool HasRegisteredInstanceOfType(Type providerType)
        {
            foreach (var provider in Providers)
            {
                if (provider.GetType() == providerType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
