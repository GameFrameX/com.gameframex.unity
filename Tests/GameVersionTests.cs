/*
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class GameVersionTests
    {
        private class TestVersionHelper : IVersionHelper
        {
            public string GameVersion { get; set; }

            public TestVersionHelper(string version)
            {
                GameVersion = version;
            }
        }

        #region GameFrameworkVersion

        [Test]
        public void GameFrameworkVersion_ReturnsNonNullString()
        {
            string version = GameVersion.GameFrameworkVersion;
            Assert.IsNotNull(version);
            Assert.IsNotEmpty(version);
        }

        [Test]
        public void GameFrameworkVersion_ReturnsExpectedValue()
        {
            Assert.AreEqual("0.1.0", GameVersion.GameFrameworkVersion);
        }

        #endregion

        #region AppVersion

        [Test]
        public void AppVersion_NoHelper_ReturnsEmptyString()
        {
            Assert.AreEqual(string.Empty, GameVersion.AppVersion);
        }

        #endregion

        #region SetVersionHelper

        [Test]
        public void SetVersionHelper_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<System.ArgumentNullException>(() => GameVersion.SetVersionHelper(null));
        }

        [Test]
        public void SetVersionHelper_ValidHelper_SetsAppVersion()
        {
            GameVersion.SetVersionHelper(new TestVersionHelper("1.2.3"));
            Assert.AreEqual("1.2.3", GameVersion.AppVersion);
        }

        #endregion
    }
}
*/
