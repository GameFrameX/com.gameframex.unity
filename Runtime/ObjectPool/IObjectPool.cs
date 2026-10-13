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

namespace GameFrameX.ObjectPool
{
    /// <summary>
    /// 对象池接口。
    /// </summary>
    /// <remarks>
    /// Interface of an object pool.
    /// </remarks>
    /// <typeparam name="T">对象类型。 / Type of the objects.</typeparam>
    public interface IObjectPool<T> where T : ObjectBase
    {
        /// <summary>
        /// 获取对象池名称。
        /// </summary>
        /// <remarks>
        /// Gets the name of the object pool.
        /// </remarks>
        string Name { get; }

        /// <summary>
        /// 获取对象池完整名称。
        /// </summary>
        /// <remarks>
        /// Gets the full name of the object pool.
        /// </remarks>
        string FullName { get; }

        /// <summary>
        /// 获取对象池对象类型。
        /// </summary>
        /// <remarks>
        /// Gets the type of objects in the pool.
        /// </remarks>
        Type ObjectType { get; }

        /// <summary>
        /// 获取对象池中对象的数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of objects in the object pool.
        /// </remarks>
        int Count { get; }

        /// <summary>
        /// 获取对象池中能被释放的对象的数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of objects in the pool that can be released.
        /// </remarks>
        int CanReleaseCount { get; }

        /// <summary>
        /// 获取是否允许对象被多次获取。
        /// </summary>
        /// <remarks>
        /// Gets whether an object can be spawned multiple times.
        /// </remarks>
        bool AllowMultiSpawn { get; }

        /// <summary>
        /// 获取或设置对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the interval in seconds at which releasable objects are automatically released.
        /// </remarks>
        float AutoReleaseInterval { get; set; }

        /// <summary>
        /// 获取或设置对象池的容量。
        /// </summary>
        /// <remarks>
        /// Gets or sets the capacity of the object pool.
        /// </remarks>
        int Capacity { get; set; }

        /// <summary>
        /// 获取或设置对象池对象过期秒数。
        /// </summary>
        /// <remarks>
        /// Gets or sets the expire time in seconds of objects in the pool.
        /// </remarks>
        float ExpireTime { get; set; }

        /// <summary>
        /// 获取或设置对象池的优先级。
        /// </summary>
        /// <remarks>
        /// Gets or sets the priority of the object pool.
        /// </remarks>
        int Priority { get; set; }

        /// <summary>
        /// 创建对象。
        /// </summary>
        /// <remarks>
        /// Registers an object.
        /// </remarks>
        /// <param name="obj">对象。 / The object.</param>
        /// <param name="spawned">对象是否已被获取。 / Whether the object has been spawned.</param>
        void Register(T obj, bool spawned);

        /// <summary>
        /// 检查对象。
        /// </summary>
        /// <remarks>
        /// Checks whether an object can be spawned.
        /// </remarks>
        /// <returns>要检查的对象是否存在。 / Whether the object to check exists.</returns>
        bool CanSpawn();

        /// <summary>
        /// 检查对象。
        /// </summary>
        /// <remarks>
        /// Checks whether an object can be spawned.
        /// </remarks>
        /// <param name="name">对象名称。 / Object name.</param>
        /// <returns>要检查的对象是否存在。 / Whether the object to check exists.</returns>
        bool CanSpawn(string name);

        /// <summary>
        /// 获取对象。
        /// </summary>
        /// <remarks>
        /// Spawns an object.
        /// </remarks>
        /// <returns>要获取的对象。 / The object to spawn.</returns>
        T Spawn();

        /// <summary>
        /// 获取对象。
        /// </summary>
        /// <remarks>
        /// Spawns an object.
        /// </remarks>
        /// <param name="name">对象名称。 / Object name.</param>
        /// <returns>要获取的对象。 / The object to spawn.</returns>
        T Spawn(string name);

        /// <summary>
        /// 回收对象。
        /// </summary>
        /// <remarks>
        /// Unspawns an object.
        /// </remarks>
        /// <param name="obj">要回收的对象。 / The object to unspawn.</param>
        void Unspawn(T obj);

        /// <summary>
        /// 回收对象。
        /// </summary>
        /// <remarks>
        /// Unspawns an object.
        /// </remarks>
        /// <param name="target">要回收的对象。 / The object to unspawn.</param>
        void Unspawn(object target);

        /// <summary>
        /// 设置对象是否被加锁。
        /// </summary>
        /// <remarks>
        /// Sets whether the object is locked.
        /// </remarks>
        /// <param name="obj">要设置被加锁的对象。 / The object to lock or unlock.</param>
        /// <param name="locked">是否被加锁。 / Whether the object is locked.</param>
        void SetLocked(T obj, bool locked);

        /// <summary>
        /// 设置对象是否被加锁。
        /// </summary>
        /// <remarks>
        /// Sets whether the object is locked.
        /// </remarks>
        /// <param name="target">要设置被加锁的对象。 / The object to lock or unlock.</param>
        /// <param name="locked">是否被加锁。 / Whether the object is locked.</param>
        void SetLocked(object target, bool locked);

        /// <summary>
        /// 设置对象的优先级。
        /// </summary>
        /// <remarks>
        /// Sets the priority of an object.
        /// </remarks>
        /// <param name="obj">要设置优先级的对象。 / The object whose priority to set.</param>
        /// <param name="priority">优先级。 / The priority.</param>
        void SetPriority(T obj, int priority);

        /// <summary>
        /// 设置对象的优先级。
        /// </summary>
        /// <remarks>
        /// Sets the priority of an object.
        /// </remarks>
        /// <param name="target">要设置优先级的对象。 / The object whose priority to set.</param>
        /// <param name="priority">优先级。 / The priority.</param>
        void SetPriority(object target, int priority);

        /// <summary>
        /// 释放对象。
        /// </summary>
        /// <remarks>
        /// Releases an object.
        /// </remarks>
        /// <param name="obj">要释放的对象。 / The object to release.</param>
        /// <returns>释放对象是否成功。 / Whether the object was released successfully.</returns>
        bool ReleaseObject(T obj);

        /// <summary>
        /// 释放对象。
        /// </summary>
        /// <remarks>
        /// Releases an object.
        /// </remarks>
        /// <param name="target">要释放的对象。 / The object to release.</param>
        /// <returns>释放对象是否成功。 / Whether the object was released successfully.</returns>
        bool ReleaseObject(object target);

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <remarks>
        /// Releases the releasable objects in the object pool.
        /// </remarks>
        void Release();

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <remarks>
        /// Releases the releasable objects in the object pool.
        /// </remarks>
        /// <param name="toReleaseCount">尝试释放对象数量。 / Number of objects to try to release.</param>
        void Release(int toReleaseCount);

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <remarks>
        /// Releases the releasable objects in the object pool.
        /// </remarks>
        /// <param name="releaseObjectFilterCallback">释放对象筛选函数。 / Filter callback used to select objects to release.</param>
        void Release(ReleaseObjectFilterCallback<T> releaseObjectFilterCallback);

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <remarks>
        /// Releases the releasable objects in the object pool.
        /// </remarks>
        /// <param name="toReleaseCount">尝试释放对象数量。 / Number of objects to try to release.</param>
        /// <param name="releaseObjectFilterCallback">释放对象筛选函数。 / Filter callback used to select objects to release.</param>
        void Release(int toReleaseCount, ReleaseObjectFilterCallback<T> releaseObjectFilterCallback);

        /// <summary>
        /// 释放对象池中的所有未使用对象。
        /// </summary>
        /// <remarks>
        /// Releases all unused objects in the object pool.
        /// </remarks>
        void ReleaseAllUnused();
    }
}