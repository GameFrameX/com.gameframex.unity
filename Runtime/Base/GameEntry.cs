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
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    /// <remarks>
    /// Game entry point.
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public static class GameEntry
    {
        private static readonly GameFrameworkLinkedList<GameFrameworkComponent> GameFrameworkComponents = new GameFrameworkLinkedList<GameFrameworkComponent>();

        /// <summary>
        /// 游戏框架所在的场景编号。
        /// </summary>
        /// <remarks>
        /// The scene ID where the game framework is located.
        /// </remarks>
        public const int GameFrameworkSceneId = 0;

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <remarks>
        /// Gets a game framework component.
        /// </remarks>
        /// <typeparam name="T">要获取的游戏框架组件类型 / Type of the game framework component to get</typeparam>
        /// <returns>要获取的游戏框架组件 / The game framework component to get</returns>
        [UnityEngine.Scripting.Preserve]
        public static T GetComponent<T>() where T : GameFrameworkComponent
        {
            return (T)GetComponent(typeof(T));
        }

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <remarks>
        /// Gets a game framework component.
        /// </remarks>
        /// <param name="type">要获取的游戏框架组件类型 / Type of the game framework component to get</param>
        /// <returns>要获取的游戏框架组件 / The game framework component to get</returns>
        [UnityEngine.Scripting.Preserve]
        public static GameFrameworkComponent GetComponent(Type type)
        {
            LinkedListNode<GameFrameworkComponent> current = GameFrameworkComponents.First;
            while (current != null)
            {
                if (type.IsAssignableFrom(current.Value.GetType()))
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <remarks>
        /// Gets a game framework component.
        /// </remarks>
        /// <param name="typeName">要获取的游戏框架组件类型名称 / Type name of the game framework component to get</param>
        /// <returns>要获取的游戏框架组件 / The game framework component to get</returns>
        [UnityEngine.Scripting.Preserve]
        public static GameFrameworkComponent GetComponent(string typeName)
        {
            LinkedListNode<GameFrameworkComponent> current = GameFrameworkComponents.First;
            while (current != null)
            {
                Type type = current.Value.GetType();
                if (type.FullName == typeName || type.Name == typeName)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// 关闭游戏框架。
        /// </summary>
        /// <remarks>
        /// Shuts down the game framework.
        /// </remarks>
        /// <param name="shutdownType">关闭游戏框架类型 / The shutdown type</param>
        [UnityEngine.Scripting.Preserve]
        public static void Shutdown(ShutdownType shutdownType)
        {
            Log.Info("Shutdown GameFrameX ({0})...", shutdownType);
            BaseComponent baseComponent = GetComponent<BaseComponent>();
            if (baseComponent != null)
            {
                baseComponent.Shutdown();
                baseComponent = null;
            }

            GameFrameworkComponents.Clear();

            if (shutdownType == ShutdownType.None)
            {
                return;
            }

            if (shutdownType == ShutdownType.Restart)
            {
                SceneManager.LoadScene(GameFrameworkSceneId);
                return;
            }

            if (shutdownType == ShutdownType.Quit)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }
        }

        /// <summary>
        /// 重置游戏入口静态状态，兼容关闭 Domain Reload 的 PlayMode。
        /// </summary>
        /// <remarks>
        /// 清空跨会话残留的组件注册表：关闭 Domain Reload 的重复 Play 中，上一次会话的组件已随场景销毁，
        /// 但静态链表仍持有其引用，新会话组件注册会误报 already exist 并自我禁用。由
        /// <c>GameFrameXRuntimeBootstrap</c> 的 SubsystemRegistration 重置调用，与全新 Domain 的初始状态对齐。
        /// <para>
        /// English: Resets the static state of the game entry for PlayMode with Domain Reload disabled. Stale
        /// component registrations from the previous session (destroyed by Unity yet still referenced by the static
        /// list) are cleared so the new session registers cleanly; called from the SubsystemRegistration reset of
        /// <c>GameFrameXRuntimeBootstrap</c> to align with a fresh domain's initial state.
        /// </para>
        /// </remarks>
        [UnityEngine.Scripting.Preserve]
        public static void Reset()
        {
            GameFrameworkComponents.Clear();
        }

        /// <summary>
        /// 注册游戏框架组件。
        /// </summary>
        /// <remarks>
        /// Registers a game framework component.
        /// </remarks>
        /// <param name="gameFrameworkComponent">要注册的游戏框架组件 / The game framework component to register</param>
        [UnityEngine.Scripting.Preserve]
        internal static bool RegisterComponent(GameFrameworkComponent gameFrameworkComponent)
        {
            if (gameFrameworkComponent == null)
            {
                Log.Error("GameFrameX component is invalid.");
                return false;
            }

            Type type = gameFrameworkComponent.GetType();

            // 先清退已销毁的历史会话残留（关闭 Domain Reload 的重复 Play 中组件随场景销毁但静态链表仍持有引用），
            // 重复判定只对存活组件生效——残留死组件不再是新会话注册失败的理由。
            // Prune destroyed stale entries first (with Domain Reload disabled, previous-session components are destroyed
            // by Unity yet remain referenced here); the duplicate check applies to live components only.
            LinkedListNode<GameFrameworkComponent> current = GameFrameworkComponents.First;
            while (current != null)
            {
                LinkedListNode<GameFrameworkComponent> next = current.Next;
                if (current.Value == null)
                {
                    GameFrameworkComponents.Remove(current);
                }
                else if (current.Value.GetType() == type)
                {
                    Log.Error("GameFrameX component type '{0}' is already exist.", type.FullName);
                    return false;
                }

                current = next;
            }

            GameFrameworkComponents.AddLast(gameFrameworkComponent);
            return true;
        }
    }
}
