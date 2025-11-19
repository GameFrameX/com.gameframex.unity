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