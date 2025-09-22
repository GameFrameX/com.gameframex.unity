// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace GameFrameX.Runtime
{
    /// <summary>
    /// System.UInt64 变量类。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class VarUInt64 : Variable<ulong>
    {
        /// <summary>
        /// 初始化 System.UInt64 变量类的新实例。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public VarUInt64()
        {
        }

        /// <summary>
        /// 从 System.UInt64 到 System.UInt64 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator VarUInt64(ulong value)
        {
            VarUInt64 varValue = ReferencePool.Acquire<VarUInt64>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.UInt64 变量类到 System.UInt64 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        [UnityEngine.Scripting.Preserve]
        public static implicit operator ulong(VarUInt64 value)
        {
            return value.Value;
        }
    }
}