using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameFrameX.ObjectPool;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    /// <summary>
    /// 薄入口组件 API 等价性测试：ObjectPoolComponent 的公开方法面必须覆盖 IObjectPoolManager 的全部公开成员
    /// （同名 + 同泛型参数数 + 同参数签名），保证「组件 / GetModule 双入口」行为一致。
    /// </summary>
    [TestFixture]
    public class ObjectPoolComponentApiParityTests
    {
        [Test]
        public void ObjectPoolComponent_Forwards_EveryObjectPoolManagerInterfaceMethod()
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            var componentType = typeof(ObjectPoolComponent);

            var componentSignatures = new HashSet<string>(
                componentType.GetMethods(flags).Select(Signature));

            var missing = typeof(IObjectPoolManager).GetMethods(flags)
                .Select(m => new { Method = m, Signature = Signature(m) })
                .Where(x => !componentSignatures.Contains(x.Signature))
                .Select(x => x.Signature)
                .ToArray();

            Assert.IsEmpty(missing,
                "ObjectPoolComponent 缺少 IObjectPoolManager 的以下成员转发（薄入口 API 等价性被破坏）：" +
                string.Join("; ", missing));
        }

        private static string Signature(MethodInfo method)
        {
            var parameters = method.GetParameters()
                .Select(p => p.ParameterType.IsGenericParameter
                    ? "g" + p.ParameterType.GenericParameterPosition
                    : p.ParameterType.Name);
            var genericArity = method.IsGenericMethodDefinition ? method.GetGenericArguments().Length : 0;
            return method.Name + "/" + genericArity + "/" + string.Join(",", parameters);
        }
    }
}
