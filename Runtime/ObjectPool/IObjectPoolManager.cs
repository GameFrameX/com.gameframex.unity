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

namespace GameFrameX.ObjectPool
{
    /// <summary>
    /// 对象池管理器。
    /// </summary>
    /// <remarks>
    /// Object pool manager.
    /// </remarks>
    public interface IObjectPoolManager
    {
        /// <summary>
        /// 获取对象池数量。
        /// </summary>
        /// <remarks>
        /// Gets the number of object pools.
        /// </remarks>
        int Count { get; }

        /// <summary>
        /// 检查是否存在对象池。
        /// </summary>
        /// <remarks>
        /// Checks whether an object pool exists.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <returns>是否存在对象池。 / Whether the object pool exists.</returns>
        bool HasObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// 检查是否存在对象池。
        /// </summary>
        /// <remarks>
        /// Checks whether an object pool exists.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <returns>是否存在对象池。 / Whether the object pool exists.</returns>
        bool HasObjectPool(Type objectType);

        /// <summary>
        /// 检查是否存在对象池。
        /// </summary>
        /// <remarks>
        /// Checks whether an object pool exists.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <returns>是否存在对象池。 / Whether the object pool exists.</returns>
        bool HasObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// 检查是否存在对象池。
        /// </summary>
        /// <remarks>
        /// Checks whether an object pool exists.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <returns>是否存在对象池。 / Whether the object pool exists.</returns>
        bool HasObjectPool(Type objectType, string name);

        /// <summary>
        /// 检查是否存在对象池。
        /// </summary>
        /// <remarks>
        /// Checks whether an object pool exists.
        /// </remarks>
        /// <param name="condition">要检查的条件。 / The condition to check.</param>
        /// <returns>是否存在对象池。 / Whether the object pool exists.</returns>
        bool HasObjectPool(Predicate<ObjectPoolBase> condition);

        /// <summary>
        /// 获取对象池。
        /// </summary>
        /// <remarks>
        /// Gets the object pool.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <returns>要获取的对象池。 / The object pool to get.</returns>
        IObjectPool<T> GetObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// 获取对象池。
        /// </summary>
        /// <remarks>
        /// Gets the object pool.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <returns>要获取的对象池。 / The object pool to get.</returns>
        ObjectPoolBase GetObjectPool(Type objectType);

        /// <summary>
        /// 获取对象池。
        /// </summary>
        /// <remarks>
        /// Gets the object pool.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <returns>要获取的对象池。 / The object pool to get.</returns>
        IObjectPool<T> GetObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// 获取对象池。
        /// </summary>
        /// <remarks>
        /// Gets the object pool.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <returns>要获取的对象池。 / The object pool to get.</returns>
        ObjectPoolBase GetObjectPool(Type objectType, string name);

        /// <summary>
        /// 获取对象池。
        /// </summary>
        /// <remarks>
        /// Gets the object pool.
        /// </remarks>
        /// <param name="condition">要检查的条件。 / The condition to check.</param>
        /// <returns>要获取的对象池。 / The object pool to get.</returns>
        ObjectPoolBase GetObjectPool(Predicate<ObjectPoolBase> condition);

        /// <summary>
        /// 获取对象池。
        /// </summary>
        /// <remarks>
        /// Gets the object pools matching the condition.
        /// </remarks>
        /// <param name="condition">要检查的条件。 / The condition to check.</param>
        /// <returns>要获取的对象池。 / The matched object pools.</returns>
        ObjectPoolBase[] GetObjectPools(Predicate<ObjectPoolBase> condition);

        /// <summary>
        /// 获取对象池。
        /// </summary>
        /// <remarks>
        /// Gets the object pools matching the condition.
        /// </remarks>
        /// <param name="condition">要检查的条件。 / The condition to check.</param>
        /// <param name="results">要获取的对象池。 / The list to fill with the matched object pools.</param>
        void GetObjectPools(Predicate<ObjectPoolBase> condition, List<ObjectPoolBase> results);

        /// <summary>
        /// 获取所有对象池。
        /// </summary>
        /// <remarks>
        /// Gets all object pools.
        /// </remarks>
        /// <returns>所有对象池。 / All object pools.</returns>
        ObjectPoolBase[] GetAllObjectPools();

        /// <summary>
        /// 获取所有对象池。
        /// </summary>
        /// <remarks>
        /// Gets all object pools.
        /// </remarks>
        /// <param name="results">所有对象池。 / The list to fill with all object pools.</param>
        void GetAllObjectPools(List<ObjectPoolBase> results);

        /// <summary>
        /// 获取所有对象池。
        /// </summary>
        /// <remarks>
        /// Gets all object pools.
        /// </remarks>
        /// <param name="sort">是否根据对象池的优先级排序。 / Whether to sort by the priority of the object pools.</param>
        /// <returns>所有对象池。 / All object pools.</returns>
        ObjectPoolBase[] GetAllObjectPools(bool sort);

        /// <summary>
        /// 获取所有对象池。
        /// </summary>
        /// <remarks>
        /// Gets all object pools.
        /// </remarks>
        /// <param name="sort">是否根据对象池的优先级排序。 / Whether to sort by the priority of the object pools.</param>
        /// <param name="results">所有对象池。 / The list to fill with all object pools.</param>
        void GetAllObjectPools(bool sort, List<ObjectPoolBase> results);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(float expireTime) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float expireTime) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float expireTime);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, float expireTime) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, float expireTime);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, int priority);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, float expireTime, int priority);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, float expireTime) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, float expireTime);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, int priority);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float expireTime, int priority);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, int capacity, float expireTime, int priority);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, int capacity, float expireTime, int priority);

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。 / The interval in seconds for the pool to automatically release releasable objects.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        IObjectPool<T> CreateSingleSpawnObjectPool<T>(string name, float autoReleaseInterval, int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许单次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned only once.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。 / The interval in seconds for the pool to automatically release releasable objects.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许单次获取的对象池。 / The single-spawn object pool to create.</returns>
        ObjectPoolBase CreateSingleSpawnObjectPool(Type objectType, string name, float autoReleaseInterval, int capacity, float expireTime, int priority);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(float expireTime) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float expireTime) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float expireTime);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, float expireTime) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, float expireTime);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, int priority);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, float expireTime, int priority);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, float expireTime) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, float expireTime);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, int priority);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float expireTime, int priority);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, int capacity, float expireTime, int priority);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, int capacity, float expireTime, int priority);

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。 / The interval in seconds for the pool to automatically release releasable objects.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        IObjectPool<T> CreateMultiSpawnObjectPool<T>(string name, float autoReleaseInterval, int capacity, float expireTime, int priority) where T : ObjectBase;

        /// <summary>
        /// 创建允许多次获取的对象池。
        /// </summary>
        /// <remarks>
        /// Creates an object pool that allows an object to be spawned multiple times.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">对象池名称。 / The name of the object pool.</param>
        /// <param name="autoReleaseInterval">对象池自动释放可释放对象的间隔秒数。 / The interval in seconds for the pool to automatically release releasable objects.</param>
        /// <param name="capacity">对象池的容量。 / The capacity of the object pool.</param>
        /// <param name="expireTime">对象池对象过期秒数。 / The expire time of pool objects in seconds.</param>
        /// <param name="priority">对象池的优先级。 / The priority of the object pool.</param>
        /// <returns>要创建的允许多次获取的对象池。 / The multi-spawn object pool to create.</returns>
        ObjectPoolBase CreateMultiSpawnObjectPool(Type objectType, string name, float autoReleaseInterval, int capacity, float expireTime, int priority);

        /// <summary>
        /// 销毁对象池。
        /// </summary>
        /// <remarks>
        /// Destroys the object pool.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <returns>是否销毁对象池成功。 / Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool<T>() where T : ObjectBase;

        /// <summary>
        /// 销毁对象池。
        /// </summary>
        /// <remarks>
        /// Destroys the object pool.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <returns>是否销毁对象池成功。 / Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool(Type objectType);

        /// <summary>
        /// 销毁对象池。
        /// </summary>
        /// <remarks>
        /// Destroys the object pool.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="name">要销毁的对象池名称。 / The name of the object pool to destroy.</param>
        /// <returns>是否销毁对象池成功。 / Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool<T>(string name) where T : ObjectBase;

        /// <summary>
        /// 销毁对象池。
        /// </summary>
        /// <remarks>
        /// Destroys the object pool.
        /// </remarks>
        /// <param name="objectType">对象类型。 / The object type.</param>
        /// <param name="name">要销毁的对象池名称。 / The name of the object pool to destroy.</param>
        /// <returns>是否销毁对象池成功。 / Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool(Type objectType, string name);

        /// <summary>
        /// 销毁对象池。
        /// </summary>
        /// <remarks>
        /// Destroys the object pool.
        /// </remarks>
        /// <typeparam name="T">对象类型。 / The object type.</typeparam>
        /// <param name="objectPool">要销毁的对象池。 / The object pool to destroy.</param>
        /// <returns>是否销毁对象池成功。 / Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool<T>(IObjectPool<T> objectPool) where T : ObjectBase;

        /// <summary>
        /// 销毁对象池。
        /// </summary>
        /// <remarks>
        /// Destroys the object pool.
        /// </remarks>
        /// <param name="objectPool">要销毁的对象池。 / The object pool to destroy.</param>
        /// <returns>是否销毁对象池成功。 / Whether the object pool was destroyed successfully.</returns>
        bool DestroyObjectPool(ObjectPoolBase objectPool);

        /// <summary>
        /// 释放对象池中的可释放对象。
        /// </summary>
        /// <remarks>
        /// Releases the releasable objects in the object pools.
        /// </remarks>
        void Release();

        /// <summary>
        /// 释放对象池中的所有未使用对象。
        /// </summary>
        /// <remarks>
        /// Releases all unused objects in the object pools.
        /// </remarks>
        void ReleaseAllUnused();
    }
}