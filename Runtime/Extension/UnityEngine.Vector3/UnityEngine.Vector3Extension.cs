﻿// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace UnityEngine
{
    /// <summary>
    /// 对 Unity 的扩展方法。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class UnityEngineVector3Extension
    {
        /// <summary>
        /// 取 <see cref="Vector3" /> 的 (x, y, z) 转换为 <see cref="Vector2" /> 的 (x, z)。
        /// </summary>
        /// <param name="vector3">要转换的 Vector3。</param>
        /// <returns>转换后的 Vector2。</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }


        /// <summary>
        /// 取 <see cref="Vector2" /> 的 (x, y) 转换为 <see cref="Vector3" /> 的 (x, 0, y)。
        /// </summary>
        /// <param name="vector3">要转换的 Vector3。</param>
        /// <returns>转换后的 Vector3。</returns>
        [UnityEngine.Scripting.Preserve]
        public static Vector3 ToVector3(this Vector3Int vector3)
        {
            return new Vector3(vector3.x, vector3.y, vector3.z);
        }
    }
}