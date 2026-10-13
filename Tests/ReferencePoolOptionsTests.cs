using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    internal sealed class ReferencePoolOptionsTests
    {
        [TearDown]
        public void TearDown()
        {
            GameFrameXOptions.Reset();
        }

        [Test]
        public void ReferencePoolOptions_ConfigureAndTryGet_RoundTripsDifferentialValues()
        {
            GameFrameXOptions.Reset();
            GameFrameXOptions.Configure(new ReferencePoolOptions { EnableStrictCheck = ReferenceStrictCheckType.OnlyEnableWhenDevelopment });

            // 组件 Awake 路径依赖运行时 Host，退化为直接验证注册表消费语义（TryGet 即组件 Awake 的读取入口）
            bool hit = GameFrameXOptions.TryGet<ReferencePoolOptions>(out var referencePoolOptions);

            Assert.IsTrue(hit, "Configure 后 TryGet 应命中");
            Assert.That(referencePoolOptions.EnableStrictCheck, Is.EqualTo(ReferenceStrictCheckType.OnlyEnableWhenDevelopment));
        }

        [Test]
        public void ReferencePoolOptions_DefaultIsAlwaysEnable()
        {
            GameFrameXOptions.Reset();
            GameFrameXOptions.Configure(new ReferencePoolOptions());

            Assert.IsTrue(GameFrameXOptions.TryGet<ReferencePoolOptions>(out var referencePoolOptions));
            Assert.That(referencePoolOptions.EnableStrictCheck, Is.EqualTo(ReferenceStrictCheckType.AlwaysEnable));
        }
    }
}
