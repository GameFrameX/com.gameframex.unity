using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 管理自动运行时覆盖 Provider。
    /// </summary>
    [Preserve]
    public static class GameFrameXRuntimeOverrideProvider
    {
        private static readonly List<IGameFrameXRuntimeOverrideProvider> Providers = new List<IGameFrameXRuntimeOverrideProvider>(32);

        /// <summary>
        /// 注册运行时覆盖 Provider。
        /// </summary>
        /// <param name="provider">运行时覆盖 Provider。</param>
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
        /// <param name="provider">运行时覆盖 Provider。</param>
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
        public static void ClearProviders()
        {
            Providers.Clear();
        }

        /// <summary>
        /// 收集所有运行时覆盖。
        /// </summary>
        /// <returns>运行时覆盖上下文。</returns>
        internal static GameFrameXRuntimeOverrideContext CollectOverrides()
        {
            var context = new GameFrameXRuntimeOverrideContext();
            foreach (var provider in Providers)
            {
                provider.CollectOverrides(context);
            }

            return context;
        }
    }
}
