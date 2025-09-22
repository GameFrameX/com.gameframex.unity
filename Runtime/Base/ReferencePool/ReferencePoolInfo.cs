// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Runtime.InteropServices;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 引用池信息。
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public readonly struct ReferencePoolInfo
    {
        private readonly Type m_Type;
        private readonly int m_UnusedReferenceCount;
        private readonly int m_UsingReferenceCount;
        private readonly int m_AcquireReferenceCount;
        private readonly int m_ReleaseReferenceCount;
        private readonly int m_AddReferenceCount;
        private readonly int m_RemoveReferenceCount;

        /// <summary>
        /// 初始化引用池信息的新实例。
        /// </summary>
        /// <param name="type">引用池类型。</param>
        /// <param name="unusedReferenceCount">未使用引用数量。</param>
        /// <param name="usingReferenceCount">正在使用引用数量。</param>
        /// <param name="acquireReferenceCount">获取引用数量。</param>
        /// <param name="releaseReferenceCount">归还引用数量。</param>
        /// <param name="addReferenceCount">增加引用数量。</param>
        /// <param name="removeReferenceCount">移除引用数量。</param>
        [UnityEngine.Scripting.Preserve]
        public ReferencePoolInfo(Type type, int unusedReferenceCount, int usingReferenceCount, int acquireReferenceCount, int releaseReferenceCount, int addReferenceCount, int removeReferenceCount)
        {
            m_Type = type;
            m_UnusedReferenceCount = unusedReferenceCount;
            m_UsingReferenceCount = usingReferenceCount;
            m_AcquireReferenceCount = acquireReferenceCount;
            m_ReleaseReferenceCount = releaseReferenceCount;
            m_AddReferenceCount = addReferenceCount;
            m_RemoveReferenceCount = removeReferenceCount;
        }

        /// <summary>
        /// 获取引用池类型。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public Type Type
        {
            get { return m_Type; }
        }

        /// <summary>
        /// 获取未使用引用数量。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int UnusedReferenceCount
        {
            get { return m_UnusedReferenceCount; }
        }

        /// <summary>
        /// 获取正在使用引用数量。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int UsingReferenceCount
        {
            get { return m_UsingReferenceCount; }
        }

        /// <summary>
        /// 获取获取引用数量。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int AcquireReferenceCount
        {
            get { return m_AcquireReferenceCount; }
        }

        /// <summary>
        /// 获取归还引用数量。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int ReleaseReferenceCount
        {
            get { return m_ReleaseReferenceCount; }
        }

        /// <summary>
        /// 获取增加引用数量。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int AddReferenceCount
        {
            get { return m_AddReferenceCount; }
        }

        /// <summary>
        /// 获取移除引用数量。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public int RemoveReferenceCount
        {
            get { return m_RemoveReferenceCount; }
        }
    }
}