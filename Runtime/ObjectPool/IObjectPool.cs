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

namespace GameFrameX.ObjectPool
{
    /// <summary>
    /// 对象池接口。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    public interface IObjectPool<T> where T : ObjectBase
    {
        /// <summary>
        /// 获取对象池名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取对象池完整名称。
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// 获取对象池对象类型。
        /// </summary>
        Type ObjectType { get; }

        /// <summary>
        /// 获取对象池中对象的数量。
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取对象池中能被释放的对象的数量。
        /// </summary>
        int CanReleaseCount { get; }

        /// <summary>
        /// 获取是否允许对象被多次获取。
        /// </summary>
        bool AllowMultiSpawn { get; }

        /// <summary>
        /// 获取或设置对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        float AutoReleaseInterval { get; set; }

        /// <summary>
        /// 获取或设置对象池的容量。
        /// </summary>
        int Capacity { get; set; }

        /// <summary>
        /// 获取或设置对象池对象过期秒数。
        /// </summary>
        float ExpireTime { get; set; }

        /// <summary>
        /// 获取或设置对象池的优先级。
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// 创建对象。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <param name="spawned">对象是否已被获取。</param>
        void Register(T obj, bool spawned);

        /// <summary>
        /// 检查对象。
        /// </summary>
        /// <returns>要检查的对象是否存在。</returns>
        bool CanSpawn();

        /// <summary>
        /// 检查对象。
        /// </summary>
        /// <param name="name">对象名称。</param>
        /// <returns>要检查的对象是否存在。</returns>
        bool CanSpawn(string name);

        /// <summary>
        /// 获取对象。
        /// </summary>
        /// <returns>要获取的对象。</returns>
        T Spawn();

        /// <summary>
        /// 获取对象。
        /// </summary>
        /// <param name="name">对象名称。</param>
        /// <returns>要获取的对象。</returns>
        T Spawn(string name);

        /// <summary>
        /// 回收对象。
        /// </summary>
        /// <param name="obj">要回收的对象。</param>
        void Unspawn(T obj);

        /// <summary>
        /// 回收对象。
        /// </summary>
        /// <param name="target">要回收的对象。</param>
        void Unspawn(object target);

        /// <summary>
        /// 设置对象是否被加锁。
        /// </summary>
        /// <param name="obj">要设置被加锁的对象。</param>
        /// <param name="locked">是否被加锁。</param>
        void SetLocked(T obj, bool locked);

        /// <summary>
        /// 设置对象是否被加锁。
        /// </summary>
        /// <param name="target">要设置被加锁的对象。</param>
        /// <param name="locked">是否被加锁。</param>
        void SetLocked(object target, bool locked);

        /// <summary>
        /// 设置对象的优先级。
        /// </summary>
        /// <param name="obj">要设置优先级的对象。</param>
        /// <param name="priority">优先级。</param>
        void SetPriority(T obj, int priority);

        /// <summary>
        /// 设置对象的优先级。
        /// </summary>
        /// <param name="target">要设置优先级的对象。</param>
        /// <param name="priority">优先级。</param>
        void SetPriority(object target, int priority);

        /// <summary>
        /// 释放对象。
        /// </summary>
        /// <param name="obj">要释放的对象。</param>
        /// <returns>释放对象是否成功。</returns>
        bool ReleaseObject(T obj);

        /// <summary>
        /// 释放对象。
        /// </summary>
        /// <param name="target">要释放的对象。</param>
        /// <returns>释放对象是否成功。</returns>
        bool ReleaseObject(object target);

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        void Release();

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <param name="toReleaseCount">尝试释放对象数量。</param>
        void Release(int toReleaseCount);

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <param name="releaseObjectFilterCallback">释放对象筛选函数。</param>
        void Release(ReleaseObjectFilterCallback<T> releaseObjectFilterCallback);

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <param name="toReleaseCount">尝试释放对象数量。</param>
        /// <param name="releaseObjectFilterCallback">释放对象筛选函数。</param>
        void Release(int toReleaseCount, ReleaseObjectFilterCallback<T> releaseObjectFilterCallback);

        /// <summary>
        /// 释放对象池中的所有未使用对象。
        /// </summary>
        void ReleaseAllUnused();
    }
}