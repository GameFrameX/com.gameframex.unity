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
using GameFrameX.Runtime;

namespace GameFrameX.ObjectPool
{
    public sealed partial class ObjectPoolManager
    {
        /// <summary>
        /// 内部对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        private sealed class Object<T> : IReference where T : ObjectBase
        {
            private T m_Object;
            private int m_SpawnCount;

            /// <summary>
            /// 初始化内部对象的新实例。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public Object()
            {
                m_Object = null;
                m_SpawnCount = 0;
            }

            /// <summary>
            /// 获取对象名称。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public string Name
            {
                get { return m_Object.Name; }
            }

            /// <summary>
            /// 获取对象是否被加锁。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool Locked
            {
                get { return m_Object.Locked; }
                internal set { m_Object.Locked = value; }
            }

            /// <summary>
            /// 获取对象的优先级。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int Priority
            {
                get { return m_Object.Priority; }
                internal set { m_Object.Priority = value; }
            }

            /// <summary>
            /// 获取自定义释放检查标记。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool CustomCanReleaseFlag
            {
                get { return m_Object.CustomCanReleaseFlag; }
            }

            /// <summary>
            /// 获取对象上次使用时间。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public DateTime LastUseTime
            {
                get { return m_Object.LastUseTime; }
            }

            /// <summary>
            /// 获取对象是否正在使用。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public bool IsInUse
            {
                get { return m_SpawnCount > 0; }
            }

            /// <summary>
            /// 获取对象的获取计数。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public int SpawnCount
            {
                get { return m_SpawnCount; }
            }

            /// <summary>
            /// 创建内部对象。
            /// </summary>
            /// <param name="obj">对象。</param>
            /// <param name="spawned">对象是否已被获取。</param>
            /// <returns>创建的内部对象。</returns>
            [UnityEngine.Scripting.Preserve]
            public static Object<T> Create(T obj, bool spawned)
            {
                if (obj == null)
                {
                    throw new GameFrameworkException("Object is invalid.");
                }

                Object<T> internalObject = ReferencePool.Acquire<Object<T>>();
                internalObject.m_Object = obj;
                internalObject.m_SpawnCount = spawned ? 1 : 0;
                if (spawned)
                {
                    obj.OnSpawn();
                }

                return internalObject;
            }

            /// <summary>
            /// 清理内部对象。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public void Clear()
            {
                m_Object = null;
                m_SpawnCount = 0;
            }

            /// <summary>
            /// 查看对象。
            /// </summary>
            /// <returns>对象。</returns>
            [UnityEngine.Scripting.Preserve]
            public T Peek()
            {
                return m_Object;
            }

            /// <summary>
            /// 获取对象。
            /// </summary>
            /// <returns>对象。</returns>
            [UnityEngine.Scripting.Preserve]
            public T Spawn()
            {
                m_SpawnCount++;
                m_Object.LastUseTime = DateTime.UtcNow;
                m_Object.OnSpawn();
                return m_Object;
            }

            /// <summary>
            /// 回收对象。
            /// </summary>
            [UnityEngine.Scripting.Preserve]
            public void Unspawn()
            {
                m_Object.OnUnspawn();
                m_Object.LastUseTime = DateTime.UtcNow;
                m_SpawnCount--;
                if (m_SpawnCount < 0)
                {
                    throw new GameFrameworkException(Utility.Text.Format("Object '{0}' spawn count is less than 0.", Name));
                }
            }

            /// <summary>
            /// 释放对象。
            /// </summary>
            /// <param name="isShutdown">是否是关闭对象池时触发。</param>
            [UnityEngine.Scripting.Preserve]
            public void Release(bool isShutdown)
            {
                m_Object.Release(isShutdown);
                ReferencePool.Release(m_Object);
            }
        }
    }
}