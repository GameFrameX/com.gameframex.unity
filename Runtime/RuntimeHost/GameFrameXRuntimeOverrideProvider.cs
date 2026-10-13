using System.Collections.Generic;
using UnityEngine.Scripting;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 管理自动运行时覆盖 Provider。
    /// </summary>
    /// <remarks>
    /// Manages auto runtime override providers.
    /// </remarks>
    [Preserve]
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
        public static void ClearProviders()
        {
            Providers.Clear();
        }

        /// <summary>
        /// 收集所有运行时覆盖。
        /// </summary>
        /// <remarks>
        /// Collects all runtime overrides.
        /// </remarks>
        /// <returns>运行时覆盖上下文。 / The runtime override context.</returns>
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
