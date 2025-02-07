namespace System
{
    /// <summary>
    /// 扩展方法，用于检查当前类型是否实现了指定的接口。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断当前类型是否实现了指定的接口。
        /// </summary>
        /// <param name="self">当前类型。</param>
        /// <param name="target">要检查的接口类型。</param>
        /// <returns>如果当前类型实现了指定的接口，则返回 true；否则返回 false。</returns>
        [UnityEngine.Scripting.Preserve]
        public static bool IsImplWithInterface(this Type self, Type target)
        {
            return self.GetInterface(target.FullName) != null && !self.IsInterface && !self.IsAbstract;
        }
    }
}