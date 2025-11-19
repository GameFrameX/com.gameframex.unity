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
            get { return m_Name; }
        }

        /// <summary>
        /// 获取对象是否被加锁。
        /// </summary>
        [Preserve] // Preserve the Locked property for Unity's serialization
        public bool Locked
        {
            get { return m_Locked; }
        }

        /// <summary>
        /// 获取对象自定义释放检查标记。
        /// </summary>
        [Preserve] // Preserve the CustomCanReleaseFlag property for Unity's serialization
        public bool CustomCanReleaseFlag
        {
            get { return m_CustomCanReleaseFlag; }
        }

        /// <summary>
        /// 获取对象的优先级。
        /// </summary>
        [Preserve] // Preserve the Priority property for Unity's serialization
        public int Priority
        {
            get { return m_Priority; }
        }

        /// <summary>
        /// 获取对象上次使用时间。
        /// </summary>
        [Preserve] // Preserve the LastUseTime property for Unity's serialization
        public DateTime LastUseTime
        {
            get { return m_LastUseTime; }
        }

        /// <summary>
        /// 获取对象是否正在使用。
        /// </summary>
        [Preserve] // Preserve the IsInUse property for Unity's serialization
        public bool IsInUse
        {
            get { return m_SpawnCount > 0; }
        }

        /// <summary>
        /// 获取对象的获取计数。
        /// </summary>
        [Preserve] // Preserve the SpawnCount property for Unity's serialization
        public int SpawnCount
        {
            get { return m_SpawnCount; }
        }
    }
}