using System.Reflection;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class GameFrameXCroppingHelperTests
    {
        #region Type Existence

        [Test]
        public void GameFrameXCroppingHelper_Exists()
        {
            Assert.IsNotNull(typeof(GameFrameXCroppingHelper));
        }

        [Test]
        public void GameFrameXCroppingHelper_IsSubclassOfMonoBehaviour()
        {
            Assert.IsTrue(typeof(GameFrameXCroppingHelper).IsSubclassOf(typeof(UnityEngine.MonoBehaviour)));
        }

        [Test]
        public void GameFrameXCroppingHelper_HasStartMethod()
        {
            var method = typeof(GameFrameXCroppingHelper).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method);
        }

        #endregion
    }
}
