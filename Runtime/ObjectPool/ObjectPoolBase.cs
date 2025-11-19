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
using UnityEngine.Scripting;

namespace GameFrameX.ObjectPool
{
    /// <summary>
    /// 对象池基类。
    /// </summary>
    [Preserve]
    public abstract class ObjectPoolBase
    {
        private readonly string m_Name;

        /// <summary>
        /// 初始化对象池基类的新实例。
        /// </summary>
        [Preserve]
        public ObjectPoolBase()
            : this(null)
        {
        }

        /// <summary>
        /// 初始化对象池基类的新实例。
        /// </summary>
        /// <param name="name">对象池名称。</param>
        [Preserve]
        public ObjectPoolBase(string name)
        {
            m_Name = name ?? string.Empty;
        }

        /// <summary>
        /// 获取对象池名称。
        /// </summary>
        [Preserve]
        public string Name
        {
            get { return m_Name; }
        }

        /// <summary>
        /// 获取对象池完整名称。
        /// </summary>
        [Preserve]
        public string FullName
        {
            get { return new TypeNamePair(ObjectType, m_Name).ToString(); }
        }

        /// <summary>
        /// 获取对象池对象类型。
        /// </summary>
        [Preserve]
        public abstract Type ObjectType { get; }

        /// <summary>
        /// 获取对象池中对象的数量。
        /// </summary>
        [Preserve]
        public abstract int Count { get; }

        /// <summary>
        /// 获取对象池中能被释放的对象的数量。
        /// </summary>
        [Preserve]
        public abstract int CanReleaseCount { get; }

        /// <summary>
        /// 获取是否允许对象被多次获取。
        /// </summary>
        [Preserve]
        public abstract bool AllowMultiSpawn { get; }

        /// <summary>
        /// 获取或设置对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        [Preserve]
        public abstract float AutoReleaseInterval { get; set; }

        /// <summary>
        /// 获取或设置对象池的容量。
        /// </summary>
        [Preserve]
        public abstract int Capacity { get; set; }

        /// <summary>
        /// 获取或设置对象池对象过期秒数。
        /// </summary>
        [Preserve]
        public abstract float ExpireTime { get; set; }

        /// <summary>
        /// 获取或设置对象池的优先级。
        /// </summary>
        [Preserve]
        public abstract int Priority { get; set; }

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        [Preserve]
        public abstract void Release();

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <param name="toReleaseCount">尝试释放对象数量。</param>
        [Preserve]
        public abstract void Release(int toReleaseCount);

        /// <summary>
        /// 释放对象池中的所有未使用对象。
        /// </summary>
        [Preserve]
        public abstract void ReleaseAllUnused();

        /// <summary>
        /// 获取所有对象信息。
        /// </summary>
        /// <returns>所有对象信息。</returns>
        [Preserve]
        public abstract ObjectInfo[] GetAllObjectInfos();

        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        internal abstract void Shutdown();
    }
}