//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace GameFrameX.ObjectPool
{
    /// <summary>
    /// 对象信息。
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    [Preserve] // Preserve the ObjectInfo struct for Unity's serialization
    public struct ObjectInfo
    {
        private readonly string m_Name;
        private readonly bool m_Locked;
        private readonly bool m_CustomCanReleaseFlag;
        private readonly int m_Priority;
        private readonly DateTime m_LastUseTime;
        private readonly int m_SpawnCount;

        /// <summary>
        /// 初始化对象信息的新实例。
        /// </summary>
        /// <param name="name">对象名称。</param>
        /// <param name="locked">对象是否被加锁。</param>
        /// <param name="customCanReleaseFlag">对象自定义释放检查标记。</param>
        /// <param name="priority">对象的优先级。</param>
        /// <param name="lastUseTime">对象上次使用时间。</param>
        /// <param name="spawnCount">对象的获取计数。</param>
        [Preserve] // Preserve the constructor for Unity's serialization
        public ObjectInfo(string name, bool locked, bool customCanReleaseFlag, int priority, DateTime lastUseTime, int spawnCount)
        {
            m_Name = name;
            m_Locked = locked;
            m_CustomCanReleaseFlag = customCanReleaseFlag;
            m_Priority = priority;
            m_LastUseTime = lastUseTime;
            m_SpawnCount = spawnCount;
        }

        /// <summary>
        /// 获取对象名称。
        /// </summary>
        [Preserve] // Preserve the Name property for Unity's serialization
        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// 获取对象是否被加锁。
        /// </summary>
        [Preserve] // Preserve the Locked property for Unity's serialization
        public bool Locked
        {
            get
            {
                return m_Locked;
            }
        }

        /// <summary>
        /// 获取对象自定义释放检查标记。
        /// </summary>
        [Preserve] // Preserve the CustomCanReleaseFlag property for Unity's serialization
        public bool CustomCanReleaseFlag
        {
            get
            {
                return m_CustomCanReleaseFlag;
            }
        }

        /// <summary>
        /// 获取对象的优先级。
        /// </summary>
        [Preserve] // Preserve the Priority property for Unity's serialization
        public int Priority
        {
            get
            {
                return m_Priority;
            }
        }

        /// <summary>
        /// 获取对象上次使用时间。
        /// </summary>
        [Preserve] // Preserve the LastUseTime property for Unity's serialization
        public DateTime LastUseTime
        {
            get
            {
                return m_LastUseTime;
            }
        }

        /// <summary>
        /// 获取对象是否正在使用。
        /// </summary>
        [Preserve] // Preserve the IsInUse property for Unity's serialization
        public bool IsInUse
        {
            get
            {
                return m_SpawnCount > 0;
            }
        }

        /// <summary>
        /// 获取对象的获取计数。
        /// </summary>
        [Preserve] // Preserve the SpawnCount property for Unity's serialization
        public int SpawnCount
        {
            get
            {
                return m_SpawnCount;
            }
        }
    }
}
